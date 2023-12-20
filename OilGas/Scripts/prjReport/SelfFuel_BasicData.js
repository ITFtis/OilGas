

$(document).ready(function () {

    //縣市鄉鎮
    $('#ddl_City').change(function () {

        $("#ddl_Area").empty();
        $("#ddl_Area").append('<option value title="">選擇鄉鎮</option>');

        var cityCode = $('#ddl_City').val();
        $.ajax({
            url: app.siteRoot + 'basic/getAreaCode',
            datatype: "json",
            type: "Get",
            data: { GSLCode: cityCode },
            success: function (data) {
                if (data.result) {
                    if (data.roads.length != 0) {
                        $.each(data.roads, function (index, v) {
                            $("#ddl_Area").append('<option value=' + v.AreaCode1 + '>' + v.AreaName + '</option>');
                        });
                    }
                }
            },
            complete: function () {
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        });
    })

    //使用狀況
    $('#ddl_BigUsage').change(function () {

        $("#ddl_UsageState").empty();
        $("#ddl_UsageState").append('<option value title="">請選擇</option>');

        var kind = $('#ddl_BigUsage').val();
        $.ajax({
            url: app.siteRoot + 'basic/getDDLUsageStateDetail',
            datatype: "json",
            type: "Get",
            data: { kind: kind },
            success: function (data) {
                if (data.result) {
                    if (data.details.length != 0) {
                        $.each(data.details, function (index, v) {
                            $("#ddl_UsageState").append('<option value=' + v.Value + '>' + v.Key + '</option>');
                        });
                    }
                }
            },
            complete: function () {
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        });
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
            url: app.siteRoot + 'SelfFuel_BasicData/ExportSelfFuel_BasicData',
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
            url: app.siteRoot + 'SelfFuel_BasicData/ResetExport',
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
})
