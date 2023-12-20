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
            url: app.siteRoot + 'CarFuel_LandData/ExportCarFuelLandDataView',
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

    douoptions.appendCustomToolbars = [a];
    //取消自動抓後端資料
    douoptions.tableOptions.url = undefined;
    douoptions.tableOptions.onLoadSuccess = function (datas) {      
        //filter日期(date)
        //$('[data-fn="CheckDate-Start-Between_"] input').attr('type', 'date');
        //$('[data-fn="CheckDate-End-Between_"] input').attr('type', 'date');
    }

    douoptions.queryFilter = function (params, callback) {

        var LandUsageZoneCode0 = params.find(a => a.key == "LandUsageZoneCode0");
        //CITY.value = JSON.stringify(CITY.value);
        LandUsageZoneCode0.value = LandUsageZoneCode0.value.join(',');

        var LandUsageZoneCode1 = params.find(a => a.key == "LandUsageZoneCode1");
        LandUsageZoneCode1.value = LandUsageZoneCode1.value.join(',');

        var LandClassCode = params.find(a => a.key == "LandClassCode");
        LandClassCode.value = LandClassCode.value.join(',');

        //params取資料yyyy/MM/dd 00:00:00
        //var checkDateStart = params.find(a => a.key == "CheckDate-Start-Between_");
        //var checkDateEnd = params.find(a => a.key == "CheckDate-End-Between_");

        //if (checkDateStart != null && checkDateStart.value != "") {
        //    checkDateStart.value = (new Date(checkDateStart.value)).DateFormat("yyyy/MM/dd 00:00:00")
        //}

        //if (checkDateEnd != null && checkDateEnd.value != "") {
        //    checkDateEnd.value = (new Date(checkDateEnd.value)).DateFormat("yyyy/MM/dd 23:59:59")
        //}
        //alert("1");
        //callback();
    }

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table
    $(".btn-confirm").hide();

    //多選
    var LandUsageZoneCode0 = $('.filter-toolbar-plus').find("[data-fn=LandUsageZoneCode0]")
        .attr('multiple', true).selectpicker({
            actionsBox: true,
            selectAllText: '全選',
            deselectAllText: '取消已選',
            selectedTextFormat: 'count > 1',
            countSelectedText: function (sc, all) {
                return '都市計畫區:挑' + sc + '個'
            }
        });
    var LandUsageZoneCode0 = $('.filter-toolbar-plus').find("[data-fn=LandUsageZoneCode1]")
        .attr('multiple', true).selectpicker({
            actionsBox: true,
            selectAllText: '全選',
            deselectAllText: '取消已選',
            selectedTextFormat: 'count > 1',
            countSelectedText: function (sc, all) {
                return '土地使用分區:挑' + sc + '個'
            }
        });
    var LandClassCode = $('.filter-toolbar-plus').find("[data-fn=LandClassCode]")
        .attr('multiple', true).selectpicker({
            actionsBox: true,
            selectAllText: '全選',
            deselectAllText: '取消已選',
            selectedTextFormat: 'count > 1',
            countSelectedText: function (sc, all) {
                return '用地類別:挑' + sc + '個'
            }
        });

    var ss = $(".form-group col-auto").find('span').css("visibility", "hidden");

})


