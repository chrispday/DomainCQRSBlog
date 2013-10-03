commentsModule.filter("momentFormat", function () {
	return function (input, format) {
		return new moment(input).format(format);
	};
});
