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
