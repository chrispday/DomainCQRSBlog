// Google Analytics
var _gaq = _gaq || [];
_gaq.push(['_setAccount', 'UA-44407792-1']);
_gaq.push(['_trackPageview']);
(function () {
	var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
	ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
	var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();

var generateGuid = function () {
	var guid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
		var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
		return v.toString(16);
	});
	return guid;
};

$.fn.serializeObject = function () {
	var o = {};
	var a = this.serializeArray();
	$.each(a, function () {
		if (o[this.name] !== undefined) {
			if (!o[this.name].push) {
				o[this.name] = [o[this.name]];
			}
			o[this.name].push(this.value || '');
		} else {
			o[this.name] = this.value || '';
		}
	});
	return o;
};

!function ($) { //ensure $ always references jQuery
	$(function () { //when dom has finished loading
		//make top text appear aligned to bottom: http://stackoverflow.com/questions/13841387/how-do-i-bottom-align-grid-elements-in-bootstrap-fluid-layout
		function fixHeader() {
			//for each element that is classed as 'pull-down'
			//reset margin-top for all pull down items
			$('.pull-down').each(function () {
				$(this).css('margin-top', 0);
			});

			//set its margin-top to the difference between its own height and the height of its parent
			$('.pull-down').each(function () {
				if ($(window).innerWidth() >= 768) {
					$(this).css('margin-top', $(this).parent().height() - $(this).height());
				}
			});
		}

		$(window).resize(function () {
			fixHeader();
		});

		fixHeader();
	});
}(window.jQuery);

var comments = {
	validateCommentForm: function() {
		return {
			//submitHandler: comments.addComment,
			submitHandler: function (form) { },
			errorClass: "text-danger",
			success: function (label, element) {
				$(element).parents("div.form-group:first").removeClass("has-error").addClass("has-success");
				$(label).remove();
			},
			highlight: function (element, errorClass, validClass) {
				$(element).parents("div.form-group:first").removeClass("has-success").addClass("has-error");
			},
			unhighlight: function (element, errorClass, validClass) {
				$(element).parents("div.form-group:first").removeClass("has-error").addClass("has-success");
			}
		};
	}
}
