$(document).ready(function () {
    var Sum_Total = douHelper.getField(douoptions.fields, 'Sum_Total');
    Sum_Total.align = "center";
    douoptions.tableOptions.onSort = function (a) {
        //InsertTotalAfterSorted();
    }
    douoptions.tableOptions.onLoadSuccess = function (datas) {
        //InsertTotalAfterSorted();
        getChart();
    }

    //匯出功能
    //douoptions.appendCustomToolbars = [a];

    douoptions.queryFilter = function (params, callback) {

        callback();
    }
    douoptions.afterUpdateServerData = function (row, callback) {
        $('[role=tablist]').empty();
        callback();
    }

    //查詢(Chart)
    function getChart() {
        //加油站
        $.ajax({
            type: "POST",
            url: $.AppConfigOptions.baseurl + "Info_Dashboard/GetChartData_CarFuel",
            success: function (res) {
                console.log('success');
                if (res.length == 0) {
                    $("#container1").hide();
                    return false;
                }
                $("#container1").show();
                var years = [];
                var counts = [];
                for (var i = 0; i < res.length; i++) {
                    years.push(res[i].year);
                    counts.push(res[i].counts);
                }
                console.log(years);
                console.log(counts);

                Highcharts.chart('container1', {
                    chart: {
                        type: 'line',
                        borderColor: "gray",
                        borderWidth: 3,
                        borderRadius: 20,
                    },

                    title: {
                        text: '加油站總站數',
                        align: 'center',
                    },

                    subtitle: {
                        text: '',
                        align: 'left'
                    },

                    yAxis: {
                        title: {
                            text: '站數'
                        },
                        plotLines: [{
                            value: 0,

                        }]
                    },

                    xAxis: {
                        title: {
                            text: '年份'
                        },
                        categories: years,
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

                    series: [{
                        name: '站數',
                        data: counts,
                        color: 'Green'

                    }],

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
            },
            error: function (request) {
                alert("Error");
            }
        });
        //汽車加氣站
        $.ajax({
            type: "POST",
            url: $.AppConfigOptions.baseurl + "Info_Dashboard/GetChartData_CarGas",
            success: function (res) {
                console.log('success');
                if (res.length == 0) {
                    $("#container2").hide();
                    return false;
                }
                $("#container2").show();
                var years = [];
                var counts = [];
                for (var i = 0; i < res.length; i++) {
                    years.push(res[i].year);
                    counts.push(res[i].counts);
                }
                console.log(years);
                console.log(counts);

                Highcharts.chart('container2', {
                    chart: {
                        type: 'column',
                        borderColor: "gray",
                        borderWidth: 3,
                        borderRadius: 20,
                    },

                    title: {
                        text: '汽車加氣站總站數',
                        align: 'center',
                    },

                    subtitle: {
                        text: '',
                        align: 'left'
                    },

                    yAxis: {
                        title: {
                            text: '站數'
                        },
                        plotLines: [{
                            value: 0,

                        }]
                    },

                    xAxis: {
                        title: {
                            text: '年份'
                        },
                        categories: years,
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

                    series: [{
                        name: '站數',
                        data: counts,
                        color: 'Blue'

                    }],

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
            },
            error: function (request) {
                alert("Error");
            }
        });
        //漁船加油站
        $.ajax({
            type: "POST",
            url: $.AppConfigOptions.baseurl + "Info_Dashboard/GetChartData_FishGas",
            success: function (res) {
                console.log('success');
                if (res.length == 0) {
                    $("#container3").hide();
                    return false;
                }
                $("#container3").show();
                var years = [];
                var counts = [];
                for (var i = 0; i < res.length; i++) {
                    years.push(res[i].year);
                    counts.push(res[i].counts);
                }
                console.log(years);
                console.log(counts);

                Highcharts.chart('container3', {
                    chart: {
                        type: 'column',
                        borderColor: "gray",
                        borderWidth: 3,
                        borderRadius: 20,
                    },

                    title: {
                        text: '漁船加油站總站數',
                        align: 'center',
                    },

                    subtitle: {
                        text: '',
                        align: 'left'
                    },

                    yAxis: {
                        title: {
                            text: '站數'
                        },
                        plotLines: [{
                            value: 0,

                        }]
                    },

                    xAxis: {
                        title: {
                            text: '年份'
                        },
                        categories: years,
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

                    series: [{
                        name: '站數',
                        data: counts,
                        color: 'Red'

                    }],

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
            },
            error: function (request) {
                alert("Error");
            }
        });
        //自用儲油站
        $.ajax({
            type: "POST",
            url: $.AppConfigOptions.baseurl + "Info_Dashboard/GetChartData_FishGas",
            success: function (res) {
                console.log('success');
                if (res.length == 0) {
                    $("#container4").hide();
                    return false;
                }
                $("#container4").show();
                var years = [];
                var counts = [];
                for (var i = 0; i < res.length; i++) {
                    years.push(res[i].year);
                    counts.push(res[i].counts);
                }
                console.log(years);
                console.log(counts);

                Highcharts.chart('container4', {
                    chart: {
                        type: 'column',
                        borderColor: "gray",
                        borderWidth: 3,
                        borderRadius: 20,
                    },

                    title: {
                        text: '自用加儲油總站數',
                        align: 'center',
                    },

                    subtitle: {
                        text: '',
                        align: 'left'
                    },

                    yAxis: {
                        title: {
                            text: '站數'
                        },
                        plotLines: [{
                            value: 0,

                        }]
                    },

                    xAxis: {
                        title: {
                            text: '年份'
                        },
                        categories: years,
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

                    series: [{
                        name: '站數',
                        data: counts,
                        color: 'Orange'

                    }],

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
            },
            error: function (request) {
                alert("Error");
            }
        });
    }

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table

    $('[data-fn="CaseType"] option[value=""]').remove();
    $('[data-fn="CheckYear"] option[value=""]').remove();
})

