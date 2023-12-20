//下載的網址
//$downloadurl = 'http://localhost//down//'

$downloadurl = 'https://pj.ftis.org.tw/down/'

//移除隱藏選單的超連結
$(document).ready(function () {
    if ($('.data-manager-menu-map a').text().includes(('隱藏選單'))) {
        $('.data-manager-menu-map a').removeAttr("href");
        $('.data-manager-menu-map a').text($('.data-manager-menu-map a').text().replace('隱藏選單/', ''));
    }
});


//只能讀
function readonly() {

    $('input').prop('readonly', true);
    $('select').prop('disabled', true);
    $('textarea').prop('readonly', true);
    $('input[type="checkbox"]').on('click', function (event) {
        event.preventDefault();
    })
    $('input[type="radio"]').on('click', function (event) {
        event.preventDefault();
    })
    $('input[type="file"]').on('click', function (event) {
        event.preventDefault();
    })



}





//URL的參數
function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue.split("#")[0];//防止URL最後出現#error11111
    }
}


//只能打數字
function typeint(paras) {
    $(paras).bind("keyup change", function () {
        $(this).val($(this).val().replace(/\D/g, ''));
    });
}



//radio 全選改變所有沒有選的單選(EX：I00是3改變I01~I08變3)
//paras是I01這種要改的
//Allparas是I00這種改全部的值
function changeradio(paras, Allparas) {
    if ($('div[data-fn="' + paras+'"] input[type="radio"]:checked').val() == undefined) {
        if (Allparas == '1') {
            $('div[data-fn="' + paras +'"] input[type="radio"][value="0"]').prop('checked', true);
        }
        else if (Allparas == '3') {
            $('div[data-fn="' + paras +'"] input[type="radio"][value="3"]').prop('checked', true);
        }
    }
}

//將表格轉置(行列互換)(目前還沒做到完美換，只能給某些頁面用)
function changetable() {



    var $originalTable = $('#_table');
    var $newTable = $(' <table id="_table" style="text-align: center;width:50vw;  margin: 0 auto; " class="table table-bordered table-hover"><tbody></tbody></table>').addClass($originalTable.attr('class'));

    // 在新表格的開頭添加“項目”和“個數”行
    var headerRow = $('<tr></tr>');
    headerRow.append('<th>項目</th>');
    headerRow.append('<th>個數</th>');
    $newTable.append(headerRow);



    $originalTable.find('tr').each(function (rowIdx, row) {
        $(row).find('td, th').each(function (colIdx, cell) {
            var $cell = $(cell);
            if ($newTable.find('tr').length <= colIdx + 1) { // 因為已經加了一行，所以要加1
                $newTable.append('<tr></tr>');
            }
            var clonedCell = $cell.clone(); // 複製cell，包括其class
            $newTable.find('tr').eq(colIdx + 1).append(clonedCell); // 同樣，這裡也要加1
        });
    });

    $originalTable.after($newTable);
    $('#_table').hide();
}

