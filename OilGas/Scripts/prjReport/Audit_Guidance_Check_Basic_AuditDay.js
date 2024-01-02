

$(document).ready(function () {

    douoptions.afterCreateEditDataForm = function ($container, row) {

        var isAdd = JSON.stringify(row) == '{}';
        if (!isAdd) {
            $('[data-field="Gas_Name"] input').prop('disabled', true);
        }

        //(1)加油站編號 帶入 加油站名稱
        $('[data-fn="CaseNo"]').on('change', function () {
            var val = this.value;

            $opt = $('[data-fn="CaseNo"]').siblings().find('option[value="' + val + '"]');
            if ($opt.length > 0) {
                var gas_name = $opt.attr('data-gas_name');
                $('[data-fn="Gas_Name"]').val(gas_name);
            }
        });

        //(2)加油站名稱 帶入 加油站編號
        $('[data-fn="Gas_Name"]').on('change', function () {
            var val = this.value;

            $opt = $('[data-fn="Gas_Name"]').siblings().find('option[value="' + val + '"]');
            if ($opt.length > 0) {
                var caseno = $opt.attr('data-caseno');                
                $('[data-fn="CaseNo"]').val(caseno);
            }
        });
    }

    $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table
    
})