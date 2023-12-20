$(document).ready(function () {

    var ItemIndex = douHelper.getField(douoptions.fields, 'ItemIndex');
    ItemIndex.rowspan = 2;
    ItemIndex.colspan = 1;
    ItemIndex.valign = "middle";
    var CheckNo = douHelper.getField(douoptions.fields, 'CheckNo');
    CheckNo.rowspan = 2;
    CheckNo.colspan = 1;
    CheckNo.valign = "middle";
    var CheckDate = douHelper.getField(douoptions.fields, 'CheckDate');
    CheckDate.rowspan = 2;
    CheckDate.colspan = 1;
    CheckDate.valign = "middle";
    var CaseNo = douHelper.getField(douoptions.fields, 'CaseNo');
    CaseNo.rowspan = 2;
    CaseNo.colspan = 1;
    CaseNo.valign = "middle";
    douoptions.fields = [
        [
            //data
            ItemIndex, CheckNo, CheckDate, CaseNo,
            { "field": "", "title": "查核系統資料", align: "center", colspan: 3 },
            { "field": "", "title": "石油設施資料", align: "center", colspan: 3 },
        ],
        [         
            //data
            douHelper.getField(douoptions.fields, 'Check_Gas_Name'),
            douHelper.getField(douoptions.fields, 'Check_Business'),
            douHelper.getField(douoptions.fields, 'Check_Addr'),
            douHelper.getField(douoptions.fields, 'Case_Gas_Name'),
            douHelper.getField(douoptions.fields, 'Case_Business'),
            douHelper.getField(douoptions.fields, 'Case_Addr'),
        ],
    ]

    var a = {};
    a.item = '<span class="btn btn-success glyphicon glyphicon-download-alt"> 匯出Excel</span>';
    a.event = 'click .glyphicon-download-alt';
    a.callback = function ExportExcel(evt) {
        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'CarFuel_CarVehicleGas_CheckError1/ExportCarFuel_CarVehicleGas_CheckError1',
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