//變更登打格子位置
//變數前面是要改的格子，最後一個是最後再加的文字
function changetypeposition(...parts) {

    let elements = {};  // 使用一個物件來存儲動態生成的變數
    let colWidth = 12 /( parts.length-1); // 根據 parts 的數量動態計算列寬度，確保欄位不會太長或太短


    parts.forEach((part, index) => {

        if (index == 0) {
            elements['$part' + index] = $('[data-field="' + part + '"]');//將該格子放入變數物件(當成變數)

            elements['$part' + index].addClass('row');//設定成同一行


            // 創建一個新的標籤並插入到第一個元素前
            $('<div class="form-group field-container"><label class="col-sm-2 control-label">' + elements['$part' + index].find('label').text() + '</label></div>').insertBefore(elements['$part' + index]);



            //改變元素class讓他同一行
            elements['$part' + index].removeClass('col-md-12').addClass('col-md-' + (colWidth-1));
            elements['$part' + index].find('label').remove()//第一個元素不要有標籤，因為塞到最上面了
            elements['$part' + index].find('div').removeClass('col-sm-12').addClass('col-sm-12');

        }
        else if (index < parts.length - 2) {

            elements['$part' + index] = $('[data-field="' + part + '"]');//將該格子放入變數物件(當成變數)

            elements['$part' + index].addClass('row');//設定成同一行

            //改變元素class讓他同一行
            elements['$part' + index].removeClass('col-md-12').addClass('col-md-' + colWidth);
            elements['$part' + index].find('label').removeClass('col-sm-12').addClass('col-sm-3');
            elements['$part' + index].find('div').removeClass('col-sm-12').addClass('col-sm-9');

        }
        else if (index == parts.length - 2) {

            elements['$part' + index] = $('[data-field="' + part + '"]');//將該格子放入變數物件(當成變數)

            elements['$part' + index].addClass('row');//設定成同一行

            //改變元素class讓他同一行(為了後面多塞文字，所以設定跟前面不同)
            elements['$part' + index].removeClass('col-md-12').addClass('col-md-' + colWidth);
            elements['$part' + index].find('label').removeClass('col-sm-12').addClass('col-sm-2');
            elements['$part' + index].find('div').removeClass('col-sm-12').addClass('col-sm-8');

        }
        else if (index == parts.length - 1) {
            //最後一個變數塞進後面當文字
            elements['$part' + (index - 1)].append('<label class="col-sm-2 control-label">' + part + '</label>')

        }

    });


    var $row = $('<div class="row col-6"></div>').insertBefore(elements['$part1']);//將全部包再row裡面，變成一行


    parts.forEach((part, index) => {
        // 將所有元素移到 row div 內
        $row.append(elements['$part' + index]);
    });

}







//讓發文歷程可在表格下載文件
function changeFile_nameField(File_name, downloadurlpart) {
    douHelper.getField(dou_options, File_name).formatter = function (v, r) {

        if (v == null) {
            var text = "-"
        }
        else {
            var text = '<a  href="' + $downloadurl + downloadurlpart + v + '">' + v + '</a>';
        }

        return (text);
    }

}



//上傳檔案
function upload(fieldname, url, ID, CaseNo, callback) {

    if ($('div[data-field="' + fieldname + '"] input')[0].files != null)//是否可上傳
    {
        if ($('div[data-field="' + fieldname + '"] input')[0].files.length != 0)//是否有檔案
        {
            var FD = new FormData();
            FD.append('file', $('div[data-field="' + fieldname + '"] input')[0].files[0]);
            FD.append('ID', ID);
            FD.append('CaseNo', CaseNo);

            $.ajax({
                url: url,
                type: "POST",
                data: FD,
                processData: false,
                contentType: false,
                success: function (data) {
                    if (data != "false") {
                        callback();

                    } else { alert("檔案與其他人重複"); }
                },
            }).fail(function () {
                alert("檔案上傳失敗");

            });

        }
        else {
            callback();
        }

    }
    else {
        callback();
    }


}










//使有文件的可以看到文件
function uploadonlytext(fieldname, path, reupload = true, ishttp = false) {
    var time = new Date()//確保ID不重複
    var id = "linkherf" + time.getTime();
    var reuploadid = "reupload" + time.getTime();
    if (ishttp) {
        //有些是直接寫連結網址
        if ($('div[data-field="' + fieldname + '"] input').val().indexOf("http") >= 0) {
            $('div[data-field="' + fieldname + '"] input').before('<a id=' + id + ' href="' + $('div[data-field="' + fieldname + '"] input').val()  + '">' + $('div[data-field="' + fieldname + '"] input').val() + '</a>');

        }
        else {
            $('div[data-field="' + fieldname + '"] input').before('<a id=' + id + ' href="' + $downloadurl + path + $('div[data-field="' + fieldname + '"] input').val() + '">' + $('div[data-field="' + fieldname + '"] input').val() + '</a>');

        }
    }
    else {
        $('div[data-field="' + fieldname + '"] input').before('<a id=' + id + ' href="' + $downloadurl + path + $('div[data-field="' + fieldname + '"] input').val() + '">' + $('div[data-field="' + fieldname + '"] input').val() + '</a>');
    }
    $('div[data-field="' + fieldname + '"] input').addClass('d-none');
    //是否可以重新上傳
    if (reupload) {
        $('div[data-field="' + fieldname + '"] input').after('<button type="button" id="' + reuploadid + '" onclick="showupload(\'' + fieldname + '\',\'' + id + '\',\'' + reuploadid +'\')" class="btn btn-default reupload">重新上傳</button>');
    }

}



