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
