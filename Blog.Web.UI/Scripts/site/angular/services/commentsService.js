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