//使有文件的上傳按鈕重新可上傳文件的樣子//id是下載的那個<a>的id
function showupload(fieldname, id, reuploadid)
{
    $('div[data-field="' + fieldname + '"] input').attr('type', 'file');
    $('div[data-field="' + fieldname + '"] input').attr('accept', '.txt,.doc,.pdf,.jpg,.png,docx');
    $('div[data-field="' + fieldname + '"] input').removeClass('d-none');
    $('#' + id).remove();
    $('#' + reuploadid).remove();
}
































//選項選其他欄位要顯示或不顯示
function otherdisplay(file,otherfile,val) {
    if ($('[data-field="' + file + '"] select').val() != val) {
        $('[data-field="' + otherfile +'"]').addClass('d-none')
    }
    else {
        $('[data-field="' + otherfile +'"]').removeClass('d-none')
    }



    $('[data-field="' + file + '"] select').change(function () {
        otherdisplay(file, otherfile, val)
    })
}




//選項換多選
function multiple(row, file, iscomma = false) {
    var filedata = row[file];

    $('[data-field="'+file+'"] input[type="radio"]').each(function () {

        //刪除選項無
        if ($(this).val() === "") {
            $(this).closest('label').remove();
        }

        //修改radio成checkbox，讓他可以多選
        $(this).attr('type', 'checkbox');
    });



    //把值填進選項中
    if (filedata) {
        var valuesToSelect = filedata.split(";");//分割取得有選到的value
        //可能用,選
        if (iscomma) {
            valuesToSelect = filedata.split(",");//分割取得有選到的value
        }
    
    //先取消選取
    $('[data-field="' + file +'"] input[type="checkbox"]').prop('checked', false);


    //選擇該value的checkbox
    for (var i = 0; i < valuesToSelect.length; i++) {
        //選取要選的
        $('[data-field="' + file +'"] input[type="checkbox"][value="' + valuesToSelect[i] + '"]').prop('checked', true);
    }
   }

}


//多選回存的文字
function multiplesave(file) {

    var result = [];

    //有選的value塞進result
    $('[data-field="' + file +'"] input[type="checkbox"]:checked').each(function () {
        result.push($(this).val());
    });



    return result;
}


//隱藏UsageState不需要看的欄位
function UsageStatednone() {

    $('div[name="form"] [data-field="AgreeDate"]').addClass('d-none');
    $('div[name="form"] [data-field="Build_Deadline"]').addClass('d-none');
    $('div[name="form"] [data-field="ExtensionCount1"]').addClass('d-none');
    $('div[name="form"] [data-field="ExtensionDateStart1"]').addClass('d-none');
    $('div[name="form"] [data-field="ExtensionDateEnd1"]').addClass('d-none');
    $('div[name="form"] [data-field="ExtensionCount2"]').addClass('d-none');
    $('div[name="form"] [data-field="ExtensionDateStart2"]').addClass('d-none');
    $('div[name="form"] [data-field="ExtensionDateEnd2"]').addClass('d-none');
    $('div[name="form"] [data-field="ExtensionCount3"]').addClass('d-none');
    $('div[name="form"] [data-field="ExtensionDateStart3"]').addClass('d-none');
    $('div[name="form"] [data-field="ExtensionDateEnd3"]').addClass('d-none');
    $('div[name="form"] [data-field="ExtensionCount4"]').addClass('d-none');
    $('div[name="form"] [data-field="ExtensionDateStart4"]').addClass('d-none');
    $('div[name="form"] [data-field="ExtensionDateEnd4"]').addClass('d-none');
    $('div[name="form"] [data-field="ExtensionCount5"]').addClass('d-none');
    $('div[name="form"] [data-field="ExtensionDateStart5"]').addClass('d-none');
    $('div[name="form"] [data-field="ExtensionDateEnd5"]').addClass('d-none');
    $('div[name="form"] [data-field="BuildDate"]').addClass('d-none');
    $('div[name="form"] [data-field="OperationDate"]').addClass('d-none');
    $('div[name="form"] [data-field="ForeclosureDate"]').addClass('d-none');
    $('div[name="form"] [data-field="ClosedDate"]').addClass('d-none');
    $('div[name="form"] [data-field="StopDate"]').addClass('d-none');
    $('div[name="form"] [data-field="UsageStateSub"]').addClass('d-none');

}


