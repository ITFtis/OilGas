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
            url: app.siteRoot + 'CarFuel_Closed/ExportCarFuel_Closed',
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
        //alert('aaa');
        InsertTotalAfterSorted();
    }
    douoptions.tableOptions.onLoadSuccess = function (datas) {
        InsertTotalAfterSorted();
        //filter日期(date)
        //$('[data-fn="CheckDate-Start-Between_"] input').attr('type', 'date');
        //$('[data-fn="CheckDate-End-Between_"] input').attr('type', 'date');
    }

    //douoptions.queryFilter = function (params, callback) {

    //    var CITY = params.find(a => a.key == "CITY");
    //    //CITY.value = JSON.stringify(CITY.value);
    //    CITY.value = CITY.value.join(',');

    //    //params取資料yyyy/MM/dd 00:00:00
    //    var checkDateStart = params.find(a => a.key == "CheckDate-Start-Between_");
    //    var checkDateEnd = params.find(a => a.key == "CheckDate-End-Between_");

    //    if (checkDateStart != null && checkDateStart.value != "") {
    //        checkDateStart.value = (new Date(checkDateStart.value)).DateFormat("yyyy/MM/dd 00:00:00")
    //    }

    //    if (checkDateEnd != null && checkDateEnd.value != "") {
    //        checkDateEnd.value = (new Date(checkDateEnd.value)).DateFormat("yyyy/MM/dd 23:59:59")
    //    }

    //    callback();
    //}

    function InsertTotalAfterSorted() {
        var fd2 = new FormData();
        console.log(fd2);
        $.ajax({
            dataType: 'json',
            url: $.AppConfigOptions.baseurl + 'CarFuel_Closed/GetSumData',
            type: "post",
            //data: new FormData($('#adj')['0']),
            data: fd2,
            processData: false,
            contentType: false,
            success: function (data) {
                //alert("儲存成功"); 
                if (data[0] != '' && data[1] != '' && data[2] != '' && data[3] != '' && data[4] != '') {
                    var $ppp = $('#_table tbody');
                    var content = '<tr data-index="9999"> \
                           <td class="dou-field-CityName">總計：</td> \
                           <td class="dou-field-cpc" style="text-align: right; ">'+ data[0] + '</td> \
                           <td class="dou-field-cpc_closed" style="text-align: right; ">'+ data[1] + '</td> \
                           <td class="dou-field-notcpc" style="text-align: right; ">'+ data[2] + '</td> \
                           <td class="dou-field-notcpc_closed" style="text-align: right; ">'+ data[3] + '</td> \
                           <td class="dou-field-tv" style="text-align: right; ">'+ data[4] + '</td> \
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

    //多選
    //var $CITY = $('.filter-toolbar-plus').find("[data-fn=CITY]")
    //    .attr('multiple', true).selectpicker({
    //        actionsBox: true,
    //        selectAllText: '全選',
    //        deselectAllText: '取消已選',
    //        selectedTextFormat: 'count > 1',
    //        countSelectedText: function (sc, all) {
    //            return '縣市:挑' + sc + '個'
    //        }
    //    });
})

