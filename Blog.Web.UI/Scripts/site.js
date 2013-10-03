///#source 1 1 /Scripts/site/md5.js
function md5cycle(x, k) {
	var a = x[0], b = x[1], c = x[2], d = x[3];

	a = ff(a, b, c, d, k[0], 7, -680876936);
	d = ff(d, a, b, c, k[1], 12, -389564586);
	c = ff(c, d, a, b, k[2], 17, 606105819);
	b = ff(b, c, d, a, k[3], 22, -1044525330);
	a = ff(a, b, c, d, k[4], 7, -176418897);
	d = ff(d, a, b, c, k[5], 12, 1200080426);
	c = ff(c, d, a, b, k[6], 17, -1473231341);
	b = ff(b, c, d, a, k[7], 22, -45705983);
	a = ff(a, b, c, d, k[8], 7, 1770035416);
	d = ff(d, a, b, c, k[9], 12, -1958414417);
	c = ff(c, d, a, b, k[10], 17, -42063);
	b = ff(b, c, d, a, k[11], 22, -1990404162);
	a = ff(a, b, c, d, k[12], 7, 1804603682);
	d = ff(d, a, b, c, k[13], 12, -40341101);
	c = ff(c, d, a, b, k[14], 17, -1502002290);
	b = ff(b, c, d, a, k[15], 22, 1236535329);

	a = gg(a, b, c, d, k[1], 5, -165796510);
	d = gg(d, a, b, c, k[6], 9, -1069501632);
	c = gg(c, d, a, b, k[11], 14, 643717713);
	b = gg(b, c, d, a, k[0], 20, -373897302);
	a = gg(a, b, c, d, k[5], 5, -701558691);
	d = gg(d, a, b, c, k[10], 9, 38016083);
	c = gg(c, d, a, b, k[15], 14, -660478335);
	b = gg(b, c, d, a, k[4], 20, -405537848);
	a = gg(a, b, c, d, k[9], 5, 568446438);
	d = gg(d, a, b, c, k[14], 9, -1019803690);
	c = gg(c, d, a, b, k[3], 14, -187363961);
	b = gg(b, c, d, a, k[8], 20, 1163531501);
	a = gg(a, b, c, d, k[13], 5, -1444681467);
	d = gg(d, a, b, c, k[2], 9, -51403784);
	c = gg(c, d, a, b, k[7], 14, 1735328473);
	b = gg(b, c, d, a, k[12], 20, -1926607734);

	a = hh(a, b, c, d, k[5], 4, -378558);
	d = hh(d, a, b, c, k[8], 11, -2022574463);
	c = hh(c, d, a, b, k[11], 16, 1839030562);
	b = hh(b, c, d, a, k[14], 23, -35309556);
	a = hh(a, b, c, d, k[1], 4, -1530992060);
	d = hh(d, a, b, c, k[4], 11, 1272893353);
	c = hh(c, d, a, b, k[7], 16, -155497632);
	b = hh(b, c, d, a, k[10], 23, -1094730640);
	a = hh(a, b, c, d, k[13], 4, 681279174);
	d = hh(d, a, b, c, k[0], 11, -358537222);
	c = hh(c, d, a, b, k[3], 16, -722521979);
	b = hh(b, c, d, a, k[6], 23, 76029189);
	a = hh(a, b, c, d, k[9], 4, -640364487);
	d = hh(d, a, b, c, k[12], 11, -421815835);
	c = hh(c, d, a, b, k[15], 16, 530742520);
	b = hh(b, c, d, a, k[2], 23, -995338651);

	a = ii(a, b, c, d, k[0], 6, -198630844);
	d = ii(d, a, b, c, k[7], 10, 1126891415);
	c = ii(c, d, a, b, k[14], 15, -1416354905);
	b = ii(b, c, d, a, k[5], 21, -57434055);
	a = ii(a, b, c, d, k[12], 6, 1700485571);
	d = ii(d, a, b, c, k[3], 10, -1894986606);
	c = ii(c, d, a, b, k[10], 15, -1051523);
	b = ii(b, c, d, a, k[1], 21, -2054922799);
	a = ii(a, b, c, d, k[8], 6, 1873313359);
	d = ii(d, a, b, c, k[15], 10, -30611744);
	c = ii(c, d, a, b, k[6], 15, -1560198380);
	b = ii(b, c, d, a, k[13], 21, 1309151649);
	a = ii(a, b, c, d, k[4], 6, -145523070);
	d = ii(d, a, b, c, k[11], 10, -1120210379);
	c = ii(c, d, a, b, k[2], 15, 718787259);
	b = ii(b, c, d, a, k[9], 21, -343485551);

	x[0] = add32(a, x[0]);
	x[1] = add32(b, x[1]);
	x[2] = add32(c, x[2]);
	x[3] = add32(d, x[3]);

}

