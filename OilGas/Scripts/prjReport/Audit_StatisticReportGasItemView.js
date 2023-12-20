$(document).ready(function () {

    douoptions.queryFilter = function (params, callback) {
        var CityCode1 = params.find(a => a.key == "CityCode1");        
        CityCode1.value = CityCode1.value.join(',');

        //filter params特殊參數調整
        var CaseType = params.find(a => a.key == "CaseType");

        if (CaseType != null) {
            var fn = CaseType.key;
            var workTable = $('.filter-toolbar-plus').find('[data-fn="' + fn + '"] option:selected').attr('data-checkitemtable');
            CaseType.value = workTable;
        }

        //驗證
        if (!ToValidate(params)) {
            return;
        }

        callback();
    }

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
            url: app.siteRoot + 'Audit_StatisticReportGasItemView/ExportAudit_StatisticReportGasItemView',
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
    //報表類型(必選)    
    $('[data-fn="CaseType"] option[value=""]').remove();
    ////test
    //$('[data-fn="WorkYear"] option[value=""]').remove();

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
    var WorkYear = params.find(a => a.key == "WorkYear");

    if (WorkYear.value == "") {
        var text = $('[data-fn="WorkYear"] option:selected').text();
        alert(text);
        return false;
    }

    return true;
}