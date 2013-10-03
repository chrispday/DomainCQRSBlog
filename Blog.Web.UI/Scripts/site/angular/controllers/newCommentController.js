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