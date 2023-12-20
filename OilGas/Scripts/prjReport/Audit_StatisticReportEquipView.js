

$(document).ready(function () {

    douoptions.tableOptions.onLoadSuccess = function (datas) {
        //var MapName = douHelper.getField($_MasterTable.instance.settings.fields, "MapName");
        if ($('[data-fn="RKind"]').val() == 1) {
            $('[data-field="MapName"] .th-inner').text('縣市別');
        }
        else if ($('[data-fn="RKind"]').val() == 2) {
            $('[data-field="MapName"] .th-inner').text('營業主體');
        }

        //合計(_tableCount)
        ListCount($_MasterTable);
    }

    var a = {};
    a.item = '<span class="btn btn-success glyphicon glyphicon-download-alt"> 匯出Excel</span>';
    a.event = 'click .glyphicon-download-alt';
    a.callback = function ExportExcel(evt) {

        var conditions = GetFilterParams($_MasterTable)
        if (conditions.length > 0) {
            var paras = { key: 'filter', value: JSON.stringify(conditions) };
        }

        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'Audit_StatisticReportEquipView/ExportAudit_StatisticReportEquipView',
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

    var b = {};
    b.item = '<span class="btn btn-success glyphicon glyphicon-download-alt"> 匯出Excel(合計)</span>';
    b.event = 'click .glyphicon-download-alt';
    b.callback = function ExportExcel(evt) {

        var conditions = GetFilterParams($_MasterTable)
        if (conditions.length > 0) {
            var paras = { key: 'filter', value: JSON.stringify(conditions) };
        }

        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'Audit_StatisticReportEquipView/ExportCountAudit_StatisticReportEquipView',
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

    douoptions.appendCustomToolbars = [a, b];

    douoptions.queryFilter = function (params, callback) {

        var CityCode1 = params.find(a => a.key == "CityCode1");
        CityCode1.value = CityCode1.value.join(',');

        var StrYear = params.find(a => a.key == "StrYear");
        StrYear.value = StrYear.value.join(',');

        var Business_theme = params.find(a => a.key == "Business_theme");
        Business_theme.value = Business_theme.value.join(',');

        callback();
    }

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table   
    //報表類型(必選)
    $('[data-fn="RKind"] option[value=""]').remove();
    $('[data-fn="CaseType"] option[value=""]').remove();    

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

    var $StrYear = $('.filter-toolbar-plus').find("[data-fn=StrYear]")
        .attr('multiple', true).selectpicker({
            actionsBox: true,
            selectAllText: '全選',
            deselectAllText: '取消已選',
            selectedTextFormat: 'count > 1',
            countSelectedText: function (sc, all) {
                return '年度:挑' + sc + '個'
            }
        });

    var $Business_theme = $('.filter-toolbar-plus').find("[data-fn=Business_theme]")
        .attr('multiple', true).selectpicker({
            actionsBox: true,
            selectAllText: '全選',
            deselectAllText: '取消已選',
            selectedTextFormat: 'count > 1',
            countSelectedText: function (sc, all) {
                return '營業主體:挑' + sc + '個'
            }
        });

    UIFilter();
    $CityCode1.selectpicker('deselectAll').selectpicker('val', '');
    $('[data-fn="RKind"]').change(function () {
        $CityCode1.selectpicker('deselectAll').selectpicker('val', '');
        $Business_theme.selectpicker('deselectAll').selectpicker('val', '');
        UIFilter();
    });
})

function UIFilter() {
    if ($('[data-fn="RKind"]').val() == 1) {
        $('[data-fn="CityCode1"]').closest('.filter-continer').show();
        $('[data-fn="Business_theme"]').closest('.filter-continer').hide();
    }
    else if ($('[data-fn="RKind"]').val() == 2) {
        $('[data-fn="CityCode1"]').closest('.filter-continer').hide();
        $('[data-fn="Business_theme"]').closest('.filter-continer').show();
    }
}

function ListCount($table) {
    //$("#_tableCount").DouEditableTable(douoptions); //初始dou table

    var paras;
    if ($('.no-records-found').is(':visible')) {
        //進入頁面不顯示清單(未使用查詢) 或 清單無資料
        paras = {};
    }
    else {
        var conditions = GetFilterParams($table)
        if (conditions.length > 0) {
            paras = { key: 'filter', value: JSON.stringify(conditions) };
        }
    }

    helper.misc.showBusyIndicator();
    $.ajax({
        url: app.siteRoot + 'Audit_StatisticReportEquipView/CountAudit_StatisticReportEquipView',
        datatype: "json",
        type: "POST",
        data: { paras: [paras] },
        success: function (_opt) {
            $('#_tableCount').douTable('destroy');
            if (_opt != "") {
                $('#_tableCount').douTable(_opt);
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