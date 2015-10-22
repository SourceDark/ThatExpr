function enterButtonOnClick() {
	var idStr = document.getElementById('idInput').value;
	if (idStr.length > 0) {
		window.location.href = "/s/" + idStr + "/discover";
	}
}