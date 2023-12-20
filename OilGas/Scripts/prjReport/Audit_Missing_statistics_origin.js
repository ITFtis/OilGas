

$(document).ready(function () {

    var a = {};
    a.item = '<span class="btn btn-success glyphicon glyphicon-download-alt"> 匯出Excel</span>';
    a.event = 'click .glyphicon-download-alt';
    a.callback = function ExportExcel(evt) {

        var conditions = GetFilterParams($_MasterTable)

        //filter params特殊參數調整
        var CaseType = conditions.find(a => a.key == "CaseType");        
        if (CaseType != null) {
            var fn = CaseType.key;
            var workTable = $('.filter-toolbar-plus').find('[data-fn="' + fn + '"] option:selected').attr('data-checkitemtable');
            CaseType.value = workTable;
        }

        var paras;
        if (conditions.length > 0) {
            paras = { key: 'filter', value: JSON.stringify(conditions) };
        }

        //驗證
        if (!ToValidate(conditions)) {
            return;
        }

        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'Audit_Missing_statistics_origin/ExportAudit_Missing_statistics_origin',
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
    douoptions.queryFilter = function (params, callback) {
        //filter params特殊參數調整
        var CaseType = params.find(a => a.key == "CaseType");

        if (CaseType != null) {
            var fn = CaseType.key;
            var workTable = $('.filter-toolbar-plus').find('[data-fn="' + fn + '"] option:selected').attr('data-checkitemtable');
            CaseType.value = workTable;
        }

        //驗證
        if (!ToValidate(params)) {
            return;
        }

        callback();
    }

    douoptions.tableOptions.onPreBody = function (params) {
        var obj = params.find(obj => obj.CheckItemTitel == "合計");        
        if (obj != null) {
            var index = params.indexOf(obj);
            params.push(params.splice(index, 1)[0]);
        }

        return false;
    }

    douoptions.tableOptions.onLoadSuccess = function (datas) {
        //總計
        if (datas.length > 0) {
            var nrow = jQuery.extend(true, {}, datas[0]);

            var sumCheckItemCount = 0;
            var sumCheckItemErrCount = 0;
            $.each(datas, function (index, value) {
                sumCheckItemCount += this.CheckItemCount;
                sumCheckItemErrCount += this.CheckItemErrCount;
            });

            nrow.CheckItemTitel = "合計";
            nrow.CheckItemCount = sumCheckItemCount;
            nrow.CheckItemErrCount = sumCheckItemErrCount;
            datas.push(nrow);
            $_MasterTable.DouEditableTable("tableReload", datas);
        }
    }

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table   
    //報表類型(必選)
    $('[data-fn="CaseType"] option[value=""]').remove();
    ////test
    //$('[data-fn="WorkYear"] option[value=""]').remove();

    $('[data-fn="CaseType"]').change(function () {
        var sYear = 0;
        var conditions = GetFilterParams($_MasterTable);
        var CaseType = conditions.find(a => a.key == "CaseType");
        if (CaseType != null) {
            var fn = CaseType.key;
            var workTable = $('.filter-toolbar-plus').find('[data-fn="' + fn + '"] option:selected').attr('data-checkitemtable');
            if (workTable == "Check_Item") {
                sYear = 0;
            }
            else {
                sYear = 103;
            }
        }

        if (sYear != 0) {
            $('.filter-toolbar-plus').find("[data-fn=WorkYear] option").each(function (i) {
                var year = $(this).val();
                if (year < sYear)
                    $(this).hide();
            });
            //預設年度
            $('.filter-toolbar-plus').find("[data-fn=WorkYear]").val(sYear);
        }
        else {
            $('.filter-toolbar-plus').find("[data-fn=WorkYear] option").show();
            $('.filter-toolbar-plus').find("[data-fn=WorkYear]").prop('selectedIndex', 0);
        }
    });
})

function ToValidate(params) {
    var WorkYear = params.find(a => a.key == "WorkYear");

    if (WorkYear.value == "") {
        var text = $('[data-fn="WorkYear"] option:selected').text();
        alert(text);
        return false;
    }

    return true;
}