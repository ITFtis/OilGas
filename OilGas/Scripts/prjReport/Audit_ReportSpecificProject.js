$(document).ready(function () {

    //百分比(%)文字
    $(douoptions.fields).each(function () {
        var that = this;

        //fileds n維處理
        $(that).each(function () {
            if (this.field == "CheckHiatusRate" || this.field == "ItemHiatusAllRate") {
                this.formatter = function (v, d) {
                    return v + '%';
                }
            }
        });
    });

    douoptions.queryFilter = function (params, callback) {

        //filter params特殊參數調整
        var CaseType = params.find(a => a.key == "CaseType");

        if (CaseType != null) {
            var fn = CaseType.key;
            var workTable = $('.filter-toolbar-plus').find('[data-fn="' + fn + '"] option:selected').attr('data-checkitemtable');
            CaseType.value = workTable;
        }

        var CityCode1 = params.find(a => a.key == "CityCode1");
        CityCode1.value = CityCode1.value.join(',');

        var Business_theme = params.find(a => a.key == "Business_theme");
        //多選
        var strs = Business_theme.value;
        $(strs).each(function (index, value) {
            strs[index] = value.split('_')[0];
        });
        Business_theme.value = strs.join(',');

        callback();
    }

    douoptions.tableOptions.onLoadSuccess = function (datas) {        
        //圖表
        ShowMap(datas);
    }

    var a = {};
    a.item = '<span class="btn btn-success glyphicon glyphicon-download-alt"> 匯出Excel</span>';
    a.event = 'click .glyphicon-download-alt';
    a.callback = function ExportExcel(evt) {

        var conditions = GetFilterParams($_MasterTable)

        //filter params特殊參數調整
        var CaseType = conditions.find(a => a.key == "CaseType");
        var Business_theme = conditions.find(a => a.key == "Business_theme");
        if (CaseType != null) {
            var fn = CaseType.key;
            var workTable = $('.filter-toolbar-plus').find('[data-fn="' + fn + '"] option:selected').attr('data-checkitemtable');
            CaseType.value = workTable;
        }

        if (Business_theme != null) {
            //多選
            var strs = Business_theme.value.split(',');
            $(strs).each(function (index, value) {
                strs[index] = value.split('_')[0];
            });
            Business_theme.value = strs.join(',');
        }

        var paras;
        if (conditions.length > 0) {
            paras = { key: 'filter', value: JSON.stringify(conditions) };
        }

        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'Audit_ReportSpecificProject/ExportAudit_ReportSpecificProject',
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

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table
    //報表類型(必選)    
    $('[data-fn="CaseType"] option[value=""]').remove();
    $('[data-fn="CaseType"] option[value="FishGas_BasicData"]').remove();   //原係統無此統計
    $('[data-fn="CaseType"] option[value="SelfFuel_Basic_Up"]').remove();   //原係統無此統計
    $('[data-fn="CaseType"] option[value="SelfFuel_Basic_Down"]').remove(); //原係統無此統計

    $('[data-fn="CaseType"]').change(function () {
        RestSelectpickerCaseType();
    })

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
                return '營業主體:挑' + sc + '個'
            }
        });    
})

function RestSelectpickerCaseType() {
    var CaseType = $('.filter-toolbar-plus').find("[data-fn=CaseType]").val();

    var $ele = $('.filter-toolbar-plus').find("[data-fn=Business_theme]");    
    $ele.find('[data-casetype!="' + CaseType + '"]').hide();
    $ele.selectpicker('refresh').selectpicker('val', '');
}

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
            { key: 'CheckAllCount', name: '查核家數' },
            { key: 'CheckHiatusCount', name: '該項目有缺失家數' },
            { key: 'CheckHiatusRate', name: '缺失發生比例' },
            { key: 'ItemHiatusAllRate', name: '佔總缺失比例' },            
        ];

        names.forEach(function (obj) {
            //特定群組來源
            var source = $.grep(datas, function (item, index) {
                return item[obj.key];
            });

            //數據null，自動補資料(0)  沒補則斷線
            $.each(xUnit, function (index, value) {
                var x = value;
                var exist = source.find(obj => obj.CheckYear == x);
                if (!exist) {
                    var defalult = {
                        CheckYear: x,
                        //CheckAllCount: 0
                    }
                    source.push(defalult);
                }
            });

            //補0是用Push，需要再重新排序 x(CheckYear)
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
                text: '查核缺失趨勢交叉分析報表',
                align: 'center',
            },

            subtitle: {
                text: '',
                align: 'left'
            },

            yAxis: {
                title: {
                    text: '平均缺失(項)'
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