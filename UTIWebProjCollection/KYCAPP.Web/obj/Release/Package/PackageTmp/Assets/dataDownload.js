app.controller("DownloadReport", ["$scope", "DownloadReportFactory", "$timeout", "$location", "$window", "toaster", "$validator", "$filter", function ($scope, DownloadReportFactory, $timeout, $location, $window, toaster, $validator, $filter) {


    $scope.from_date = new Date(new Date().setHours(0, 0, 0, 0));
    $scope.to_date = new Date(new Date().setHours(0, 0, 0, 0));


    $scope.download_formats = [{ "ID": 0, "Name": "Select" }, { "ID": 1, "Name": "PDF" }]
    $scope.format_type = $scope.download_formats[0];

    $scope.convertdate = function (from_date, to_date) {
        //debugger;
        if (from_date === undefined) {
            $scope.is_download_enable = false;
        }
        else {
            $scope.temp_from_date = $filter('date')(from_date, "yyyy-MM-dd");
        }
        if (to_date === undefined) {
            $scope.is_download_enable = false;
        } else {
            $scope.temp_to_date = $filter('date')(to_date, "yyyy-MM-dd");
        }

        if (from_date !== undefined && to_date !== undefined) {
            $scope.is_download_enable = true;
        }

    };




    //DownloadReportFactory.init(
    //    function (success) {
    //        $scope.model = success[0].data;
    //    });
}]);


app.factory("DownloadReportFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        var urls = window.location.href.split('?')[0].split('/');
        if (!isNaN(urls[urls.length - 1]))
            id = eval(urls[urls.length - 1]);
        $q.all([
            this.GetGridData()
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.GetGridData = function () {
        return $http.post(("/KYCRemediation/GetGridData"))
    }
    return this;
}]);
