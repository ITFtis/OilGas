

$(document).ready(function () {

    //百分比(%)文字
    $(douoptions.fields).each(function () {
        var that = this;

        if (this.field == "CheckHiatusRate" || this.field == "ItemHiatusAllRate") {
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

        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'Audit_ReportMissingYear/ExportAudit_ReportMissingYear',
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

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table
    //報表類型(必選)    
    $('[data-fn="CaseType"] option[value=""]').remove();

    //舊系統103年度以上查詢
    $('[data-fn="WorkYear"] option').each(function () {
        var year = this.value;
        if (year != "" && year < 103) {
            $(this).remove();
        }
    });

    //報表類型連動下拉 - 新增/編輯
    $('[data-fn="CaseType"]').change(function () {                
        var CaseType = $('[data-fn="CaseType"]').val();

        var $ddl = $('[data-fn="CheckItemTitel"]');
        $ddl.empty();
        $ddl.trigger('change');

        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'Audit_ReportMissingYear/GetCheckItemList',
            datatype: "json",
            type: "POST",
            data: { caseType: CaseType },
            success: function (data) {
                if (data.result) {
                    //set
                    $ddl.append('<option value title="所有查核項目">選擇查核項目</option>');
                    if (data.itemList.length != 0) {
                        $.each(data.itemList, function (index, v) {
                            $ddl.append('<option value=' + v.CheckItemTitelSum + '>' + v.CheckItemTitel + '</option>');
                        });
                    }      
                } else {
                    alert("查核項目取得失敗：\n" + data.errorMessage);
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

    //第二層
    $('[data-fn="CheckItemTitel"]').change(function () {
        var CaseType = $('[data-fn="CaseType"]').val();
        var CheckItemTitelSum = $('[data-fn="CheckItemTitel"]').val();

        var $ddl = $('[data-fn="CheckItemDescNo"]');
        $ddl.empty();
        $ddl.trigger('change');

        if (CheckItemTitelSum == null || CheckItemTitelSum == "") {            
            $ddl.append('<option value title="所有查核細項">選擇查核細項</option>');
            return;
        }

        helper.misc.showBusyIndicator();
        $.ajax({
            url: app.siteRoot + 'Audit_ReportMissingYear/GetCheckItemDetail',
            datatype: "json",
            type: "POST",
            data: { caseType: CaseType, checkItemTitelSum: CheckItemTitelSum },
            success: function (data) {
                if (data.result) {
                    //set
                    $ddl.append('<option value title="所有查核細項">選擇查核細項</option>');
                    if (data.itemDetail.length != 0) {
                        $.each(data.itemDetail, function (index, v) {
                            $ddl.append('<option value=' + v.CheckItemDescNo + '>' + v.CheckItemDesc + '</option>');
                        });
                    }
                } else {
                    alert("查核細項取得失敗：\n" + data.errorMessage);
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
})