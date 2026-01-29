app.controller("EmailList", ["$scope", "EmailListListFactory", "$timeout", "$location", "$window", function ($scope, EmailListListFactory, $timeout, $location, $window) {

    EmailListListFactory.init(
        function (success) {
            debugger;
            $("#table_id").dataTable({
                bLengthChange: true,
                lengthMenu: [[5, 10, -1], [5, 10, "All"]],
                bFilter: true,
                bSort: true,
                bPaginate: true,
                data: success[0].data,
                columns: [

                    {
                        'data': 'ID',
                        "render": function (data, type, full, meta) {
                            success[0].data[meta.row].id = meta.row + 1; // adds id to dataset
                            return meta.row + 1; // adds id to serial no
                        }
                    },
                    {
                        'data': 'DESC1',
                        'render': function (data, type, row) {
                            return '<a href="/EmailConfiguration/Item/' + row.ID + '">' + row.DESC1 + '</a>'
                        }
                    },
                    { 'data': 'DESC1_MAIL_LIST' },
                    { 'data': 'DESC1_Remark' },
                    { 'data': 'DESC2' },
                    { 'data': 'DESC2_MAIL_LIST' },
                    { 'data': 'DESC2_Remark' },
                    { 'data': 'Modify_by' },
                    {
                        'data': 'Modify_date',
                        "render": function (data) {
                            if (data == null)
                                return "data updated successfully";
                            else {
                                var date = new Date(data);
                                var month = date.getMonth() + 1;
                                return date.getDate() + "/" + (month.toString().length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                            }
                        }
                    }
                ]
            });
        });
}]);

app.factory("EmailListListFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {

    this.init = function (success, failure) {
        var id = 0;
        $q.all([
            this.getEmailConfigurationGrid()
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getEmailConfigurationGrid = function () {
        return $http.post("/EmailConfiguration/GetEmailConfigurationGrid");
    }
    return this;
}]);


app.controller("EmailConfiguration", ["$scope", "EmailConfigurationFactory", "$timeout", "$location", "$window", "toaster", "$validator", function ($scope, EmailConfigurationFactory, $timeout, $location, $window, toaster, $validator) {
    $scope.Update = function () {
        debugger;
        $validator.validate($scope).success(function () {
            EmailConfigurationFactory.Update($scope.model).then(function (success) {
                alert(success.data);
                window.location.href = window.location.origin + '/EmailConfiguration/Item/1';
            });
        });
    };
    EmailConfigurationFactory.init(
        function (success) {

            $scope.model = success[0].data;
        });
}]);


app.factory("EmailConfigurationFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    debugger;
    this.init = function (success, failure) {
        var id = 0;
        var urls = window.location.href.split('?')[0].split('/');
        if (!isNaN(urls[urls.length - 1]))
            id = eval(urls[urls.length - 1]);
        $q.all([
            this.getItem(id)

        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getItem = function (id) {
        return $http.post(("/EmailConfiguration/GetItem"), { id: id })
    },

        this.getGridDetail = function (AsOnDate, ClaimType) {
            return $http.get('/EmailConfiguration/GetEmailConfigurationGrid/');
        }
    this.Update = function (model) {
        return $http.post(("/EmailConfiguration/Update"), { model: model });
    }
    return this;
}]);