function cmn(q, a, b, x, s, t) {
	a = add32(add32(a, q), add32(x, t));
	return add32((a << s) | (a >>> (32 - s)), b);
}

function ff(a, b, c, d, x, s, t) {
	return cmn((b & c) | ((~b) & d), a, b, x, s, t);
}

function gg(a, b, c, d, x, s, t) {
	return cmn((b & d) | (c & (~d)), a, b, x, s, t);
}

function hh(a, b, c, d, x, s, t) {
	return cmn(b ^ c ^ d, a, b, x, s, t);
}

function ii(a, b, c, d, x, s, t) {
	return cmn(c ^ (b | (~d)), a, b, x, s, t);
}

function md51(s) {
	txt = '';
	var n = s.length,
	state = [1732584193, -271733879, -1732584194, 271733878], i;
	for (i = 64; i <= s.length; i += 64) {
		md5cycle(state, md5blk(s.substring(i - 64, i)));
	}
	s = s.substring(i - 64);
	var tail = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
	for (i = 0; i < s.length; i++)
		tail[i >> 2] |= s.charCodeAt(i) << ((i % 4) << 3);
	tail[i >> 2] |= 0x80 << ((i % 4) << 3);
	if (i > 55) {
		md5cycle(state, tail);
		for (i = 0; i < 16; i++) tail[i] = 0;
	}
	tail[14] = n * 8;
	md5cycle(state, tail);
	return state;
}

/* there needs to be support for Unicode here,
 * unless we pretend that we can redefine the MD-5
 * algorithm for multi-byte characters (perhaps
 * by adding every four 16-bit characters and
 * shortening the sum to 32 bits). Otherwise
 * I suggest performing MD-5 as if every character
 * was two bytes--e.g., 0040 0025 = @%--but then
 * how will an ordinary MD-5 sum be matched?
 * There is no way to standardize text to something
 * like UTF-8 before transformation; speed cost is
 * utterly prohibitive. The JavaScript standard
 * itself needs to look at this: it should start
 * providing access to strings as preformed UTF-8
 * 8-bit unsigned value arrays.
 */
function md5blk(s) { /* I figured global was faster.   */
	var md5blks = [], i; /* Andy King said do it this way. */
	for (i = 0; i < 64; i += 4) {
		md5blks[i >> 2] = s.charCodeAt(i)
		+ (s.charCodeAt(i + 1) << 8)
		+ (s.charCodeAt(i + 2) << 16)
		+ (s.charCodeAt(i + 3) << 24);
	}
	return md5blks;
}

var hex_chr = '0123456789abcdef'.split('');

function rhex(n) {
	var s = '', j = 0;
	for (; j < 4; j++)
		s += hex_chr[(n >> (j * 8 + 4)) & 0x0F]
		+ hex_chr[(n >> (j * 8)) & 0x0F];
	return s;
}

function hex(x) {
	for (var i = 0; i < x.length; i++)
		x[i] = rhex(x[i]);
	return x.join('');
}

function md5(s) {
	return hex(md51(s));
}

