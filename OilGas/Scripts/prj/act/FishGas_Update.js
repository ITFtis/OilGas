﻿
function download(v) {
    helper.misc.showBusyIndicator();
    $.ajax({
        url: app.siteRoot + 'FishGas_Update/ExportExcel',
        datatype: "json",
        type: "Post",
        data: { "CaseNo": v },
        success: function (data) {
            if (data.result) {
                console.log(data.result)
                location.href = app.siteRoot + data.url
            } else {
                alert(data.errorMessage)
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
}

$(document).ready(function () {

    //UpdateForm
    SetUpdateForm();

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

    $(douoptions.fields).each(function () {
        if (this.field == "CaseNo") {
            this.formatter = function (v, d) {
                var text = '<a href="' + app.siteRoot + 'FishGas_Select/Index?ID=' + d.ID + '">' + d.CaseNo + '</a>' + '<button onclick="download(\'' + v + '\')"  class="btn btn-primary float-end">證照套印</button>';
                return text;
            }
        }
    })

    //var _filed = douHelper.getField(douoptions.fields, "CaseNo");
    //_filed.formatter = function (v, d) {
    //    var text = '<a>' + d.CaseNo + '</a>';
    //};

    //douoptions.fields.push({
    //    title: "下載",
    //    field: "DownloadExcel",
    //    formatter: function (v, r) {

    //        var text = '<button onclick="download(\'' + r.CaseNo + '\')"  >下載</button>';

    //        return (text);
    //    }
    //});

    var $_MasterTable = $("#_table").DouEditableTable(douoptions); //初始dou table

    function SetUpdateForm() {
        $.getJSON($.AppConfigOptions.baseurl + 'FishGas_Update/getUpdateForm', function (_opt) { //取model option

            _opt.title = '批次變更表單';

            _opt.afterCreateEditDataForm = function ($container, row) {
                //多選                
                var cbl_CopyUnit = $('.modal-dialog').find("[data-fn=cbl_CopyUnit]")
                    .attr('multiple', true).selectpicker({
                        noneSelectedText: '請挑選副本單位',
                        actionsBox: true,
                        selectAllText: '全選',
                        deselectAllText: '取消已選',
                        selectedTextFormat: 'count > 1',
                        countSelectedText: function (sc, all) {
                            return '副本單位:挑' + sc + '個'
                        }
                    });

                cbl_CopyUnit.selectpicker('deselectAll');
            }

            //實體Dou js                                
            var $_dt1 = $('#_UpdateForm').douTable(_opt);

            //讓filename變成上傳的input
            $('div[data-field="FileName"] input').attr('type', 'file');
            $('div[data-field="FileName"] input').attr('accept', '.pdf,.jpg,.bmp');

            //隱藏div(button 確定、取消)            
            $('.modal-dialog').css('min-height', '');
            $('.modal-dialog .modal-footer').addClass('justify-content-center');

            $('.modal-dialog .modal-footer button').hide();
            var back = '<button id="btnBatch" class="btn btn-primary">批次變更修改資料</button>';
            $(back).appendTo($('.modal-dialog .modal-footer'));

            //批次變更修改資料
            $('#btnBatch').click(function () {

                //表單驗證(dou)
                var current = $_dt1.instance;
                if (!ValidateFrom(current))
                    return;

                var CaseNos = aryCheck;
                if (CaseNos.length == 0) {
                    alert('請勾選需變更負責人之案件');
                    return;
                }

                var obj = {};
                obj.txt_BossName = $('.modal-dialog').find('[data-fn="txt_BossName"]').val();
                obj.txt_ID_No = $('.modal-dialog').find('[data-fn="txt_ID_No"]').val();
                obj.ZipCode2 = $('.modal-dialog').find('[data-fn="ZipCode2"]').val();
                obj.Address2 = $('.modal-dialog').find('[data-fn="Address2"]').val();
                obj.txt_Boss_Tel = $('.modal-dialog').find('[data-fn="txt_Boss_Tel"]').val();
                obj.txt_Boss_Email = $('.modal-dialog').find('[data-fn="txt_Boss_Email"]').val();
                obj.txt_Dispatch_date = $('.modal-dialog').find('[data-fn="txt_Dispatch_date"] input').val();
                obj.ddl_selectLicenseNo = $('.modal-dialog').find('[data-fn="ddl_selectLicenseNo"]').val();
                obj.txt_Dispatch_No = $('.modal-dialog').find('[data-fn="txt_Dispatch_No"]').val();
                obj.txt_Shouwen_Units = $('.modal-dialog').find('[data-fn="txt_Shouwen_Units"]').val();
                obj.cbl_CopyUnit = $('.modal-dialog').find('[data-fn="cbl_CopyUnit"]').val().join(';');
                obj.txt_OtherCopyUnit = $('.modal-dialog').find('[data-fn="txt_OtherCopyUnit"]').val();

                var fileInput = $('.modal-dialog').find('[data-fn="FileName"]');
                obj.fileName = fileInput[0].files[0] == undefined ? "" : fileInput[0].files[0].name;

                //return;

                helper.misc.showBusyIndicator();
                $.ajax({
                    url: app.siteRoot + 'FishGas_Update/BatchSave',
                    datatype: "json",
                    type: "Post",
                    data: { "CaseNos": CaseNos, "obj": obj },
                    success: function (data) {
                        if (data.result) {
                            alert("批次變更負責人已完成");

                            aryCheck = [];
                            $('.bootstrap-table.fishgas_Updatecontroller').find('.btn-confirm').trigger('click');

                        } else {
                            alert("批次變更負責人失敗：\n" + data.errorMessage);
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

                //上傳檔案
                var nullfuntion = function () { };
                upload("FileName", $.AppConfigOptions.baseurl + 'FishGas_Update/Sendupload', obj.txt_ID_No, CaseNos, nullfuntion);
            });
        });


    }
});