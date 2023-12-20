

$(document).ready(function () {

    //百分比(%)文字
    $(douoptions.fields).each(function () {
        var that = this;

        if (this.field == "Average") {
            this.formatter = function (v, d) {
                return v + '%';
            }
        }
    });

    douoptions.queryFilter = function (params, callback) {

        var Business_theme = params.find(a => a.key == "Business_theme");
        if (Business_theme != null) {
            Business_theme.value = Business_theme.value.split('_')[0];
        }

        //hidden不傳送參數
        if ($('[data-fn="Business_theme"]').is(":hidden")) {
            Business_theme.value = "";
        }
        if ($('[data-fn="CityCode1"]').is(":hidden")) {
            var CityCode1 = params.find(a => a.key == "CityCode1");
            CityCode1.value = "";
        }

        callback();
    }

    douoptions.tableOptions.onLoadSuccess = function (datas) {
        //圖表
        ShowMap(datas);
    }

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table
    //報表類型(必選)
    $('[data-fn="CaseType"] option[value=""]').remove();
    $('[data-fn="SType"] option[value=""]').remove();
    UIFilter();

    $('[data-fn="SType"]').change(function () {
        $('[data-fn="CityCode1"]').val("");
        $('[data-fn="Business_theme"]').val("");
        UIFilter();
    });
})

function UIFilter() {
    if ($('[data-fn="SType"]').val() == "byCity") {
        $('[data-fn="CityCode1"]').closest('.filter-continer').show();
        $('[data-fn="Business_theme"]').closest('.filter-continer').hide();
    }
    else if ($('[data-fn="SType"]').val() == "byBusi") {
        $('[data-fn="CityCode1"]').closest('.filter-continer').hide();
        $('[data-fn="Business_theme"]').closest('.filter-continer').show();
    }
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
            { key: 'CheckCount', name: '檢查家數' },
            { key: 'CheckNoHiatusCount', name: '查核零缺失' },
            { key: 'Average', name: '零缺失比例' },
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