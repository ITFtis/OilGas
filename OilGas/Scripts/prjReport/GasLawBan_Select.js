

$(document).ready(function () {

    //查詢
    $('#btnSearch').click(function () {
        //alert('aaa');
        //<span class="btn btn-confirm btn-secondary">查 詢</span>
        //$("#_table").DouEditableTable('refresh');
        $('.btn-confirm').trigger('click');
    })

    //匯出
    $('#btnExcel').click(function () {

        var conditions = GetFilterParams();
        if (conditions.length > 0) {
            var paras = { key: 'filter', value: JSON.stringify(conditions) };
        }

        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'GasLawBan_Select/ExportGasLawBan_Select',
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
    });

    douoptions.queryFilter = function (params, callback) {

        //清空，改用客製化param
        params.splice(0);

        var conditions = GetFilterParams();
        params = $.merge(params, conditions);

        callback();
    }

    var $_nowTable = $("#_table").DouEditableTable(douoptions); //初始dou table

    //隱藏dou預設功能
    $('.fixed-table-toolbar').hide();

    //初始化UI
    iniUI();
})

function GetFilterParams() {
    //params.push({ key: "a1", value: "a2" });
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
            conditions.push({ key: id, value: value });
        }
    });

    return conditions;
}

function iniUI() {
    //動態(fsource) 兼營設施
    $('#AllCityCode').click(function () {
        var checked = $(this).is(":checked");
        $('input[fsource=cityCode]').prop('checked', checked);
    })
}