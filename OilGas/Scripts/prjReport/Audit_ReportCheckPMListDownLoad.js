

$(document).ready(function () {

    //取消自動抓後端資料
    douoptions.tableOptions.url = undefined;
    $('#_table').hide();

    var a = {};
    a.item = '<span class="btn btn-success glyphicon glyphicon-download-alt"> 匯出Excel</span>';
    a.event = 'click .glyphicon-download-alt';
    a.callback = function ExportExcel(evt) {

        var conditions = GetFilterParams($_MasterTable)
        var paras;
        if (conditions.length > 0) {
            paras = { key: 'filter', value: JSON.stringify(conditions) };
        }

        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'Audit_ReportCheckPMListDownLoad/ExportAudit_ReportCheckPMListDownLoad',
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
    b.item = '<span class="btn btn-secondary glyphicon glyphicon-download-alt"> 重新計算查核等級</span>';
    b.event = 'click .glyphicon-download-alt';
    b.callback = function ExportExcel(evt) {
        var $element = $('body');
        helper.misc.showBusyIndicator($element, { timeout: 15 * 60 * 1000 });
        $.ajax({
            url: app.siteRoot + 'Audit_ReportCheckPMListDownLoad/ResetSend0',
            datatype: "json",
            type: "Get",
            success: function (data) {
                if (data.result) {
                    alert("「重新計算查核等級」已完成");
                    location.reload();
                } else {
                    alert("「重新計算查核等級」失敗：\n" + data.errorMessage);
                }
            },
            complete: function () {
                helper.misc.hideBusyIndicator($element);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
                helper.misc.hideBusyIndicator($element);
            }
        });
    };

    var c = {};
    c.item = '<span class="btn btn-secondary glyphicon glyphicon-download-alt"> 重新計算高風險因子</span>';
    c.event = 'click .glyphicon-download-alt';
    c.callback = function ExportExcel(evt) {
        var $element = $('body');
        helper.misc.showBusyIndicator($element, { timeout: 15 * 60 * 1000 });
        $.ajax({
            url: app.siteRoot + 'Audit_ReportCheckPMListDownLoad/ResetSend1',
            datatype: "json",
            type: "Get",
            success: function (data) {
                if (data.result) {
                    alert("「重新計算高風險因子」已完成");                    
                } else {
                    alert("「重新計算高風險因子」失敗：\n" + data.errorMessage);
                }
            },
            complete: function () {
                helper.misc.hideBusyIndicator($element);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
                helper.misc.hideBusyIndicator($element);
            }
        });
    };

    var d = {};
    d.item = '<span class="btn btn-secondary glyphicon glyphicon-download-alt"> 重新計算新設、變更及復業</span>';
    d.event = 'click .glyphicon-download-alt';
    d.callback = function ExportExcel(evt) {
        var $element = $('body');
        helper.misc.showBusyIndicator($element, { timeout: 15 * 60 * 1000 });
        $.ajax({
            url: app.siteRoot + 'Audit_ReportCheckPMListDownLoad/ResetSend2',
            datatype: "json",
            type: "Get",
            success: function (data) {
                if (data.result) {
                    alert("「重新計算新設、變更及復業」已完成");
                } else {
                    alert("「重新計算新設、變更及復業」失敗：\n" + data.errorMessage);
                }
            },
            complete: function () {
                helper.misc.hideBusyIndicator($element);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
                helper.misc.hideBusyIndicator($element);
            }
        });
    };

    douoptions.appendCustomToolbars = [a, b, c, d];

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table
    $(".btn-confirm").hide();
    //報表類型(必選)
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

    //多選
    var $CheckTypeT = $('.filter-toolbar-plus').find("[data-fn=CheckTypeT]")
        .attr('multiple', true).selectpicker({
            actionsBox: true,
            selectAllText: '全選',
            deselectAllText: '取消已選',
            selectedTextFormat: 'count > 1',
            countSelectedText: function (sc, all) {
                return '高風險:挑' + sc + '個'
            }
        });
})
