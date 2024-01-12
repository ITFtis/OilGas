$(document).ready(function () {

	dou_options.tableOptions.pageSize = 20;



	dou_options.appendCustomToolbars = [{
		item: '<span class="btn btn-success" title="Excel下載">Excel下載</span>', event: 'click',
		callback: function (e) {

			helper.misc.showBusyIndicator();
			$.ajax({
				url: app.siteRoot + 'WS_GSM/ExportExcel',
				datatype: "json",
				type: "POST",

				success: function (data) {
					if (data.result) {
						location.href = app.siteRoot + data.url;
						
					} else {
						alert("下載失敗：\n" + data.errorMessage);
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
	}];

	$("#_table").DouEditableTable(dou_options);
});