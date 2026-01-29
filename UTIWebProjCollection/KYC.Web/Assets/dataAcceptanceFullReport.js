app.controller("kycDataAcceptanceFullReport", ["$scope", "DataAcceptanceFullReportFactory", "$timeout", "$location", "$window", function ($scope, DataAcceptanceFullReportFactory, $timeout, $location, $window) {

    $scope.searchButtonText = "Search";

    $scope.from_date = new Date(new Date().setHours(0, 0, 0, 0));
    $scope.to_date = new Date(new Date().setHours(0, 0, 0, 0));
       
    $scope.GetGridRecord = function (from_date,to_date) {
        debugger;
        $scope.searchButtonText = "Searching";
        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));

        DataAcceptanceFullReportFactory.getGridDetail(from_date, to_date).then(function (success) {
            $scope.searchButtonText = "Search";
            $scope.data_acceptance_full_grid_data = success.data;
            if (success.data.length > 0) {
            }
            else {
                alert("No Record Found !");
            }
        });
    }


}]);
app.factory("DataAcceptanceFullReportFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        $q.all([
            // write here to page load
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getGridDetail = function (from_date, to_date) {
        return $http.post(('/DataAcceptanceFullReport/GetGridDetail/'), { from_date: from_date, to_date: to_date});
    }
    return this;
}]);