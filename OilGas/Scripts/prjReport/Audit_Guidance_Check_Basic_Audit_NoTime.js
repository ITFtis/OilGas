function otherdisplayInput(file, otherfile, val) {

	let input = $('[data-fn="' + otherfile + '"]')

	if ($('[data-field="' + file + '"] input').val() != val) {
		$('[data-field="' + otherfile + '"]').addClass('d-none');
		input.attr('required', false);
		
	}
	else {
		$('[data-field="' + otherfile + '"]').removeClass('d-none');

		
		input.attr('required', true);
		input.attr('placeholder', '不可空值');
		input.attr('data-bs-validation-message', '不可空值');
	}



	$('[data-field="' + file + '"] input').change(function () {
		otherdisplayInput(file, otherfile, val)
	})
}

function download(CheckNum) {
	helper.misc.showBusyIndicator();
	$.ajax({
		url: app.siteRoot + 'Audit_Guidance_Check_Basic_NoTime/ExportExcel',
		datatype: "json",
		type: "POST",
		data: { CaseNo: CheckNum },
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
}

$(document).ready(function () {
	douoptions.afterCreateEditDataForm = function (e, row) {

		//營業主體選其他的話就會有輸入其他營業主體的選項輸入
		otherdisplayInput("Business_theme", "otherBusiness_theme", "其他")
		

	}

	
	

	douoptions.fields.push({
		title: "下載excel",
		filed: "DownloadExcel",
		formatter: function (v,r) {
			var text = '<button onclick="download(\'' + r.Check_Number + '\')"  >下載</button>';
			return text;
		}
	})



	$("#_table").DouEditableTable(douoptions);
})