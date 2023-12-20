$(document).ready(function () {
    
    douoptions.queryFilter = function (params, callback) {
        var DDLCityCode1 = params.find(a => a.key == "DDLCityCode1");
        DDLCityCode1.value = DDLCityCode1.value.join(',');

        var DDLBusiness_theme = params.find(a => a.key == "DDLBusiness_theme");
        DDLBusiness_theme.value = DDLBusiness_theme.value.join(',');

        //驗證
        if (!ToValidate(params)) {
            return;
        }

        callback();
    }

    douoptions.tableOptions.onLoadSuccess = function (datas) {
        //var MapName = douHelper.getField($_MasterTable.instance.settings.fields, "MapName");
        if ($('[data-fn="SType"]').val() == 'byCity') {
            $('[data-field="MapName"] .th-inner').text('縣市別');
        }
        else if ($('[data-fn="SType"]').val() == 'byBusi') {
            $('[data-field="MapName"] .th-inner').text('營業主體');
        }

        //圖表
        ShowMap(datas);
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

        //驗證
        if (!ToValidate(conditions)) {
            return;
        }

        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'Audit_ReportCheck_counts_CrossAnalysis/ExportAudit_ReportCheck_counts_CrossAnalysis',
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
    $('[data-fn="DDLCityCode1"] option[value=""]').text('全國');
    $('[data-fn="DDLBusiness_theme"] option[value=""]').text('集團');
    ////test
    //$('[data-fn="SYear"] option[value=""]').remove();
    //$('[data-fn="EYear"] option[value=""]').remove();
    $('[data-fn="SType"] option[value=""]').remove();
    $('[data-fn="SType"]').change(function () {
        SetSTypeUI();
    })
    

    //多選
    var $DDLCityCode1 = $('.filter-toolbar-plus').find("[data-fn=DDLCityCode1]")
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
    var $DDLBusiness_theme = $('.filter-toolbar-plus').find("[data-fn=DDLBusiness_theme]")
        .attr('multiple', true).selectpicker({
            actionsBox: true,
            selectAllText: '全選',
            deselectAllText: '取消已選',
            selectedTextFormat: 'count > 1',
            countSelectedText: function (sc, all) {
                return '營業主體:挑' + sc + '個'
            }
        });
        
    function SetSTypeUI() {
        var val = $('[data-fn="SType"]').val();

        //filter 單選與全選取消
        $('.filter-toolbar-plus').find("[data-fn=CityCode1]").prop('selectedIndex', 0);
        $DDLCityCode1.selectpicker('deselectAll').selectpicker('val', '');
        $('.filter-toolbar-plus').find("[data-fn=Business_theme]").prop('selectedIndex', 0);
        $DDLBusiness_theme.selectpicker('deselectAll').selectpicker('val', '');

        if (val == 'byCity') {
            //縣市查
            $('[data-fn="DDLCityCode1"]').closest('.filter-continer').show();
            $('[data-fn="CityCode1"]').closest('.filter-continer').hide();
            $('[data-fn="DDLBusiness_theme"]').closest('.filter-continer').hide();
            $('[data-fn="Business_theme"]').closest('.filter-continer').show();
        }
        else if (val == 'byBusi') {
            //營業主體查
            //alert('aaa');
            $('[data-fn="DDLCityCode1"]').closest('.filter-continer').hide();
            $('[data-fn="CityCode1"]').closest('.filter-continer').show();
            $('[data-fn="DDLBusiness_theme"]').closest('.filter-continer').show();
            $('[data-fn="Business_theme"]').closest('.filter-continer').hide();
        }
    }

    SetSTypeUI();
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
        var setNames = $.map(datas, function (item, index) {
            return item.MapName;  //欄位名稱(ex：各縣市)
        });        
        var names = new Set(setNames);  //distinct

        names.forEach(function (name) {
            //特定群組來源
            var source = $.grep(datas, function (item, index) {
                return item.MapName === name;
            });

            //資料數據
            var list = $.map(source, function (item, index) {
                return item.AvgMiss;
            });

            //queue(shift):先進先出
            var data = {
                name: name,
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

function ToValidate(params) {
    var SYear = params.find(a => a.key == "SYear");
    var EYear = params.find(a => a.key == "EYear");

    if (SYear.value == "") {
        var text = $('[data-fn="SYear"] option:selected').text();
        alert(text);
        return false;
    }

    if (EYear.value == "") {
        var text = $('[data-fn="EYear"] option:selected').text();
        alert(text);
        return false;
    }

    return true;
}