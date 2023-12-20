$(document).ready(function () {

    var a = {};
    a.item = '<span class="btn btn-success glyphicon glyphicon-download-alt"> 匯出Excel</span>';
    a.event = 'click .glyphicon-download-alt';
    a.callback = function ExportExcel(evt) {
        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'CarFuel_CheckOilTypeByYear/ExportCarFuel_CheckOilTypeByYear',
            datatype: "json",
            type: "POST",
            //data: { paras: [paras] },
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
})


