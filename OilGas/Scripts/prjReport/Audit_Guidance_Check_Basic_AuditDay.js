

$(document).ready(function () {

    douoptions.afterCreateEditDataForm = function ($container, row) {

        var isAdd = JSON.stringify(row) == '{}';
        if (!isAdd) {
            $('[data-field="Gas_Name"] input').prop('disabled', true);
        }

        //(1)加油站編號 帶入 加油站名稱
        $('[data-fn="CaseNo"]').on('change', function () {
            var val = this.value;

            $opt = $('[data-fn="CaseNo"]').siblings().find('option[value="' + val + '"]');
            if ($opt.length > 0) {
                var gas_name = $opt.attr('data-gas_name');
                $('[data-fn="Gas_Name"]').val(gas_name);
            }
        });

        //(2)加油站名稱 帶入 加油站編號
        $('[data-fn="Gas_Name"]').on('change', function () {
            var val = this.value;

            $opt = $('[data-fn="Gas_Name"]').siblings().find('option[value="' + val + '"]');
            if ($opt.length > 0) {
                var caseno = $opt.attr('data-caseno');                
                $('[data-fn="CaseNo"]').val(caseno);
            }
        });

        //下拉選單Null或空值預設挑選(afterCreateEditDataForm)
        EditDataFormSelectDefault();
    }

    douoptions.fields.push({
        title: "下載",
        field: "DownloadExcel",
        formatter: function(v, r) {

            var text = '<button onclick="download(\'' + r.CaseNo + '\')"  >下載</button>';

            return (text);
        }
    });

    $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table
    
})

function download(CaseNo) {

    $.ajax({
        url: app.siteRoot + 'Audit_Guidance_Check_List_local/ExportExcel',
        datatype: "json",
        type: "POST",
        data: { CaseNo: CaseNo },
        success: function (data) {
            if (data != "false") {
                location.href = app.siteRoot + data;
            } else {
                alert("下載失敗：\n" + data.errorMessage);
            }
        },

    });

    helper.misc.showBusyIndicator();
    $.ajax({
        url: app.siteRoot + 'Audit_Guidance_Check_Basic_AuditDay/ExportExcel',
        datatype: "json",
        type: "POST",
        data: { CaseNo: CaseNo },
        success: function (data) {
            if (data.result) {
                //location.href = app.siteRoot + data.url;
                alert('ok Go');
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

}