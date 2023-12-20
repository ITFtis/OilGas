

$(document).ready(function () {
    
    var field_ItemName = douHelper.getField(douoptions.fields, 'ItemName');
    field_ItemName.rowspan = 2;
    field_ItemName.colspan = 1;
    field_ItemName.valign = "middle";

    douoptions.fields = [
        [   
            //filter(4 cols)
            { "field": "", "title": "", align: "center", colspan: 4 },

            //data            
            field_ItemName,
            { "field": "", "title": "缺失家數(家)", align: "center", colspan: 2 },
            { "field": "", "title": "查核家數(家)", align: "center", colspan: 2 },
            { "field": "", "title": "缺失百分比(%)", align: "center", colspan: 2 }
        ],
        [
            //filter(4 cols)
            douHelper.getField(douoptions.fields, 'CaseType'),
            douHelper.getField(douoptions.fields, 'CheckDate-Start-Between_'),
            douHelper.getField(douoptions.fields, 'CheckDate-End-Between_'),
            douHelper.getField(douoptions.fields, 'Business_theme'),

            //data
            douHelper.getField(douoptions.fields, 'ItemHiatusCountByBusi'),
            douHelper.getField(douoptions.fields, 'ItemHiatusCountByYear'),
            douHelper.getField(douoptions.fields, 'ItemHiatusCheckByBusi'),
            douHelper.getField(douoptions.fields, 'ItemHiatusCheckByYear'),
            douHelper.getField(douoptions.fields, 'ItemHiatusPercentByBusi'),
            douHelper.getField(douoptions.fields, 'ItemHiatusPercentByYear'),
        ],        
    ]

    douoptions.queryFilter = function (params, callback) {
        //filter params特殊參數調整
        var CaseType = params.find(a => a.key == "CaseType");
        var Business_theme = params.find(a => a.key == "Business_theme");        

        if (CaseType != null) {
            var fn = CaseType.key;
            var workTable = $('.filter-toolbar-plus').find('[data-fn="' + fn + '"] option:selected').attr('data-checkitemtable');            
            CaseType.value = workTable;
        }
        if (Business_theme != null) {
            Business_theme.value = Business_theme.value.split('_')[0];
        }

        //驗證
        if (!ToValidate(params)) {
            return;
        }        

        callback();
    }

    //百分比(%)文字
    $(douoptions.fields).each(function () {
        var that = this;

        //fileds n維處理
        $(that).each(function () {
            if (this.field == "ItemHiatusPercentByBusi" || this.field == "ItemHiatusPercentByYear") {
                this.formatter = function (v, d) {
                    return v + '%';
                }
            }
        });
    });

    douoptions.tableOptions.onLoadSuccess = function (datas) {
        ////header 公司名稱
        var fn = "Business_theme";        
        var Business_themeS = $('.filter-toolbar-plus').find('[data-fn="' + fn + '"] option:selected').text();        
        $('[data-field="ItemHiatusCountByBusi"] .th-inner').html(Business_themeS);        
        $('[data-field="ItemHiatusCheckByBusi"] .th-inner').html(Business_themeS);        
        $('[data-field="ItemHiatusPercentByBusi"] .th-inner').html(Business_themeS);                
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
            url: app.siteRoot + 'Audit_StatisticReportItemView/ExportAudit_StatisticReportItemView',
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
    $('[data-fn="Business_theme"] option[value=""]').remove();

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

