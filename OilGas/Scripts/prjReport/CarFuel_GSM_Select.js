$(document).ready(function () {

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
            url: app.siteRoot + 'CarFuel_GSM_Select/ExportCarFuel_GSM_SelectView',
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

    //douoptions.appendCustomToolbars = [a];
    //取消自動抓後端資料
    //douoptions.tableOptions.url = undefined;
    douoptions.tableOptions.onLoadSuccess = function (datas) {      
        //filter日期(date)
        //$('[data-fn="CheckDate-Start-Between_"] input').attr('type', 'date');
        //$('[data-fn="CheckDate-End-Between_"] input').attr('type', 'date');
    }

    douoptions.queryFilter = function (params, callback) {

        var CaseType = params.find(a => a.key == "CaseType");
        CaseType.value = CaseType.value.join(',');

        //alert("1");
        callback();
    }

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table

    //多選
    var CaseType = $('.filter-toolbar-plus').find("[data-fn=CaseType]")
        .attr('multiple', true).selectpicker({
            actionsBox: true,
            selectAllText: '全選',
            deselectAllText: '取消已選',
            selectedTextFormat: 'count > 1',
            countSelectedText: function (sc, all) {
                return '列管項目:挑' + sc + '個'
            }
        });

    var ss = $(".form-group col-auto").find('span').css("visibility", "hidden");

})


