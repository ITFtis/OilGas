

$(document).ready(function () {

    var aryCheck = [];
    douoptions.useMutiSelect = true;

    douoptions.tableOptions.onCheck = function (row, e) {

        var caseNo = row.CaseNo;
        if (aryCheck.indexOf(caseNo) == -1) {
            aryCheck.push(caseNo);
        }

        return false;
    };

    douoptions.tableOptions.onUncheck = function (row, e) {

        var caseNo = row.CaseNo;
        aryCheck = aryCheck.filter(a => a != caseNo);

        return false;
    };

    douoptions.tableOptions.onCheckAll = function (rows, b) {
        var aryDatas = rows.map(function (data) {
            return data.CaseNo;
        });

        //全選(merge 不重複)        
        aryCheck = aryCheck.concat(aryDatas.filter(ele => !aryCheck.includes(ele)));
    };

    douoptions.tableOptions.onUncheckAll = function (a, rows) {
        var aryDatas = rows.map(function (data) {
            return data.CaseNo;
        });

        //全不勾
        aryCheck = aryCheck.filter(item => !aryDatas.includes(item));
    };

    douoptions.tableOptions.onLoadSuccess = function (datas) {

        //選取數量
        var n_sels = 0;
        $('.bootstrap-table #_table tbody').find('.dou-field-CaseNo').each(function (index) {
            var caseNo = $(this).text();
            if (aryCheck.indexOf(caseNo) > -1) {
                n_sels++;

                var a = $(this).closest('tr').find('.bs-checkbox');
                //用trigger，不使用jquery保持資料正確性
                $('#_table').bootstrapTable('check', index);
            }
        });

        //若清單選取數量都勾,全選也勾
        if (n_sels > 0) {
            if (n_sels == datas.rows.length) {
                //$('input[name="btSelectAll"]').prop('checked', true);
                $('#_table').bootstrapTable('checkAll');
            }
        }

        return false;
    };

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table
})