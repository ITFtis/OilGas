

$(document).ready(function () {

    douoptions.queryFilter = function (params, callback) {

        var CityCode1 = params.find(a => a.key == "CityCode1");
        CityCode1.value = CityCode1.value.join(',');

        var Business_theme = params.find(a => a.key == "Business_theme");
        Business_theme.value = Business_theme.value.join(',');

        callback();
    }

    var a = {};
    a.item = '<span class="btn btn-success glyphicon glyphicon-download-alt"> 匯出Excel</span>';
    a.event = 'click .glyphicon-download-alt';
    a.callback = function ExportExcel(evt) {

        var conditions = GetFilterParams($_MasterTable)
        var paras;
        if (conditions.length > 0) {
            paras = { key: 'filter', value: JSON.stringify(conditions) };
        }

        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'Audit_ReportPreviousDistribution/ExportAudit_ReportPreviousDistribution',
            datatype: "json",
            type: "POST",
            data: { paras: [paras] },
            success: function (data) {
                if (data.result) {
                    location.href = app.siteRoot + data.url;                    
                } else {
                    alert("查詢失敗：\n" + data.errorMessage);
                }
            },
            complete: function () {
                helper.misc.hideBusyIndicator();
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
                helper.misc.hideBusyIndicator();
            }
        });
    };

    douoptions.appendCustomToolbars = [a];

    douoptions.tableOptions.onLoadSuccess = function (datas) {
        //圖表
        ShowMap(datas);
    }

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table

    //多選
    var $CityCode1 = $('.filter-toolbar-plus').find("[data-fn=CityCode1]")
        .attr('multiple', true).selectpicker({
            actionsBox: true,
            selectAllText: '全選',
            deselectAllText: '取消已選',
            selectedTextFormat: 'count > 1',
            countSelectedText: function (sc, all) {
                return '縣市:挑' + sc + '個'
            }
        });
    //多選
    var $Business_theme = $('.filter-toolbar-plus').find("[data-fn=Business_theme]")
        .attr('multiple', true).selectpicker({
            actionsBox: true,
            selectAllText: '全選',
            deselectAllText: '取消已選',
            selectedTextFormat: 'count > 1',
            countSelectedText: function (sc, all) {
                return '縣市:挑' + sc + '個'
            }
        });
})

function ShowMap(datas) {

    //清空圖表
    if ($('#container1').highcharts())
        $('#container1').highcharts().destroy();

    if (datas.length > 0) {
        var xUnit = [];     //X軸資料 單位:95,96年        
        var yLine = [{}];   //Y軸資料 數據:{群組}
        var colors = GetDefaultColors();

        //1.X軸 => CheckYear
        var setUnits = $.map(datas, function (item, index) {
            return item.CheckYear;
        });
        xUnit = Array.from(new Set(setUnits));  //distinct  new Set(setUnits)

        //2.Y軸 => AvgMiss        
        var names = [
            { key:'CheckAllCount', name: '查核家數'},            
            { key: 'CheckAveCount', name: '平均缺失數'},
            { key: 'CheckNoHiatusCount', name: '零缺失家數' },
            { key: 'CheckNoHiatusRate', name: '零缺失比例' }
        ];

        names.forEach(function (obj) {
            //特定群組來源
            var source = $.grep(datas, function (item, index) {
                return item[obj.key];
            });

            //數據null，自動補資料(0)  沒補則斷線
            $.each(xUnit, function (index, value) {
                var year = value;
                var exist = source.find(obj => obj.CheckYear == year);
                if (!exist) {
                    var defalult = {
                        CheckYear: year,
                        CheckAllCount: 0, CheckAllDoesmeet: 0, CheckAveCount: 0., CheckNoHiatusCount: 0
                    }
                    source.push(defalult);
                }
            });

            //補0是用Push，需要再重新排序 CheckYear
            source = source.sort(function (a, b) {
                var x = a.CheckYear < b.CheckYear ? -1 : 1;
                return x;
            })

            //資料數據
            var list = $.map(source, function (item, index) {
                return item[obj.key] == null ? 0 : item[obj.key];
            });       

            //queue(shift):先進先出
            var data = {
                name: obj.name,
                data: list,
                color: colors.shift()
            }

            //群組數據
            yLine.push(data);
        });

        //圖表設定
        Highcharts.chart('container1', {
            chart: {
                type: 'line',
                borderColor: "gray",
                borderWidth: 3,
                borderRadius: 20,
            },

            title: {
                text: '歷年度查核次數與缺失分布',
                align: 'center',
            },

            subtitle: {
                text: '',
                align: 'left'
            },

            yAxis: {
                title: {
                    text: '查核次數'
                },
                plotLines: [{
                    value: 0,

                }]
            },

            xAxis: {
                title: {
                    text: '年份'
                },
                categories: xUnit,
                gridLineWidth: 1,
                tickWidth: 1,
                tickmarkPlacement: 'on',
            },

            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'middle'
            },

            plotOptions: {
                series: {
                    label: {
                        connectorAllowed: false
                    },
                },
                line: {
                    dataLabels: {
                        enabled: true
                    },
                    enableMouseTracking: true
                }
            },

            series: yLine,

            responsive: {
                rules: [{
                    condition: {
                        maxWidth: 500
                    },
                    chartOptions: {
                        legend: {
                            layout: 'horizontal',
                            align: 'center',
                            verticalAlign: 'bottom'
                        }
                    }
                }]
            }

        });
    }
}