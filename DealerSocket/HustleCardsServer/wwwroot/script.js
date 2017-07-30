
function test(){
	document.write(document.getElementById("username").value);
}

document.getElementById("loginBtn").addEventListener("click", function printTest() {
	var jobValue = document.getElementsByName('username')[0].value;
	document.write(jobValue);
	});