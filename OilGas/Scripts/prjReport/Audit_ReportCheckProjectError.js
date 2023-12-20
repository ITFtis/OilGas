

$(document).ready(function () {

    //取消自動抓後端資料
    douoptions.tableOptions.url = undefined;
    $('#_table').hide();

    var a = {};
    a.item = '<span class="btn btn-success glyphicon glyphicon-download-alt"> 匯出Excel</span>';
    a.event = 'click .glyphicon-download-alt';
    a.callback = function ExportExcel(evt) {

        var conditions = GetFilterParams($_MasterTable)

        //filter params特殊參數調整
        var CaseType = conditions.find(a => a.key == "CaseType");        
        if (CaseType != null) {
            if (CaseType.value == "CarFuel_BasicData") {
                var workTable = "Check_Item";
                CaseType.value = workTable;
            }
        }

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
            url: app.siteRoot + 'Audit_ReportCheckProjectError/ExportAudit_ReportCheckProjectError',
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
    //必填
    $('[data-fn="CaseType"] option[value=""]').remove();
    ////test
    //$('[data-fn="SYear"] option[value="98"]').attr("selected", "selected");
    //$('[data-fn="EYear"] option[value="98"]').attr("selected", "selected");
})

function ToValidate(params) {
    var CaseType = params.find(a => a.key == "CaseType");
    var SYear = params.find(a => a.key == "SYear");
    var EYear = params.find(a => a.key == "EYear");

    if (SYear.value == '') {
        var text = $('[data-fn="SYear"] option:selected').text();
        alert(text);
        return false;
    }
    if (EYear.value == '') {
        var text = $('[data-fn="EYear"] option:selected').text();
        alert(text);
        return false;
    }

    if (CaseType.value == "Check_Item") {
        if ((SYear.value <= 97) && (EYear.value > 97)) {
            alert("「汽/機車加油站」之查核區間，不能跨民國97至98年度。");
            return false;
        }
    }
    else if (CaseType.value == "Check_Item_Fish") {
        if ((SYear.value <= 103) && (EYear.value > 103)) {
            alert("「漁船加油站」之查核區間，不能跨民國103至104年度。");
            return false;
        }
    }

    return true;
}