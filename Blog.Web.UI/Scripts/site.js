// Google Analytics
var _gaq = _gaq || [];
_gaq.push(['_setAccount', 'UA-44407792-1']);
_gaq.push(['_trackPageview']);
(function () {
	var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
	ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
	var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();

var comments = {
	displayComments: function (postId, $postComments) {
		var $spinner = $("<p class='text-center'><i class='icon-spinner icon-3x icon-spin'></i></p>");
		$postComments.before($spinner);
		var url = "/Post/Comments/" + postId;
		$.ajax({
			type: "GET",
			url: url,
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: function (data) {
				var $comments = $(templates.commentsTemplate(data));
				$comments.appendTo($postComments);
				$postComments.slideDown(300);
				$spinner.remove();
			},
			error: function (xhr, ajaxOptions, thrownError) {
				$spinner.remove();
				$(this).click(comments.loadComments);
				$("<div class='alert alert-warning'>Could not load comments, please retry.<br/>" + xhr.status + " " + thrownError + "<div>").appendTo($postComments);
			}
		});
	},

	loadComments: function () {
		var $this = $(this);
		$this.off("click");
		$this.click(function () { return false; });

		var $post = $this.parents("div.post:first");
		var postId = $post.find("input.postId:first").val();
		var $postComments = $post.find("div.postComments:first");
		$postComments.children().remove();
		comments.displayComments(postId, $postComments);
		return false;
	},

	displayCommentForm: function () {
		var $this = $(this);
		$this.off("focusin");

		var $commentForm = $this.parents("form.addCommentForm:first");
		$commentRecaptcha = $commentForm.find("div.commentRecaptcha:first");
		$commentRecaptcha.children().remove();
		Recaptcha.destroy();
		Recaptcha.create("6LdJ_-cSAAAAABSQ03vab49RBrBYXyiovv8ncnbp", $commentRecaptcha.attr("id"), {
			theme: "clean",
			callback: Recaptcha.focus_response_field
		});
		$commentForm.children("div.form-group").show();
	},

	displayCommentAvatar: function () {
		var emailHash = md5($(this).val().trim().toLowerCase());

		var $addCommentForm = $(this).parents("form.addCommentForm:first");
		$addCommentForm.find("#EmailHash").val(emailHash);
		$addCommentForm.find("img.commentAvatar:first").attr("src", "http://www.gravatar.com/avatar/" + emailHash + "?d=mm&s=60");
	},

	addComment: function () {
		var $this = $(this);

		var $post = $this.parents("div.post:first");
		var $form = $this.parents("form.addCommentForm:first");
		var formData = $form.serializeArray();

		$.ajax({
			type: "POST",
			url: "/Comments/Add",
			data: formData,
			success: function (data) {
				// increase post count
				$post.find("span.postTotalComments").each(function (idx, item) {
					var postTotalComments = parseInt($(item).html()) + 1;
					$(item).html(postTotalComments);
				});

				// if posts not displayed get posts
				var $postComments = $post.find("div.postComments:first");
				if ($postComments.is(":hidden")) {
					$post.find("a.viewComments").click();
				}
				else {
					// add post content
					var jsonFormData = $form.serializeObject();
					jsonFormData.WhenCommented = Date.now();
					$(templates.commentsTemplate([jsonFormData])).appendTo($postComments);
				}

				// reset form

				var $commentText = $form.find("textarea.commentText:first");
				$form.trigger("reset");
				$form.find("#Id").val(generateGuid());
				$form.find("img.commentAvatar:first").attr("href", "http://www.gravatar.com/avatar/x?d=mm&s=60");
				$form.find("div.form-group").hide(0);
				$commentText.parents("div.form-group:first").show(0);
				$commentText.off("focusin");
				$commentText.focusin(comments.displayCommentForm);
			},
			error: function (xhr, ajaxOptions, thrownError) {
				alert(xhr.status);
				alert(thrownError);
			}
		});
		return false;
	}
}

var generateGuid = function(){
	var guid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
		var r = Math.random()*16|0, v = c == 'x' ? r : (r&0x3|0x8);
		return v.toString(16);
	});     
	return guid;
};

$.fn.serializeObject = function()
{
	var o = {};
	var a = this.serializeArray();
	$.each(a, function() {
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

var templates = {
	commentsTemplate: function (data) {
		var $this = this;
		if ($this.commentsTemplateResult === undefined) {
			$.ajax({
				type: "GET",
				url: "/Content/Templates/commentsTemplate.html",
				success: function (data) {
					$this.commentsTemplateResult = Handlebars.compile(data);
				},
				error: function (xhr, ajaxOptions, thrownError) {
					alert(xhr.status);
					alert(thrownError);
				},
				async: false
			});
		}
		return $this.commentsTemplateResult(data);
	}
};