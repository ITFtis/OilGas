

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

    //營運狀態
    $('#divUsageState1').find('[name="filterConditions"]').click(function () {
        var type = $(this).val();

        $('#ddl_UsageState option').hide();
        $('#ddl_UsageState option[value=""]').show();
        $('#ddl_UsageState').val('');
        $('#ddl_UsageState option[type="' + type + '"]').show();
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
            url: app.siteRoot + 'CarGas_BasicData_List/ExportCarGas_BasicData_List',
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
            url: app.siteRoot + 'CarGas_BasicData_List/ResetExport',
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

    //初始化UI
    iniUI();
})

function iniUI() {
    //動態(fsource) 附屬設施
    $('#AllAncillaryFacility').click(function () {
        ResetGroupColumns();
        $('input[fsource=ancillaryFacility]').prop('checked', true);
    })
    //動態(fsource) 兼營設施
    $('#AllFacility').click(function () {
        ResetGroupColumns();
        $('input[fsource=carVehicleGas_Facility]').prop('checked', true);
    })

    //靜態 保險資料
    $('#Insurance').click(function () {
        ResetGroupColumns();
        $('#Insurance').closest('tr').find('input[name=filterColumns]').prop('checked', true);
    })
    //靜態 土地資料
    $('#Land').click(function () {
        ResetGroupColumns();
        $('#Land').closest('tr').find('input[name=filterColumns]').prop('checked', true);
    })
    //靜態 設施狀況
    $('#AllIsland').click(function () {
        ResetGroupColumns();
        $('#AllIsland').closest('tr').find('input[name=filterColumns]').prop('checked', true);
    })
    //販售油品、油槽種類與容量
    $('#AllOilData').click(function () {
        ResetGroupColumns();
        $('#AllOilData').closest('tr').find('input[name=filterColumns]').prop('checked', true);
    })
    //營業主體
    $('#AllcarVehicleGas_BusinessOrganization').click(function () {
        $('#AllcarVehicleGas_BusinessOrganization').closest('tr').find('input[name=filterConditions]').prop('checked', true);
    })
    $('#Business_1').click(function () {
        $('#Business_1').closest('tr').find('input[name=filterConditions]').prop('checked', true);
        $('#carVehicleGas_BusinessOrganization_16').prop('checked', false);
    })
    $('#Business_16').click(function () {
        $('#Business_16').closest('tr').find('input[name=filterConditions]').prop('checked', false);
        $('#carVehicleGas_BusinessOrganization_16').prop('checked', true);
    })

    //群組radio
    //群組(g1)隱藏內部checkbox 預設
    $('[group=g1]').closest('tr').find('input[name=filterColumns]:first-child').closest('td').hide();
    $('[group=g1] input:radio').change(function () {
        $('[group=g1] input:radio').prop('checked', false);
        $(this).prop('checked', true);

        //群組(g1)隱藏內部checkbox
        $('[group=g1]').closest('tr').find('input[name=filterColumns]:first-child').closest('td').hide();
        $(this).closest('tr').find('input[name=filterColumns]:first-child').closest('td').show();
    })
    $('[group=g2] input:radio').change(function () {
        $('[group=g2] input:radio').prop('checked', false);
        $(this).prop('checked', true);
    })

    //加油機數
    $('#gun').change(function () {
        if ($(this).prop("checked")) {
            $('#one_gun').prop('checked', true);
            $('#two_gun').prop('checked', true);
            $('#four_gun').prop('checked', true);
            $('#six_gun').prop('checked', true);
            $('#eight_gun').prop('checked', true);

            $('#one_gun').prop('disabled', false);
            $('#two_gun').prop('disabled', false);
            $('#four_gun').prop('disabled', false);
            $('#six_gun').prop('disabled', false);
            $('#eight_gun').prop('disabled', false);
        }
        else {
            $('#one_gun').prop('checked', false);
            $('#two_gun').prop('checked', false);
            $('#four_gun').prop('checked', false);
            $('#six_gun').prop('checked', false);
            $('#eight_gun').prop('checked', false);

            $('#one_gun').prop('disabled', true);
            $('#two_gun').prop('disabled', true);
            $('#four_gun').prop('disabled', true);
            $('#six_gun').prop('disabled', true);
            $('#eight_gun').prop('disabled', true);
        }
    })  
}

//取消:欄位清單-群組勾選
function ResetGroupColumns() {

    //動態(fsource)
    $('input[fsource=ancillaryFacility]').prop('checked', false);
    $('input[fsource=carVehicleGas_Facility]').prop('checked', false);

    //靜態
    $('#Insurance').closest('tr').find('input[name=filterColumns]').prop('checked', false);
    $('#Land').closest('tr').find('input[name=filterColumns]').prop('checked', false);
    $('#AllIsland').closest('tr').find('input[name=filterColumns]').prop('checked', false);
    $('#AllOilData').closest('tr').find('input[name=filterColumns]').prop('checked', false);
}