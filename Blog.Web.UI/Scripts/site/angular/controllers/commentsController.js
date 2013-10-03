commentsModule.controller("commentsController", function ($scope, $timeout, $http, recaptchaService, commentsService, commentsFactory) {

	// models
	$scope.comments = [];
	$scope.totalComments = 0;
	$scope.showNewComment = false;
	$scope.newComment = commentsFactory.createNewComment();
	$scope.newCommentEmail = "";
	$scope.newCommentValidation = commentsFactory.createNewCommentValidation();

	$scope.showComments = function (postId, afterGet) {
		if ($scope.comments.length == 0) {
			$scope.showSpinner = true;
			commentsService.getComments(postId, function (data) {
				$scope.comments = data;
				if (afterGet !== undefined) {
					afterGet();
				}
				$scope.showSpinner = false;
			});
		}
	};

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
				$scope.comments.push($scope.newComment);
				$scope.totalComments = $scope.comments.length;
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