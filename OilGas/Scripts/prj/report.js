$(document).ready(function () {
    //數字輸入驗證
    $('input[type=number]').keydown(function (e) {
        //console.log(e.keyCode);
        // Allow: backspace, delete, tab, escape and enter
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || (e.keyCode > 57 && e.keyCode != 189 && e.keyCode != 190 && e.keyCode != 109 && e.keyCode != 110)))
            && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
        // 如果使用者輸入-，先判斷現在的值有沒有-，如果有，就不允許輸入
        if ((e.keyCode == 109 || e.keyCode == 189) && /-/g.test(this.value)) {
            e.preventDefault();
        }
        // 如果使用者輸入.，先判斷現在的值有沒有.，如果有，就不允許輸入
        if ((e.keyCode == 110 || e.keyCode == 190) && /\./g.test(this.value)) {
            e.preventDefault();
        }
    });
});
