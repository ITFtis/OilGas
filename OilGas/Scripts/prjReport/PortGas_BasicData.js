

$(document).ready(function () {

    //列管現況
    $('#ddl_portGas_CodeMaster').change(function () {
        var value = $(this).val();

        //因舊版資料存文字，非代碼。故下連動抓codeNo
        var codeNo = $('#ddl_portGas_CodeMaster option:selected').attr('codeNo');

        if (value == '') {
            //全部顯示
            $('#ddl_portGas_CodeDetail option').show();
        }
        else {
            $('#ddl_portGas_CodeDetail option').hide();

            $('#ddl_portGas_CodeDetail option[value=""]').show();            
            $('#ddl_portGas_CodeDetail option[PortGasCode_Type="' + codeNo + '"]').show();
        }

        //Master change，Detail reset ''
        $('#ddl_portGas_CodeDetail').val('');
    })

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

        //欄位
        var columns = [];
        $('input[name="filterColumns"]:checked').each(function () {
            //fsource表示從DB或其它地方，動態生出
            if ($(this).attr('fsource')) {
                //動態多選
                columns.push({ 'Id': $(this).attr('fsource'), 'Value': $(this).val() });
                var aa = '123';
            }
            else {
                //靜態多選
                columns.push({ 'Id': this.id });
            }
        });

        var objs = { 'conditions': conditions, 'columns': columns };

        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'PortGas_BasicData/ExportPortGas_BasicData',
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

    $('#btnRest').click(function () {
        location.reload();
    })

    $('#btnRenewCache').click(function () {
        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'PortGas_BasicData/ResetExport',
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