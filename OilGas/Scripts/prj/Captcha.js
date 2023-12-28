$(document).ready(function () {
  function RefreshCaptcha() {
    $.ajax({
      url: "/Home/RefreshCaptcha",
      method: "GET",
      success: function (data) {
        let src = data.src;
        let vcode = data.verificationCode;
        $("#CaptchaImg").attr("src", src);
        $("#captchaCode").val(vcode);
      },
      error: function () {
        console.log("some error...");
      },
    });
  }

  //刷新驗證碼圖檔
  $("#refreshCapt").on("click", function (e) {
    e.preventDefault();
    RefreshCaptcha();
  });

  //驗證碼通過再送表單
  //有需要驗證再打開這支功能
  // $("#submit").on("click", function (e) {
  //   let inputVcode = $("#vcode").val();
  //   let captchaCode = $("#captchaCode").val();

  //   //trim後如果是空白就會是false
  //   if (!inputVcode.trim()) {
  //     e.preventDefault();
  //     alert("請輸入驗證碼!");
  //   } else if (inputVcode !== captchaCode) {
  //     RefreshCaptcha();
  //     e.preventDefault();
  //     alert("驗證碼輸入錯誤");
  //   }
  // });
});
