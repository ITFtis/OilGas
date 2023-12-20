function GetDefaultColors() {
    var colors = [
        "#ff0000", "#ffff00", "#00ff00", "#00ffff", "#0000ff",
        "#ff00ff", "#808080", "#008000", "#800000", "#000080",
        "#808000", "#800080", "#c0c0c0", "#008080", "#7b68ee",
        "#1e90ff", "#f08080", "#ffe4c4", "#b8860b", "#00ced1",
        "#556b2f", "#4b0082", "#d8bfd8", 
    ];

    return colors;
}

function GetFilterParams($_MasterTable) {
    var _fielter = [];
    var current = this;
    $.each($('.filter-toolbar-plus').find("[data-fn]"), function () {
        var _fn = $(this).attr("data-fn");
        var _filed = douHelper.getField($_MasterTable.instance.settings.fields, _fn);
        var obj = _filed.editFormtter.getValue.call(_filed, $(this));
        _fielter.push({ key: _fn, value: $.isArray(obj) ? obj.join(',') : obj.split(',').join(',') });
    });

    return _fielter;
}

//colspan固定為1
//datas資料, field 合併的欄位名稱
function MergeTableRows($container, datas, field) {
    //$container.bootstrapTable('mergeCells', { index: 0, field: 'CheckItemTitel', colspan: 1, rowspan: 2 });

    if (datas.length > 0) {
        var aryMergeCells = [];
        var sRow;        //開始列
        var ptext;       //前1個
        var rMerge = 0;  //rowspan
        $.each(datas, function (index, value) {            
            var text = this[field];
            if (ptext == null) {
                //default
                sRow = index;
                rMerge = 1;
                ptext = text;
            }
            else if (ptext != text) {

                aryMergeCells.push({ index: sRow, field: field, colspan: 1, rowspan: rMerge });

                //reset
                sRow = index;
                rMerge = 1;
                ptext = text;
            }
            else {
                //合併rowspan
                rMerge++;
            }
        });
        //最後
        aryMergeCells.push({ index: sRow, field: field, colspan: 1, rowspan: rMerge });

        $.each(aryMergeCells, function (index, value) {
            $container.bootstrapTable('mergeCells', this);
        })
    }
}