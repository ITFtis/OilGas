$(document).ready(function () {
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
            url: app.siteRoot + 'SelfFuel_StopBusiness/ExportSelfFuel_StopBusiness',
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

    douoptions.tableOptions.onSort = function (a) {
        InsertTotalAfterSorted();
    }
    douoptions.tableOptions.onLoadSuccess = function (datas) {
        InsertTotalAfterSorted();
        getChart();
    }

    //查詢(Chart)
    function getChart() {
        $.ajax({
            type: "POST",
            url: $.AppConfigOptions.baseurl + "SelfFuel_StopBusiness/GetChartData",
            success: function (res) {
                console.log('success');
                if (res.length == 0) {
                    $("#container1").hide();
                    $("#container2").hide();
                    return false;
                }
                $("#container1").show();
                $("#container2").show();
                var selectYear = $("[data-fn=Year_Start]").val();
                var workCity = [];
                var AddBusiness = [];
                var EndBusiness = [];
                for (var i = 0; i < res.length; i++) {
                    workCity.push(res[i].workCity);
                    AddBusiness.push(res[i].AddBusiness);
                    EndBusiness.push(res[i].EndBusiness);
                }
                console.log(workCity);
                console.log(AddBusiness);
                console.log(EndBusiness);
                //新設、歇業
                Highcharts.chart('container1', {
                    chart: {
                        type: 'column',
                        borderColor: "gray",
                        borderWidth: 3,
                        borderRadius: 20,
                    },

                    title: {
                        text: '各縣市' + selectYear+'年度新設與歇業之趨勢圖',
                        align: 'center'
                    },

                    subtitle: {
                        text: '',
                        align: 'left'
                    },

                    yAxis: {
                        title: {
                            text: '自用加儲油站數'
                        },
                        plotLines: [{
                            value: 0,

                        }]
                    },

                    xAxis: {
                        title: {
                            text: '縣市別'
                        },
                        categories: workCity,
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
                        name: '新設',
                        data: AddBusiness,
                        color: 'lightblue',
                    },
                    {
                        name: '歇業',
                        data: EndBusiness,
                        color: 'red',
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

    function InsertTotalAfterSorted() {
        var fd2 = new FormData();
        console.log(fd2);
        $.ajax({
            dataType: 'json',
            url: $.AppConfigOptions.baseurl + 'SelfFuel_StopBusiness/GetSumData',
            type: "post",
            //data: new FormData($('#adj')['0']),
            data: fd2,
            processData: false,
            contentType: false,
            success: function (data) {
                //alert("儲存成功"); 
                if (data[0] != '' && data[1] != '') {
                    var $ppp = $('#_table tbody');
                    var content = '<tr data-index="9999"> \
                           <td class="dou-field-workCity">總計：</td> \
                           <td class="dou-field-AddBusiness" style="text-align: right; ">'+ data[0] + '</td> \
                           <td class="dou-field-EndBusiness" style="text-align: right; ">'+ data[1] + '</td> \
                       </tr>';
                    $(content).appendTo($ppp);
                }
            },
            error: function (request) {
                //alert(request.responseJSON.Message);
                alert("error");
            }
        });
    }

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table

    //明細
    $('#_table').on('click', 'td', function () {
        // 獲取所點選欄位的數值
        const cellData = $(this).text();

        // 獲取所在行的所有欄位數值
        const rowData = $(this).closest('tr').find('td').map(function () {
            return $(this).text();
        }).get();
        // 獲取被點選的欄位索引
        const columnIndex = $(this).index();
        // 顯示被點選的欄位是第幾個
        console.log('你點選的欄位是第 ' + (columnIndex + 1) + ' 欄');
        // 顯示你點選的欄位值
        console.log('你點選的欄位值是：' + cellData);
        // 顯示所在行的所有欄位值
        console.log('所在行的所有欄位值是：');
        console.log(rowData);
        if (cellData == "0" || rowData[0] == "總計：" || columnIndex == 0) {
            return;
        }
        // var $_DetailTable = $("#_table_detail").DouEditableTable(douoptions);
        var workType = "";
        var workTypeCN = "";
        switch (columnIndex) {
            case 1:
                workType = "AddBusiness";
                workTypeCN = "新設";
                break;
            case 2:
                workType = "EndBusiness";
                workTypeCN = "歇業";
                break;
            default:
                workType = "";
                workTypeCN = "";
        }
        var years = parseInt($("[data-fn=Year_Start]").val()) + 1911;
        $("#titledetail").text(rowData[0] + years + "年度" + workTypeCN + "之自用加儲油站清單")
        $("#ITPopUp").modal('show');
        console.log(rowData[0], years, workType);
        var fd = new FormData();
        fd.append('workCity', rowData[0]);
        fd.append('workYear', years);
        fd.append('workType', workType);
        console.log(fd);
        $.ajax({
            url: app.siteRoot + 'SelfFuel_StopBusiness/GetDetailTableData',
            datatype: "json",
            type: "POST",
            //data: { paras: [paras] },
            data: fd,
            processData: false,
            contentType: false,
            success: function (data) {
                console.log("success")
                $("#_table_detail tr:not(:first)").html("");
                var trHTML = '';
                $.each(data, function (i, item) {
                    trHTML += '<tr><td>' + item.CaseNo + '</td><td>' + item.Fuel_Name + '</td><td>'
                        + item.Business_theme + '</td><td>' + item.Dispatch_date + '</td><td>'
                        + item.Dispatch_No + '</td><td>' + item.DispatchName + '</td><td>' + item.CityName + '</td></tr>';
                });
                $('#_table_detail').append(trHTML);
                $("#DetailCount").text("共 " + data.length + " 筆")
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
    });
})
//明細取消
function closeDialog() {
    console.log("closed");
    $("#ITPopUp").modal('hide');
}
//匯出明細
function ExportDetailExcel() {
    var fd = new FormData();
    fd.append('titlename', $("#titledetail").text());
    $.ajax({
        url: app.siteRoot + 'SelfFuel_StopBusiness/ExportSelfFuel_workBusinessDesc',
        datatype: "json",
        type: "POST",
        //data: { paras: [paras] },
        data: fd,
        processData: false,
        contentType: false,
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
}