/* this function is much faster,
so if possible we use it. Some IEs
are the only ones I know of that
need the idiotic second function,
generated by an if clause.  */

function add32(a, b) {
	return (a + b) & 0xFFFFFFFF;
}

if (md5('hello') != '5d41402abc4b2a76b9719d911017c592') {
	function add32(x, y) {
		var lsw = (x & 0xFFFF) + (y & 0xFFFF),
		msw = (x >> 16) + (y >> 16) + (lsw >> 16);
		return (msw << 16) | (lsw & 0xFFFF);
	}
}
///#source 1 1 /Scripts/site/site.js
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

var comments = {

//	displayComments: function (postId, $postComments) {
//		var $spinner = $("<p class='text-center'><i class='icon-spinner icon-3x icon-spin'></i></p>");
//		$postComments.before($spinner);
//		var url = "/Post/Comments/" + postId;
//		$.ajax({
//			type: "GET",
//			url: url,
//			dataType: "json",
//			contentType: "application/json; charset=utf-8",
//			success: function (data) {
//				var $comments = $(templates.commentsTemplate(data));
//				$comments.appendTo($postComments);
//				$postComments.slideDown(300);
//				$spinner.remove();
//			},
//			error: function (xhr, ajaxOptions, thrownError) {
//				$spinner.remove();
//				$(this).click(comments.loadComments);
//				$("<div class='alert alert-warning'>Could not load comments, please retry.<br/>" + xhr.status + " " + thrownError + "<div>").appendTo($postComments);
//			}
//		});
//	},

//	loadComments: function () {
//		var $this = $(this);
//		$this.off("click");
//		$this.click(function () { return false; });

//		var $post = $this.parents("div.post:first");
//		var postId = $post.find("input.postId:first").val();
//		var $postComments = $post.find("div.postComments:first");
//		$postComments.children().remove();
//		comments.displayComments(postId, $postComments);
//		return false;
//	},

//	displayCommentForm: function () {
//		var $this = $(this);
//		$this.off("focusin");

//		var $commentForm = $this.parents("form.addCommentForm:first");
//		comments.createRecaptcha($commentForm);
//		$commentForm.children("div.form-group").show();
//		$commentForm.find("#Name").focus();
//	},

//	createRecaptcha: function($commentForm) {
//		$commentRecaptcha = $commentForm.find("div.commentRecaptcha:first");
//		$commentRecaptcha.children().remove();
//		$commentRecaptcha.parent().find("span.help-block").remove();
//		Recaptcha.destroy();
//		Recaptcha.create("6LdJ_-cSAAAAABSQ03vab49RBrBYXyiovv8ncnbp", $commentRecaptcha.attr("id"), {
//			theme: "clean"
//		});
//	},

//	displayCommentAvatar: function () {
//		var emailHash = md5($(this).val().trim().toLowerCase());

//		var $addCommentForm = $(this).parents("form.addCommentForm:first");
//		$addCommentForm.find("#EmailHash").val(emailHash);
//		$addCommentForm.find("img.commentAvatar:first").attr("src", "http://www.gravatar.com/avatar/" + emailHash + "?d=mm&s=60");
//	},

//	addComment: function (form) {
//		var $form = $(form);
//		var $post = $form.parents("div.post:first");
//		var formData = $form.serializeArray();

//		$.ajax({
//			type: "POST",
//			url: "/Comments/Add",
//			data: formData,
//			success: function (data) { comments.commentAdded(data, $post, $form); },
//			error: function (xhr, ajaxOptions, thrownError) {
//				alert(xhr.status);
//				alert(thrownError);
//			}
//		});
//		return false;
//	},

//	commentAdded: function (data, $post, $form) {
//		// check result
//		if (!data.IsValid) {
//			comments.createRecaptcha($form);
//			if (data.RecaptchaResponse !== undefined) {
//				var $recaptcha = $form.find("div.commentRecaptcha:first");
//				$recaptcha.parent().append($("<label class='text-danger'>" + data.RecaptchaResponse.ErrorMessage + "</label>"));
//			}
//			else if (data.Message !== undefined) {
//				var $button = $form.find("button.addComment:first");
//				$button.before($("<div class='text-danger'>" + data.Message + "</div>"));
//			}
//			else {
//				alert(JSON.stringify(data));
//			}
//		}
//		else {
//			// increase post count
//			$post.find("span.postTotalComments").each(function (idx, item) {
//				var postTotalComments = parseInt($(item).html()) + 1;
//				$(item).html(postTotalComments);
//			});

//			var $postComments = $post.find("div.postComments:first");
//			if ($postComments.is(":hidden")) {
//				// if comments not displayed, get them
//				$post.find("a.viewComments").click();
//			}
//			else {
//				// add post content
//				var jsonFormData = $form.serializeObject();
//				jsonFormData.WhenCommented = Date.now();
//				$(templates.commentsTemplate([jsonFormData])).appendTo($postComments);
//			}

//			// reset form
//			comments.resetCommentForm($form, true);
//		}
//	},

//	resetCommentForm: function($form, resetData) {
//		var $commentText = $form.find("textarea.commentText:first");
//		if (resetData) {
//			$form.trigger("reset");
//			$form.find("#Id").val(generateGuid());
//			$form.find("img.commentAvatar:first").attr("href", "http://www.gravatar.com/avatar/x?d=mm&s=60");
//		};
//		$form.find("div.form-group").hide(0);
//		$commentText.parents("div.form-group:first").show(0);
//		$commentText.off("focusin");
//		$commentText.focusin(comments.displayCommentForm);
//	},

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

//var templates = {
//	commentsTemplate: function (data) {
//		var $this = this;
//		if ($this.commentsTemplateResult === undefined) {
//			$.ajax({
//				type: "GET",
//				url: "/Content/Templates/commentsTemplate.html",
//				success: function (data) {
//					$this.commentsTemplateResult = Handlebars.compile(data);
//				},
//				error: function (xhr, ajaxOptions, thrownError) {
//					alert(xhr.status);
//					alert(thrownError);
//				},
//				async: false
//			});
//		}
//		return $this.commentsTemplateResult(data);
//	}
//};

///#source 1 1 /Scripts/site/siteAngular.js

///#source 1 1 /Scripts/site/angular/modules/commentModule.js
var commentsModule = angular.module("commentsModule", []);

///#source 1 1 /Scripts/site/angular/factories/commentsFactory.js
commentsModule.factory("commentsFactory", function ($http) {
	return {
		createNewCommentValidation: function () {
			return {
				RecaptchaId: "",
				RecaptchaError: "",
				AddCommentError: "",
			};
		},
		createNewComment: function () {
			return {
				Id: generateGuid(),
				PostId: "",
				Name: "",
				EmailHash: "",
				CommentText: "",
				Homepage: "",
			};
		},
	}
});

///#source 1 1 /Scripts/site/angular/services/commentsService.js
commentsModule.service("commentsService", function ($http) {
	return {
		getComments: function (postId, successCallback) {
			var url = "/Post/Comments/" + postId;
			$http.get(url).success(successCallback);
		},
		addComment: function (comment, successCallback) {
			$http.post("/Comments/Add", comment).success(successCallback);
		}
	};
});

///#source 1 1 /Scripts/site/angular/services/recaptchaService.js
commentsModule.service("recaptchaService", function () {
	return {
		create: function (key, id, theme) {
			Recaptcha.destroy();
			Recaptcha.create(key, id, {
				theme: theme
			});
		}
	};
});

///#source 1 1 /Scripts/site/angular/directives/focusWhen.js
commentsModule.directive("focusWhen", function ($timeout, $parse) {
	return {
		link: function (scope, element, attrs) {
			var model = $parse(attrs.focusWhen);
			scope.$watch(model, function (value) {
				if (value === true) {
					$timeout(function () {
						element[0].focus();
					});
				}
			});
		}
	}
});

///#source 1 1 /Scripts/site/angular/directives/recaptcha.js
commentsModule.directive("recaptcha", function ($timeout, $parse, recaptchaService) {
	return {
		link: function (scope, element, attrs) {
			var key = attrs.key;
			var theme = attrs.theme;
			var showWhen = $parse(attrs.showWhen);
			var id = attrs.id;
			var ngModel = $parse(attrs.ngModel);
			ngModel.assign(scope, id);

			scope.$watch(showWhen, function (value) {
				$timeout(function () {
					recaptchaService.create(key, id, theme);
				});
			});
		}
	}
});

///#source 1 1 /Scripts/site/angular/filters/momentFormat.js
commentsModule.filter("momentFormat", function () {
	return function (input, format) {
		return new moment(input).format(format);
	};
});

///#source 1 1 /Scripts/site/angular/controllers/commentsController.js
commentsModule.controller("commentsController", function ($scope, $timeout, $http, recaptchaService, commentsService, commentsFactory) {
	// model
	$scope.comments = [];
	$scope.totalComments = 0;

	$scope.$on("NewComment", function (event, newComment) {
		$scope.comments.push(newComment);
		$scope.totalComments = $scope.comments.length;
	});

	$scope.showComments = function (postId, afterGet) {
		if ($scope.comments.length == 0) {
			$scope.showSpinner = true;
			commentsService.getComments(postId, function (data) {
				$scope.comments = data;
				$scope.totalComments = $scope.comments.length;
				if (afterGet !== undefined) {
					afterGet();
				}
				$scope.showSpinner = false;
			});
		}
	};
});
///#source 1 1 /Scripts/site/angular/controllers/newCommentController.js
commentsModule.controller("newCommentController", function ($scope, $timeout, $http, recaptchaService, commentsService, commentsFactory) {
	// model
	$scope.showNewComment = false;
	$scope.newCommentValidation = commentsFactory.createNewCommentValidation();
	$scope.newComment = commentsFactory.createNewComment();
	$scope.newCommentEmail = "";
	$scope.newCommentTextRows = 2;

	$scope.$watch("showNewComment", function (value) {
		$timeout(function () {
			$scope.newCommentTextRows = value ? 5 : 2;
		});
	});

	$scope.setNewCommentEmailHash = function () {
		$timeout(function () {
			if ($scope.newCommentEmail !== undefined) {
				$scope.newComment.EmailHash = md5($scope.newCommentEmail.trim().toLowerCase());
			}
		});
	};

	$scope.addNewComment = function () {
		$scope.newComment.WhenCommented = new Date();
		var $recaptcha = $("#" + $scope.newCommentValidation.RecaptchaId);
		$scope.newComment.recaptcha_challenge_field = $recaptcha.find("#recaptcha_challenge_field").val();
		$scope.newComment.recaptcha_response_field = $recaptcha.find("#recaptcha_response_field").val();

		var commentAdded = function (data) {
			if (!data.IsValid) {
				recaptchaService.create($recaptcha.attr("key"), $recaptcha.attr("id"), $recaptcha.attr("theme"));
				if (data.RecaptchaResponse !== undefined) {
					$scope.newCommentValidation.RecaptchaError = data.RecaptchaResponse.ErrorMessage;
				}
				else if (data.Message !== undefined) {
					$scope.newCommentValidation.AddCommentError = data.Message;
				}
				else {
					alert(JSON.stringify(data));
				}
			}
			else {
				$scope.$emit("NewComment", $scope.newComment);
				$scope.newComment = commentsFactory.createNewComment();
				$scope.showNewComment = false;
			}
		};

		if ($scope.totalComments != 0 && $scope.comments.length == 0) {
			$scope.showComments($scope.newComment.PostId, function () {
				commentsService.addComment($scope.newComment, commentAdded);
			});
		}
		else {
			$timeout(function () {
				commentsService.addComment($scope.newComment, commentAdded);
			});
		}
	};
});
