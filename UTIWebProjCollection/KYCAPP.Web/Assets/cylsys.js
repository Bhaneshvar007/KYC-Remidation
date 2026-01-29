app.provider('cylsyssolution', function () {
    this.data = null;
    this.siteId = 0;
    this.site = null;
    this.userSign = null;
    this.lang = "us-en";
    this.theme = null;

    this.notify = function (type, message) {
        if (type == "error") {
            var errs = message.split('ERROR!');
            var opts = {};
            opts.timeOut = 0;
            $(errs).each(function (i, e) {
                if ($.trim(e) == "")
                    return;
                toastr[type](e, null, opts).css("font-size", "large");
            });
        } else
            toastr[type](message, null, opts);
    };
});
