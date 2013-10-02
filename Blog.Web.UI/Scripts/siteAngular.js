var commentsModule = angular.module("commentsModule", []);

commentsModule.factory("commentsFactory", function ($http) {
	return {
		getComments: function (postId, setComments) {
			var url = "/Post/Comments/" + postId;
			$http.get(url).success(setComments);
		},
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
				Email: "",
				EmailHash: "x",
				CommentText: "",
				ShowEmail: false,
				Validation: this.createNewCommentValidation()
			};
		},
	}
});

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

commentsModule.filter("momentFormat", function () {
	return function (input, format) {
		return new moment(input).format(format);
	};
});

commentsModule.controller("commentsController", function ($scope, $timeout, $http, recaptchaService, commentsFactory) {
	$scope.comments = [];

	$scope.totalComments = 0;

	$scope.showComments = function (postId, afterGet) {
		$scope.showSpinner = true;
		commentsFactory.getComments(postId, function (data) {
			$scope.comments = data;
			if (afterGet !== undefined) {
				afterGet();
			}
			$scope.showSpinner = false;
		});
	};

	// New comments
	$scope.showNewComment = false;

	$scope.newComment = commentsFactory.createNewComment();

	$scope.setNewCommentEmailHash = function () {
		$timeout(function () {
			if ($scope.newComment.Email !== undefined) {
				$scope.newComment.EmailHash = md5($scope.newComment.Email.trim().toLowerCase());
			}
		});
	};

	// Add comment
	$scope.addNewComment = function () {
		$scope.newComment.WhenCommented = new Date();
		var $recaptcha = $("#" + $scope.newComment.Validation.RecaptchaId);
		$scope.newComment.recaptcha_challenge_field = $recaptcha.find("#recaptcha_challenge_field").val();
		$scope.newComment.recaptcha_response_field = $recaptcha.find("#recaptcha_response_field").val();

		var addComment = function () {
			$http.post("/Comments/Add", $scope.newComment)
				.success(function (data) {
					if (!data.IsValid) {
						recaptchaService.create($recaptcha.attr("key"), $recaptcha.attr("id"), $recaptcha.attr("theme"));
						if (data.RecaptchaResponse !== undefined) {
							$scope.newComment.Validation.RecaptchaError = data.RecaptchaResponse.ErrorMessage;
						}
						else if (data.Message !== undefined) {
							$scope.newComment.Validation.AddCommentError = data.Message;
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
				});
		};

		if ($scope.comments.length == 0) {
			$scope.showComments($scope.newComment.PostId, addComment);
		}
		else {
			$timeout(addComment);
		}
	};
});