//顯示UsageState需要看得欄位
function UsageStateremovednone() {



    switch ($('[data-field="UsageState2"] select').val()) {
        case "2": //已開業
            UsageStatednone();
            $('div[name="form"] [data-field="OperationDate"]').removeClass('d-none');



            break;
    }

    switch ($('[data-field="UsageState3"] select').val()) {
        case "1": //同意認定

            UsageStatednone();

            $('div[name="form"] [data-field="AgreeDate"]').removeClass('d-none');


            break;
        case "2": //同意籌建

            UsageStatednone();
            $('div[name="form"] [data-field="Build_Deadline"]').removeClass('d-none');




            break;
        case "7": //申請開業展延

            UsageStatednone();
            $('div[name="form"] [data-field="ExtensionCount2"]').removeClass('d-none');
            $('div[name="form"] [data-field="ExtensionDateStart2"]').removeClass('d-none');
            $('div[name="form"] [data-field="ExtensionDateEnd2"]').removeClass('d-none');


            break;
        case "8": //營業中變更
        case "10": //暫停營業中變更

            UsageStatednone();
            $('div[name="form"] [data-field="UsageStateSub"]').removeClass('d-none');


            break;
    }

    switch ($('[data-field="UsageState4"] select').val()) {
        case "2": //同意籌建

            UsageStatednone();
            $('div[name="form"] [data-field="Build_Deadline"]').removeClass('d-none');




            break;
        case "9": //申請核發經營許可執照展延
        case "26": //申請暫停營業展延
        case "40": //申請復業展延
        case "44": //申請同意籌建展延

            UsageStatednone();

            $('div[name="form"] [data-field="ExtensionCount2"]').removeClass('d-none');
            $('div[name="form"] [data-field="ExtensionDateStart2"]').removeClass('d-none');
            $('div[name="form"] [data-field="ExtensionDateEnd2"]').removeClass('d-none');




            break;
        case "27": //同意暫停營業

            UsageStatednone();

            $('div[name="form"] [data-field="ClosedDate"]').removeClass('d-none');



            break;
        case "36": //同意歇業


            UsageStatednone();
            //前系統好像不給填
            // $('div[name="form"] [data-field="StopDate"]').removeClass('d-none');


            break;
    }
    switch ($('[data-field="UsageState5"] select').val()) {
        case "20": //申請核發經營許可執照展延

            UsageStatednone();

            $('div[name="form"] [data-field="ExtensionCount2"]').removeClass('d-none');
            $('div[name="form"] [data-field="ExtensionDateStart2"]').removeClass('d-none');
            $('div[name="form"] [data-field="ExtensionDateEnd2"]').removeClass('d-none');



            break;
    }
    switch ($('[data-field="UsageState6"] select').val()) {
        case "2": //法拍
            UsageStatednone();
            $('div[name="form"] [data-field="ForeclosureDate"]').removeClass('d-none');

            break;
    }




}









