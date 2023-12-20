

$(document).ready(function () {

    //取消自動抓後端資料
    douoptions.tableOptions.url = undefined;
    $('#_table').hide();    

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
            url: app.siteRoot + 'Audit_Check_counts/ExportAudit_Check_counts',
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
    $(".btn-confirm").hide();
    //報表類型(必選)
    $('[data-fn="CaseType"] option[value=""]').remove();
    $('[data-fn="CheckYear"] option[value=""]').remove();
})