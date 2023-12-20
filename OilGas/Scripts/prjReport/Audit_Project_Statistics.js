

$(document).ready(function () {

    //百分比(%)文字
    $(douoptions.fields).each(function () {
        var that = this;

        if (this.field == "CheckItemPercentage") {
            this.formatter = function (v, d) {
                return v + '%';
            }
        }
    });

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

        var CheckItemTableType = params.find(a => a.key == "CheckItemTableType");
        CheckItemTableType.value = CheckItemTableType.value.join(',');

        callback();
    }

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
            url: app.siteRoot + 'Audit_Project_Statistics/ExportAudit_Project_Statistics',
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

    douoptions.tableOptions.onLoadSuccess = function (datas) {

        if (datas.length > 0) {
            //dou 清單(資料列) => 合併儲存格
            var $container = $("#_table");
            MergeTableRows($container, datas, 'CheckItemTitel');
        }
    }

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table
    //報表類型(必選)
    $('[data-fn="CaseType"] option[value=""]').remove();
    ////Test
    //$('[data-fn="WorkYear"] option[value=""]').remove();

    $('[data-fn="WorkYear"]').change(function () {
        //檢查設備
        SetUICheckItemTableType();
    })

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
                //保留請選擇
                if (year != "" && year < sYear)
                    $(this).hide();
            });
            //預設年度
            $('.filter-toolbar-plus').find("[data-fn=WorkYear]").val(sYear);
        }
        else {
            $('.filter-toolbar-plus').find("[data-fn=WorkYear] option").show();
            $('.filter-toolbar-plus').find("[data-fn=WorkYear]").prop('selectedIndex', 0);
        }

        //檢查設備
        SetUICheckItemTableType();
    });

    $('[data-fn="WorkYear"]').change(function () {
        //檢查設備
        SetUICheckItemTableType();
    })
})

function SetUICheckItemTableType() {

    var fn1 = "CaseType";
    var CaseType = $('.filter-toolbar-plus').find('[data-fn="' + fn1 + '"] option:selected').attr('data-checkitemtable');

    var fn2 = "WorkYear";
    var WorkYear = $('.filter-toolbar-plus').find('[data-fn="' + fn2 + '"]').val();
    
    $('.filter-toolbar-plus').find("[data-fn=CheckItemTableType]").empty();    
    $('.filter-toolbar-plus').find("[data-fn=CheckItemTableType]").append($('<option>', {
        value: "",
        text: "請選擇檢查設備"
    }));

    $.ajax({
        url: app.siteRoot + 'Audit_Project_Statistics/GetDDLCheckItemTableType',
        datatype: "json",
        type: "Get",
        data: { CaseType: CaseType, WorkYear: WorkYear },
        success: function (datas) {
            if (datas.result) {
                $.each(datas.types, function (i, item) {
                    $('.filter-toolbar-plus').find("[data-fn=CheckItemTableType]").append($('<option>', {
                        value: item.v,
                        text: item.s
                    }));
                });

                //多選                
                var $CheckItemTableType = $('.filter-toolbar-plus').find("[data-fn=CheckItemTableType]")
                    .attr('multiple', true).selectpicker({
                        actionsBox: true,
                        selectAllText: '全選',
                        deselectAllText: '取消已選',
                        selectedTextFormat: 'count > 1',
                        countSelectedText: function (sc, all) {
                            return '設備:挑' + sc + '個'
                        }
                    });
                $CheckItemTableType.selectpicker('refresh');

            } else {
                alert("查詢失敗：\n" + data.errorMessage);
            }
        },
        complete: function () {
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }
    });
}

function ToValidate(params) {
    var WorkYear = params.find(a => a.key == "WorkYear");

    if (WorkYear.value == "") {
        var text = $('[data-fn="WorkYear"] option:selected').text();
        alert(text);
        return false;
    }

    return true;
}