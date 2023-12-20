

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
            url: app.siteRoot + 'Audit_StatisticReportTroughView/ExportAudit_StatisticReportTroughView',
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

        //filter日期(date)
        $('[data-fn="CheckDate-Start-Between_"] input').attr('type', 'date');
        $('[data-fn="CheckDate-End-Between_"] input').attr('type', 'date');
    }

    douoptions.queryFilter = function (params, callback) {

        var CITY = params.find(a => a.key == "CITY");
        //CITY.value = JSON.stringify(CITY.value);
        CITY.value = CITY.value.join(',');

        //params取資料yyyy/MM/dd 00:00:00
        var checkDateStart = params.find(a => a.key == "CheckDate-Start-Between_");
        var checkDateEnd = params.find(a => a.key == "CheckDate-End-Between_");

        if (checkDateStart != null && checkDateStart.value != "") {
            checkDateStart.value = (new Date(checkDateStart.value)).DateFormat("yyyy/MM/dd 00:00:00")
        }

        if (checkDateEnd != null && checkDateEnd.value != "") {
            checkDateEnd.value = (new Date(checkDateEnd.value)).DateFormat("yyyy/MM/dd 23:59:59")
        }

        callback();
    }

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table
    $('[data-fn="CaseType"] option[value=""]').remove();

    //多選
    var $CITY = $('.filter-toolbar-plus').find("[data-fn=CITY]")
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

