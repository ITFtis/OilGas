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
            url: app.siteRoot + 'Audit_CounselingReportRate1/ExportAudit_CounselingReportRate1',
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
        //getChart();
    }

    var b = {};
    b.item = '<span class="btn btn-secondary glyphicon glyphicon-download-alt"> 重新計算</span>';
    b.event = 'click .glyphicon-download-alt';
    b.callback = function ExportExcel(evt) {
        var $element = $('body');
        helper.misc.showBusyIndicator($element, { timeout: 15 * 60 * 1000 });
        $.ajax({
            url: app.siteRoot + 'Audit_CounselingReportRate1/Recalculate',
            datatype: "json",
            type: "Get",
            success: function (data) {
                if (data.result) {
                    alert("「重新計算」已完成");
                    location.reload();
                } else {
                    alert("「重新計算」失敗：\n" + data.errorMessage);
                }
            },
            complete: function () {
                helper.misc.hideBusyIndicator($element);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
                helper.misc.hideBusyIndicator($element);
            }
        });
    };

    //匯出功能
    douoptions.appendCustomToolbars = [a, b];

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table
})

