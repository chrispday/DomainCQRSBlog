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