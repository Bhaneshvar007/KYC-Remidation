app.controller("FileUploadStatus", ["$scope", "FileUploadStatusFactory", "$timeout", "$location", "$window", function ($scope, FileUploadStatusFactory, $timeout, $location, $window) {

    $scope.allCheckBoxStatus = "Select All";
    $scope.BATCH_NUMBER = 1001;
    $scope.isPaymentdone = true;
    $scope.isAssignBatchStart = false;
    $scope.From_Date = new Date(new Date().setHours(0, 0, 0, 0));
    $scope.To_Date = new Date(new Date().setHours(0, 0, 0, 0));

    $scope.GetGridRecord = function (from_date, to_date) {

        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));

        FileUploadStatusFactory.getGridDetail(from_date, to_date).then(function (success) {
            //debugger;
            $scope.FileUploadStatusGridData = success.data;
            if (success.data.length > 0) {
                $("#table_id").dataTable({
                    bLengthChange: true,
                    lengthMenu: [[5, 10, -1], [5, 10, "All"]],
                    bFilter: true,
                    bSort: true,
                    bDestroy: true,
                    bPaginate: true,
                    data: success.data,
                    columns: [
                        {
                            'data': 'ID',
                            "render": function (data, type, full, meta) {
                                success.data[meta.row].id = meta.row + 1; // adds id to dataset
                                return meta.row + 1; // adds id to serial no
                            }
                        },
                        {
                            'data': 'FileName'

                        },
                        { 'data': 'TotalRowCount' },
                        {
                            'data': 'UploadDate',
                            "render": function (data) {
                                var date = new Date(data);
                                var month = date.getMonth() + 1;
                                return date.getDate() + "/" + (month.toString().length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                            }
                        }]
                        
                });
            }
            else {
                alert("No Record Found !")
            }
        });
    }

}]);
app.factory("FileUploadStatusFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {

    this.getGridDetail = function (from_date, to_date) {
        return $http.post(('/FileUploadStatus/GetGridDetail/'), { From_Date: from_date.toJSON(), To_Date: to_date.toJSON() });
    }
    return this;
}]);