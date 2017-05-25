$(document).ready(function() {
	$.getScript("../assets/plugins/backstretch/jquery.backstretch.min.js", function(){
		$(".fullscreen-static-image").backstretch([
	  "../assets/img/bg/child-girl-happy.jpg",
	  ], {duration: 8000, fade: 800});
	});
});