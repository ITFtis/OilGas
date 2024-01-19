$(document).ready(function () {
    var w1 = "";
    var w2 = "";
    var w3 = "";
    var w4 = "";

    var workItem = douHelper.getField(douoptions.fields, 'workItem');
    workItem.rowspan = 2;
    workItem.colspan = 1;
    workItem.valign = "middle";
    workItem.align = "center";
    douoptions.fields = [
        [
            //filter(2 cols)
            { "field": "", "title": "", align: "center", colspan: 2 },

            //data
            workItem,
            { "field": "", "title": "年講習會出席", align: "center", colspan: 3 },
            { "field": "", "title": "年講習會出席", align: "center", colspan: 3 },
            { "field": "", "title": "年查核結果", align: "center", colspan: 3 },
            { "field": "", "title": "年查核結果", align: "center", colspan: 3 },
        ],
        [
            //filter(2 cols)
            douHelper.getField(douoptions.fields, 'Counseling_Year'),
            douHelper.getField(douoptions.fields, 'CheckYear'),

            //data
            douHelper.getField(douoptions.fields, 'AttendSCount1'),
            douHelper.getField(douoptions.fields, 'DenominatorCount1'),
            douHelper.getField(douoptions.fields, 'CounselingRate1'),
            douHelper.getField(douoptions.fields, 'AttendSCount2'),
            douHelper.getField(douoptions.fields, 'DenominatorCount2'),
            douHelper.getField(douoptions.fields, 'CounselingRate2'),
            douHelper.getField(douoptions.fields, 'SumCheckCount1'),
            douHelper.getField(douoptions.fields, 'SumCheckError1'),
            douHelper.getField(douoptions.fields, 'Average1'),
            douHelper.getField(douoptions.fields, 'SumCheckCount2'),
            douHelper.getField(douoptions.fields, 'SumCheckError2'),
            douHelper.getField(douoptions.fields, 'Average2'),
        ],
    ]

    var a = {};
    a.item = '<span class="btn btn-success glyphicon glyphicon-download-alt"> 匯出Excel</span>';
    a.event = 'click .glyphicon-download-alt';
    a.callback = function ExportExcel(evt) {
        var conditions = GetFilterParams($_MasterTable)
        if (conditions.length > 0) {
            var paras = { key: 'filter', value: JSON.stringify(conditions) };
        }

        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'Audit_CounselingReportMissing1/ExportAudit_CounselingReportMissing1',
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

    douoptions.tableOptions.onSort = function (a) {
        //InsertTotalAfterSorted();
    }
    douoptions.tableOptions.onLoadSuccess = function (datas) {
        //InsertTotalAfterSorted();
        getChart();
    }

    //匯出功能
    douoptions.appendCustomToolbars = [a];

    douoptions.queryFilter = function (params, callback) {

        var Counseling_Year = params.find(a => a.key == "Counseling_Year");
        //CITY.value = JSON.stringify(CITY.value);
        Counseling_Year.value = Counseling_Year.value.join(',');
        var split_Counseling_Year = (Counseling_Year.value).split(',');
        split_Counseling_Year = split_Counseling_Year.filter((split_Counseling_Year) => split_Counseling_Year !== "");
        var intCount1 = split_Counseling_Year.length;
        if (intCount1 > 2) {
            alert("講習會年度最多只能勾選兩個年度");
        }
        if (intCount1 > 0 && intCount1 <= 2) {
            w1 = split_Counseling_Year[0];
            w2 = split_Counseling_Year[1];
            var t1 = '<div class="th-inner" >' + w1 + '年講習會出席</div>';
            var t2 = '<div class="th-inner" >' + w2 + '年講習會出席</div>';
            document.getElementById("_table").rows[0].cells[1].innerHTML = t1;
            document.getElementById("_table").rows[0].cells[2].innerHTML = t2;
        }

        var CheckYear = params.find(a => a.key == "CheckYear");
        CheckYear.value = CheckYear.value.join(',');
        var split_CheckYear = (CheckYear.value).split(',');
        split_CheckYear = split_CheckYear.filter((split_CheckYear) => split_CheckYear !== "");
        var intCount2 = split_CheckYear.length;
        if (intCount2 > 2) {
            alert("查核輔導年度最多只能勾選兩個年度");
        }
        if (intCount2 > 0 && intCount2 <= 2) {
            w3 = split_CheckYear[0];
            w4 = split_CheckYear[1];
            var t3 = '<div class="th-inner" >' + w3 + '年查核結果</div>';
            var t4 = '<div class="th-inner" >' + w4 + '年查核結果</div>';
            document.getElementById("_table").rows[0].cells[3].innerHTML = t3;
            document.getElementById("_table").rows[0].cells[4].innerHTML = t4;
        }

        callback();
    }

    //查詢(Chart)
    function getChart() {
        $.ajax({
            type: "POST",
            url: $.AppConfigOptions.baseurl + "Audit_CounselingReportMissing1/GetChartData",
            success: function (res) {
                console.log('success');
                if (res.length == 0) {
                    $("#container1").hide();
                    return false;
                }
                $("#container1").show();
                var workItem = [];
                var Counseling_Year_Average1 = [];
                var Counseling_Year_Average2 = [];
                var CheckYear_Average1 = [];
                var CheckYear_Average2 = [];
                var Counseling_Year1 = res[0].Counseling_Year1;
                var Counseling_Year2 = res[0].Counseling_Year2;
                var CheckYear1 = res[0].CheckYear1;
                var CheckYear2 = res[0].CheckYear2;
                for (var i = 0; i < res.length; i++) {
                    workItem.push(res[i].workItem);
                    Counseling_Year_Average1.push(res[i].Counseling_Year_Average1);
                    Counseling_Year_Average2.push(res[i].Counseling_Year_Average2);
                    CheckYear_Average1.push(res[i].CheckYear_Average1);
                    CheckYear_Average2.push(res[i].CheckYear_Average2);
                }
                console.log(workItem, Counseling_Year_Average1, Counseling_Year_Average2, CheckYear_Average1, CheckYear_Average2);

                //
                Highcharts.chart('container1', {
                    chart: {
                        
                        borderColor: "gray",
                        borderWidth: 3,
                        borderRadius: 20,
                    },

                    title: {
                        text: '各縣市出席率與查核缺失數比較圖',
                        align: 'center'
                    },

                    subtitle: {
                        text: '',
                        align: 'left'
                    },

                    yAxis: [{
                        title: {
                            text: '平均查核次數'
                        },
                        plotLines: [{
                            value: 0,
                        }]
                    },
                        {
                        title: {
                            text: "出席率"
                            },
                        opposite: true 
                     }],

                    xAxis: {
                        title: {
                            text: '縣市'
                        },
                        categories: workItem,
                        gridLineWidth: 1,
                        tickWidth: 1,
                        tickmarkPlacement: 'on',
                    },

                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle'
                    },

                    //plotOptions: {
                    //    column: {
                    //        stacking: 'percent',
                    //        dataLabels: {
                    //            enabled: true,
                    //            format: '{point.percentage:.2f}%'
                    //        }
                    //    },
                    //    series: {
                    //        label: {
                    //            connectorAllowed: false
                    //        },

                    //    },
                    //    line: {
                    //        dataLabels: {
                    //            enabled: true
                    //        },
                    //        enableMouseTracking: true
                    //    }
                    //},

                    series: [
                    {
                        type: 'line',
                        name: Counseling_Year1 + '年講習',
                        data: Counseling_Year_Average1,
                        yAxis: 1,
                        color: 'PaleVioletRed',
                    },
                    {
                        type: 'line',
                        name: Counseling_Year2 + '年講習',
                        data: Counseling_Year_Average2,
                        yAxis: 1,
                        color: 'green',
                    },
                    {
                        type: 'column',
                        name: CheckYear1 + '年查核',
                        data: CheckYear_Average1,
                        color: 'lightgreen',
                    },
                    {
                        type: 'column',
                        name: CheckYear2 + '年查核',
                        data: CheckYear_Average2,
                        color: 'DeepPink',
                    }
                    ],

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

    //多選
    var Counseling_Year = $('.filter-toolbar-plus').find("[data-fn=Counseling_Year]")
        .attr('multiple', true).selectpicker({
            actionsBox: true,
            selectAllText: '全選',
            deselectAllText: '取消已選',
            selectedTextFormat: 'count > 1',
            countSelectedText: function (sc, all) {
                return '講習會年度:挑' + sc + '個'
            }
        });
    var CheckYear = $('.filter-toolbar-plus').find("[data-fn=CheckYear]")
        .attr('multiple', true).selectpicker({
            actionsBox: true,
            selectAllText: '全選',
            deselectAllText: '取消已選',
            selectedTextFormat: 'count > 1',
            countSelectedText: function (sc, all) {
                return '查核輔導年度:挑' + sc + '個'
            },
        });

})

