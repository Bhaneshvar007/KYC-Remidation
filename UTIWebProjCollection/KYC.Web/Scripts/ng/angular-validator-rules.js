(function () {
    angular.module('validator.rules', ['validator']).config(['$validatorProvider', function ($validatorProvider) {
        $validatorProvider.register('required', {
            invoke: 'watch',
            validator: /.+/,
            error: 'This field is required.'
        });
        $validatorProvider.register('requiredSelect', {
            invoke: 'blur',
            validator: function (value, scope, element, attrs, $injector) {
                return value !== undefined && value !== 0;
            },
            error: 'This field is required.'
        });

        //      

        $validatorProvider.register('minus', {
            invoke: 'watch',
            validator: /^\-?\d*\.?\d*$/,
            error: 'This field should not be the minus number.'

        });

        $validatorProvider.register('number', {
            invoke: 'watch',
            validator: /^[+]?[0-9]*[\.]?[0-9]*$/,
            error: 'This field should be the number.'
        });



        $validatorProvider.register('Zeronumber', {
            invoke: 'watch',
            //validator: /^[1-9]\d{0,2}(\d*|(,\d{3})*)$/,
            validator: /^0*[1-9]\d{0,2}(\d*|(,\d{3})*)$/,
            error: 'This field should not be ZERO.'
        });
        //?:[1-9]\d*|0

        $validatorProvider.register('word', {
            invoke: 'watch',
            validator: /^(?!.*(^storage$|^handling$)).*$/i,
            error: 'Please do not enter "Storage" or "Handling"as code.'
            /* ^ = beginning of string , . = matches any character except newline, * = repeat token 0 to infinite times, ?! = doesnt followed particular word*/
            /* /^\s*(STORAGE|HANDLING)\s*$/i - By Mitan Sir*/
        });

        $validatorProvider.register('min', {
            invoke: 'watch',
            init: function (value, scope, element, attrs, $injector) {
                this.customError = function (attrs) { return "This field width should be minimum " + attrs["min"]; }
            },
            validator: function (value, scope, element, attrs, $injector) {
                if (attrs["min"] === undefined)
                    return true;
                return (value.toString().length >= eval(attrs["min"]));
            },
            error: 'This field width should be minimum x'
        });
        $validatorProvider.register('max', {
            invoke: 'watch',
            init: function (value, scope, element, attrs, $injector) {
                this.customError = function (attrs) { return "This field should be maximum " + attrs["max"]; }
            },
            validator: function (value, scope, element, attrs, $injector) {
                if (attrs["max"] === undefined || value === undefined)
                    return true;
                return (value.toString().length <= eval(attrs["max"]));
            },
            error: 'This field should be maximum x'
        });


        $validatorProvider.register('rangemax', {
            invoke: 'watch',
            init: function (value, scope, element, attrs, $injector) {
                this.customError = function (attrs) { return "This field should be range less than" + attrs["rangemax"]; }
            },
            validator: function (value, scope, element, attrs, $injector) {
                if (attrs["rangemax"] === undefined || value === undefined)
                    return true;
                return (value <= eval(attrs["rangemax"]));
            },
            error: 'This field should be maximum x'
        });

        $validatorProvider.register('rangemin', {
            invoke: 'watch',
            init: function (value, scope, element, attrs, $injector) {
                this.customError = function (attrs) { return "This field should be range greater than " + attrs["rangemin"]; }
            },
            validator: function (value, scope, element, attrs, $injector) {
                if (attrs["rangemin"] === undefined || value === undefined)
                    return true;
                return (value >= eval(attrs["rangemin"]));
            },
            error: 'This field should be maximum x'
        });

        //this will not allow past date
        $validatorProvider.register('pastDate', {
            invoke: 'watch',
            init: function (value, scope, element, attrs, $injector) {
                this.customError = function (attrs) { return attrs["validatorMessage"]; }
            },
            validator: function (value, scope, element, attrs, $injector) {
                if (attrs["pastvalue"] === undefined)
                    return true;
                var val = scope.$eval(attrs["pastvalue"]);
                var currDate = new Date(new Date());
                if (val === undefined)
                    return false;
                else if (val < currDate)
                    return false;
                else
                    return true;
            },
            error: 'This field should be greater than xxx.'
        });

        //this will not allow future date
        $validatorProvider.register('futureDate', {
            invoke: 'watch',
            init: function (value, scope, element, attrs, $injector) {
                this.customError = function (attrs) { return attrs["validatorMessage"]; }
            },
            validator: function (value, scope, element, attrs, $injector) {
                if (attrs["futurevalue"] === undefined)
                    return true;
                var val = scope.$eval(attrs["futurevalue"]);
                var currDate = new Date(new Date());
                if (val === undefined)
                    return false;
                else if (val > currDate)
                    return false;
                else
                    return true;
            },
            error: 'This field should be greater than xxx.'
        });

        $validatorProvider.register('gt', {
            invoke: 'watch',
            init: function (value, scope, element, attrs, $injector) {
                this.customError = function (attrs) { return attrs["validatorMessage"]; }
            },
            validator: function (value, scope, element, attrs, $injector) {
                if (attrs["gtvalue"] === undefined)
                    return true;
                var val = scope.$eval(attrs["gtvalue"]);
                if (value === undefined)
                    return false;
                else if (value > val)
                    return true;
                else
                    return false;

            },
            error: 'This field should be greater than xxx.'
        });

        $validatorProvider.register('gte', {
            invoke: 'watch',
            init: function (value, scope, element, attrs, $injector) {
                this.customError = function (attrs) { return attrs["validatorMessage"]; }
            },
            validator: function (value, scope, element, attrs, $injector) {
                debugger;
                if (attrs["gtevalue"] === undefined)
                    return true;
                var val = scope.$eval(attrs["gtevalue"]);
                
                val = val.setHours(0, 0, 0, 0);
                if (value === undefined)
                    return false;
                else if (value >= val)
                    return true
                else
                    return false;

            },
            error: 'This field should be greater then or equal to xxx.'
        });


        $validatorProvider.register('lt', {
            invoke: 'watch',
            init: function (value, scope, element, attrs, $injector) {
                this.customError = function (attrs) { return attrs["validatorMessage"]; }
            },
            validator: function (value, scope, element, attrs, $injector) {
                if (attrs["ltvalue"] === undefined)
                    return true;
                var val = scope.$eval(attrs["ltvalue"]);
                if (val === undefined)
                    return true;
                if (value === undefined)
                    return false;
                else if (value < val)
                    return true
                else
                    return false;

            },
            error: 'This field should be less than xxx.'
        });

        $validatorProvider.register('lte', {
            invoke: 'watch',
            init: function (value, scope, element, attrs, $injector) {
                this.customError = function (attrs) { return attrs["validatorMessage"]; }
            },
            validator: function (value, scope, element, attrs, $injector) {
                if (attrs["ltevalue"] === undefined)
                    return true;
                var val = scope.$eval(attrs["ltevalue"]);
                if (value === undefined)
                    return false;
                else if (value <= val)
                    return true

                else
                    return false;

            },
            error: 'This field should be less than or equal to xxx.'
        });

        /*Under progress*/
        $validatorProvider.register('bt', {
            invoke: 'watch',
            init: function (value, scope, element, attrs, $injector) {
                this.customError = function (attrs) { return attrs["validatorMessage"]; }
            },
            validator: function (value, scope, element, attrs, $injector) {
                if (attrs["btvalue"] === undefined)
                    return true;
                var val = scope.$eval(attrs["btvalue"]);
                if (value === undefined)
                    return false;
                else if (value >= val && value <= val)
                    return true
                else
                    return false;

            },
            error: 'This field should be less than or equal to xxx.'
        });


        $validatorProvider.register('custom', {
            invoke: 'watch',
            init: function (value, scope, element, attrs, $injector) {
                this.customError = function (attrs) { return attrs["validatorMessage"]; }
            },
            validator: function (value, scope, element, attrs, $injector) {
                if (attrs["function"] === undefined || value === undefined)
                    return true;
                var err = scope.$eval(attrs["function"]);
                if (err === undefined || err === "")
                    return true;
                else {
                    element.attr("validator-message", err);
                    return false;
                }
            },
            error: 'This field should be less.'
        });


        $validatorProvider.register('alphanumeric', {
            invoke: 'watch',
            validator: /^([-]\[+])?\w*$/i,
            error: 'This field should not contain special character and spaces.'
        });

        $validatorProvider.register('NoNumber', {
            invoke: 'watch',
            validator: /^([^0-9]*)$/,
            error: 'This field should not allowed number.'
        });
        $validatorProvider.register('Date', {
            invoke: 'watch',
            validator: /[D|d]{2}[-|\/][m|M]{2}[-|\/][y|Y]{4} [H | h]{2}:[M | m]{2}:[S | s]{2}$/,
            error: 'This field should only allowed / or - for date format.'
        });
        $validatorProvider.register('UTCValidator', {
            invoke: 'watch',
            validator: /[\d]{2}:[\d]{2}$/,
            error: 'UTC Zone fromat according to city eg.(HH:mm)'
        });
        $validatorProvider.register('hashRegx', {
            invoke: 'watch',
            validator: /^[#]...+[#]/,
            error: 'This field should starts and ends with #.'
        });


        $validatorProvider.register('integer', {
            invoke: 'watch',
            validator: /^[0-9]+$/,
            error: 'This field should be the number.'
        });
        $validatorProvider.register('email', {
            invoke: 'blur',
            validator: /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
            error: 'This field should be an email.'
        });
        $validatorProvider.register('url', {
            invoke: 'blur',
            validator: /((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)/,
            error: 'This field should be the url.'
        });

        $validatorProvider.register('characters', {
            invoke: 'watch',
            validator: /^[a-zA-Z ]*$/,
            error: 'This field should be the characters only.'
        });

        $validatorProvider.register('special', {
            invoke: 'watch',
            validator: /^[a-zA-Z0-9,()-/ ]*$/,
            error: 'This field should not allowing special characters except - or /.'
        });
        return $validatorProvider;
    }
    ]);

}).call(this);
