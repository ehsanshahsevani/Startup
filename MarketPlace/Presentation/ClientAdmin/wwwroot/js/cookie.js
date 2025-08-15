function SetCookie(cname, cvalue, exdays) {

	const d = new Date();
	d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
	let expires = "expires=" + d.toUTCString();
	document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";

}

//getCookie("nameofthevalue") function helps to retrieve the value of a predefined cookie using JavaScript. 
function GetCookie(name) {

	var cname = name + "=";
	var decodedCookie = decodeURIComponent(document.cookie);
	var ca = decodedCookie.split(';');

	for (var i = 0; i < ca.length; i++) {

		var c = ca[i];
		while (c.charAt(0) == ' ') {

			c = c.substring(1);

		}

		if (c.indexOf(cname) == 0) {

			return c.substring(cname.length, c.length);

		}

	}

	return "";

}

//deleteCookie("nameofthecookie") function helps to remove cookie using JavaScript. 
function DeleteCookie(name) {

	var d = new Date();

	d.setTime(d.getTime() - (60 * 60 * 1000));

	var expires = "expires=" + d.toGMTString();
	document.cookie = name + "=;" + expires + ";path=/";

}