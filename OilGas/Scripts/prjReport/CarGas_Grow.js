$(document).ready(function () {
    //匯出
    $('#btnExportBasicExcel').click(function () {

        //條件
        var conditions = [];
        $('[name="filterConditions"]').each(function () {            

            //需要勾選項目:type:checkbox,radio()
            var types = ['checkbox', 'radio'];
            if (types.indexOf(this.type) > -1 && !this.checked) {
                return; // 等於continue
            }

            var id = '';
            if ($(this).attr('fsource')) {
                //動態多選
                id = $(this).attr('fsource');
            }
            else {
                id = this.id;
            }

            value = $(this).val();
            if (value) {
                conditions.push({ 'Id': id, 'Value': value });
            }
        });

        var objs = { 'conditions': conditions};

        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'CarGas_Grow/ExportCarGas_Grow',
            datatype: "json",
            type: "POST",
            data: { objs: objs },
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
    });
    //重設
    $('#btnRest').click(function () {
        location.reload();
    })
    //清除暫存
    $('#btnRenewCache').click(function () {
        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'CarGas_Grow/ResetExport',
            datatype: "json",
            type: "Get",            
            success: function (data) {
                if (data.result) {
                    alert("資料更新(Cache)成功");
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
    })
});


