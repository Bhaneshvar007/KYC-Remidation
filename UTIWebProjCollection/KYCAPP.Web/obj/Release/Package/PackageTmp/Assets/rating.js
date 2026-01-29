app.controller("RatingList", ["$scope", "RatingListFactory", "$timeout", "$location", "$window", function ($scope, RatingListFactory, $timeout, $location, $window) {

    RatingListFactory.init(
        function (success) {
            //debugger;
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
                            return '<a href="/Rating/Item/' + row.ID + '">' + row.Code + '</a>'
                        }
                    },
                    { 'data': 'Rating' },
                    { 'data': 'Rating_Name' },
                    { 'data':'Rating_value'},
                    { 'data': 'IsActive' }]
            });
           // $scope.Rating_grid_data = success[0].data;
        });
}]);

app.factory("RatingListFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        $q.all([
           this.getRatingGrid()
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getRatingGrid = function () {
        return $http.post("/Rating/GetRatingGrid");
    }
    return this;
}]);


app.controller("Rating", ["$scope", "RatingFactory", "$timeout", "$location", "$window", "$validator", function ($scope, RatingFactory, $timeout, $location, $window, $validator) {


    $scope.save = function () {
        //debugger;
        $validator.validate($scope).success(function () {
            RatingFactory.save($scope.model).then(function (success) {
                alert(success.data);
                //window.location.href = window.location.origin + '/Item/0';
                $window.location.reload();
            });
        });
    };


    RatingFactory.init(
        function (success) {
            //debugger;
            $scope.model = success[0].data;
            $scope.periods = [{ "Name": "UpTo3M" }, { "Name": "UpTo1Y" }, { "Name": "MORTHAN1Y" }];
        });
}]);
app.factory("RatingFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
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
        return $http.post(("/Rating/GetItem"), { id: id })
    },
    this.save = function (model) {
        return $http.post(("/Rating/Save"), { model: model });
    }
    this.getGridDetail = function (AsOnDate, ClaimType) {
        return $http.get('/Rating/GetGridDetail/');
    }
    this.getClaimType = function () {
        return $http.get('/Rating/GetDetails/');
    }
    return this;
}]);