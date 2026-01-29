app.controller("kycDataAcceptanceReportStaff", ["$scope", "DataAcceptanceRptStaffReportFactory", "$timeout", "$location", "$window", function ($scope, DataAcceptanceRptStaffReportFactory, $timeout, $location, $window) {

    $scope.searchButtonText = "Search";
    $scope.from_date = new Date(new Date().setHours(0, 0, 0, 0));
    $scope.to_date = new Date(new Date().setHours(0, 0, 0, 0));

    $scope.status_list = [{ "ID": "S", "Name": "Selected" }, { "ID": "R", "Name": "Remediated" }];

    $scope.GetGridRecord = function (from_date, to_date, status) {
        //debugger;
        $scope.searchButtonText = "Searching";
        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));

        DataAcceptanceRptStaffReportFactory.getGridDetail(from_date, to_date, status).then(function (success) {
            $scope.searchButtonText = "Search";
            $timeout(() => {
                var data = $('#table_id').DataTable({
                    lengthMenu: [[5, 25, 50, 100, 200, -1], [5, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })
            $scope.data_acceptance_grid_data_staff = success.data;


            if (success.data.length > 0) {
            }
            else {
                alert("No Record Found !");
            }
        });
    }


}]);
app.factory("DataAcceptanceRptStaffReportFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        $q.all([
            // write here to page load
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getGridDetail = function (from_date, to_date, status) {
        return $http.post(('/KYC/DataAcceptanceRpt_Staff/GetGridDetail/'), { from_date: from_date, to_date: to_date, status: status });
    }
    return this;
}]);