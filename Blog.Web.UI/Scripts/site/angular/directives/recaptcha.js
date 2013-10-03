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
