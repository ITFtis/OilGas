$(document).ready(function () {

    var CaseIndex = douHelper.getField(douoptions.fields, 'CaseIndex');
    CaseIndex.rowspan = 2;
    CaseIndex.colspan = 1;
    CaseIndex.valign = "middle";
    var CaseNo = douHelper.getField(douoptions.fields, 'CaseNo');
    CaseNo.rowspan = 2;
    CaseNo.colspan = 1;
    CaseNo.valign = "middle";
    var Gas_Name = douHelper.getField(douoptions.fields, 'Gas_Name');
    Gas_Name.rowspan = 2;
    Gas_Name.colspan = 1;
    Gas_Name.valign = "middle";
    var Business_theme = douHelper.getField(douoptions.fields, 'Business_theme');
    Business_theme.rowspan = 2;
    Business_theme.colspan = 1;
    Business_theme.valign = "middle";
    douoptions.fields = [
        [
            //data
            CaseIndex, CaseNo, Gas_Name, Business_theme,
            { "field": "", "title": "石油設施地址", align: "center", colspan: 2 },
            { "field": "", "title": "最後發文日期", align: "center", colspan: 2 },
        ],
        [         
            //data
            douHelper.getField(douoptions.fields, 'Mod_date'),
            douHelper.getField(douoptions.fields, 'UsageStateName'),
            douHelper.getField(douoptions.fields, 'Last_Dispatch_date'),
            douHelper.getField(douoptions.fields, 'Last_DispatchName'),
        ],
    ]

    var a = {};
    a.item = '<span class="btn btn-success glyphicon glyphicon-download-alt"> 匯出Excel</span>';
    a.event = 'click .glyphicon-download-alt';
    a.callback = function ExportExcel(evt) {
        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'CarFuel_CaseError1/ExportCarFuel_CaseError1',
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


