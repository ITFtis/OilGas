document.getElementById("refreshCapt").addEventListener("click", () => {
	fetch("RefreshCaptcha")
		.then(response => response.json())
		.then(imgPath => {
			console.log(imgPath)
			document.getElementById("CaptchaImg").src = imgPath
		})
		.catch(error => {
			console.log(error);
		});
});