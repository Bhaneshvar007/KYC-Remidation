app.controller("RoleList", ["$scope", "RoleListFactory", "$timeout", "$location", "$window", function ($scope, RoleListFactory, $timeout, $location, $window) {


    //$scope.save = function () {
    //    //debugger;
    //    RoleFactory.save($scope.model).then(function (success) {
    //        alert(success.data);
    //        $window.location.reload();
    //    });
    //};


    RoleListFactory.init(
        function (success) {
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
                        'data': 'Code',
                        'render': function (data, type, row) {
                            return '<a href="/Role/Item/' + row.ID + '">' + row.Code + '</a>'
                        }
                    },
                    { 'data': 'Name' },
                    { 'data': 'isactive' }]
            });
           // $scope.rolegridData = success[0].data;
        });
}]);

app.factory("RoleListFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        $q.all([
           this.getRoleGrid()
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getRoleGrid = function () {
        return $http.post("/Role/GetRoleGrid");
    }
    return this;
}]);


app.controller("Role", ["$scope", "RoleFactory", "$timeout", "$location", "$window", "toaster","$validator", function ($scope, RoleFactory, $timeout, $location, $window, toaster,$validator) {

    $scope.downloadTypes = [{ "ID": 0, "Name": "Select" }, { "ID": 1, "Name": "PDF" }]

    $scope.save = function () {
        //debugger;
        $validator.validate($scope).success(function () {
            RoleFactory.save($scope.model).then(function (success) {
                //toaster.pop('success', "success", "text");
                alert(success.data);
                window.location.href = window.location.origin + '/Role';
                //$window.location.reload();
            });
        });
    };


    RoleFactory.init(
        function (success) {
            $scope.model = success[0].data;
        });
}]);


app.factory("RoleFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
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
        return $http.post(("/Role/GetItem"), { id: id })
    },
    this.save = function (model) {
        return $http.post(("/Role/save"), { model: model });
    }
    return this;
}]);