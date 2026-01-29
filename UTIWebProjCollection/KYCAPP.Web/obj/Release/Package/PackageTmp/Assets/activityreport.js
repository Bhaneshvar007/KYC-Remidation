app.controller("ActivityReport", ["$scope", "ActivityReportFactory", "$timeout", "$location", "$window", function ($scope, ActivityReportFactory, $timeout, $location, $window) {
    $scope.AS_ON_DATE = new Date(new Date().setHours(0, 0, 0, 0));

    $scope.GetGridRecord = function (asOnDate) {

        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));

        ActivityReportFactory.getGridDetail(asOnDate).then(function (success) {
            $scope.ActivityReportGridData = success.data;
        });
    }

}]);
app.factory("ActivityReportFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.getGridDetail = function (AsOnDate) {
        return $http.post(('/ActivityReport/GetGridDetail/'), { AsOnDate: AsOnDate.toJSON() });
    }
    return this;
}]);