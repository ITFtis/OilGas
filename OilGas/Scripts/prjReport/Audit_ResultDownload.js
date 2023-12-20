

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
        var Business_theme = conditions.find(a => a.key == "Business_theme");
        if (CaseType != null) {
            var fn = CaseType.key;
            var workTable = $('.filter-toolbar-plus').find('[data-fn="' + fn + '"] option:selected').attr('data-checkitemtable');
            CaseType.value = workTable;
        }
        if (Business_theme != null) {
            Business_theme.value = Business_theme.value.split('_')[0];
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
            url: app.siteRoot + 'Audit_ResultDownload/ExportAudit_ResultDownload',
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

    //多選
    var $CityCode1 = $('.filter-toolbar-plus').find("[data-fn=CityCode1]")
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


function ToValidate(params) {
    var CaseType = params.find(a => a.key == "CaseType");
    var CheckDate_Start_Between_ = params.find(a => a.key == "CheckDate-Start-Between_");
    var CheckDate_End_Between_ = params.find(a => a.key == "CheckDate-End-Between_");

    ////var sYear = new Date(CheckDate_Start_Between_.value).getFullYear();
    ////var eYear = new Date(CheckDate_End_Between_.value).getFullYear();
    ////(97-98)跨年度限制
    var sYear = CheckDate_Start_Between_.value == "" ? 1900 : new Date(CheckDate_Start_Between_.value).getFullYear();
    var eYear = CheckDate_End_Between_.value == "" ? 2999 : new Date(CheckDate_End_Between_.value).getFullYear();
    if (CaseType.value == "Check_Item") {
        if ((sYear <= 1998) && (eYear > 1998)) {
            alert("「汽/機車加油站」之查核區間，不能跨民國97至98年度。");
            return false;
        }
    }
    else if (CaseType.value == "Check_Item_Fish") {
        if ((sYear <= 2014) && (eYear > 2014)) {
            alert("「漁船加油站」之查核區間，不能跨民國103至104年度。");
            return false;
        }
    }

    return true;
}