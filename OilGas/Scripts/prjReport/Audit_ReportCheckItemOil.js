

$(document).ready(function () {

    var $_MasterTable;

    douoptions.tableOptions.onLoadSuccess = function (datas) {

    //    ////if (datas.data.length > 0) {
    //    ////    //查核細項(A01)
    //    ////    var CheckItemDescNo = $('[data-fn="CheckItemDescNo"]').val();
    //    ////}

    //    ////查核細項(A01)
    //    //var CheckItemDescNo = $('[data-fn="CheckItemDescNo"]').val();
        //    //$('[data-field="Ch1"]').closest("th").hide();

        //var _filed = douHelper.getField($_MasterTable.instance.settings.fields, 'Ch1');

        //顯示columns數量
        var num = $('[data-fn="CheckItemDescNo"] option').length - 1;

        //(1)table th 名稱
        $('[data-field="Ch1"]').closest('th').text(num >= 1 ? $('[data-fn="CheckItemDescNo"] option').get(1).text : '');
        $('[data-field="Ch2"]').closest('th').text(num >= 2 ? $('[data-fn="CheckItemDescNo"] option').get(2).text : '');
        $('[data-field="Ch3"]').closest('th').text(num >= 3 ? $('[data-fn="CheckItemDescNo"] option').get(3).text : '');
        $('[data-field="Ch4"]').closest('th').text(num >= 4 ? $('[data-fn="CheckItemDescNo"] option').get(4).text : '');
        $('[data-field="Ch5"]').closest('th').text(num >= 5 ? $('[data-fn="CheckItemDescNo"] option').get(5).text : '');
        $('[data-field="Ch6"]').closest('th').text(num >= 6 ? $('[data-fn="CheckItemDescNo"] option').get(6).text : '');
        $('[data-field="Ch7"]').closest('th').text(num >= 7 ? $('[data-fn="CheckItemDescNo"] option').get(7).text : '');
        $('[data-field="Ch8"]').closest('th').text(num >= 8 ? $('[data-fn="CheckItemDescNo"] option').get(8).text : '');
        $('[data-field="Ch9"]').closest('th').text(num >= 9 ? $('[data-fn="CheckItemDescNo"] option').get(9).text : '');
        $('[data-field="Ch10"]').closest('th').text(num >= 10 ? $('[data-fn="CheckItemDescNo"] option').get(10).text : '');
        $('[data-field="Ch11"]').closest('th').text(num >= 11 ? $('[data-fn="CheckItemDescNo"] option').get(11).text : '');
        $('[data-field="Ch12"]').closest('th').text(num >= 12 ? $('[data-fn="CheckItemDescNo"] option').get(12).text : '');
        $('[data-field="Ch13"]').closest('th').text(num >= 13 ? $('[data-fn="CheckItemDescNo"] option').get(13).text : '');
        $('[data-field="Ch14"]').closest('th').text(num >= 14 ? $('[data-fn="CheckItemDescNo"] option').get(14).text : '');
        $('[data-field="Ch15"]').closest('th').text(num >= 15 ? $('[data-fn="CheckItemDescNo"] option').get(15).text : '');
        $('[data-field="Ch16"]').closest('th').text(num >= 16 ? $('[data-fn="CheckItemDescNo"] option').get(16).text : '');
    }

    douoptions.queryFilter = function (params, callback) {

        //查核細項(A01)
        var CheckItemDescNo = $('[data-fn="CheckItemDescNo"]').val();

        //$("#_table").bootstrapTable('showColumn', column) showColumn hideColumn
        //顯示columns數量
        var num = $('[data-fn="CheckItemDescNo"] option').length - 1;

        //table column 顯示
        var descNo = $('[data-fn="CheckItemDescNo"]').val();
        $("#_table").bootstrapTable((num >= 1 ? "showColumn" : "hideColumn"), 'Ch1');
        $("#_table").bootstrapTable((num >= 2 ? "showColumn" : "hideColumn"), 'Ch2');
        $("#_table").bootstrapTable((num >= 3 ? "showColumn" : "hideColumn"), 'Ch3');
        $("#_table").bootstrapTable((num >= 4 ? "showColumn" : "hideColumn"), 'Ch4');
        $("#_table").bootstrapTable((num >= 5 ? "showColumn" : "hideColumn"), 'Ch5');
        $("#_table").bootstrapTable((num >= 6 ? "showColumn" : "hideColumn"), 'Ch6');
        $("#_table").bootstrapTable((num >= 7 ? "showColumn" : "hideColumn"), 'Ch7');
        $("#_table").bootstrapTable((num >= 8 ? "showColumn" : "hideColumn"), 'Ch8');
        $("#_table").bootstrapTable((num >= 9 ? "showColumn" : "hideColumn"), 'Ch9');
        $("#_table").bootstrapTable((num >= 10 ? "showColumn" : "hideColumn"), 'Ch10');
        $("#_table").bootstrapTable((num >= 11 ? "showColumn" : "hideColumn"), 'Ch11');
        $("#_table").bootstrapTable((num >= 12 ? "showColumn" : "hideColumn"), 'Ch12');
        $("#_table").bootstrapTable((num >= 13 ? "showColumn" : "hideColumn"), 'Ch13');
        $("#_table").bootstrapTable((num >= 14 ? "showColumn" : "hideColumn"), 'Ch14');
        $("#_table").bootstrapTable((num >= 15 ? "showColumn" : "hideColumn"), 'Ch15');
        $("#_table").bootstrapTable((num >= 16 ? "showColumn" : "hideColumn"), 'Ch16');

        callback();
    }

    $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table
    //必填
    $('[data-fn="CheckItemTitel"] option[value=""]').remove();
})