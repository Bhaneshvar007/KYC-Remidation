app.controller("PanSearch", ["$scope", "PanSearchFactory", "$timeout", "$location", "$window", function ($scope, PanSearchFactory, $timeout, $location, $window) {


    $scope.searchButtonText = "Search";

    $scope.AS_ON_DATE = new Date(new Date().setHours(0, 0, 0, 0));


    $scope.GetGridRecord = function (PanNo) {


        $('#table_id').dataTable().fnDestroy();
        $scope.searchButtonText = "Searching";

        PanSearchFactory.getGridDetail(PanNo).then(function (success) {
            //debugger;
            $scope.searchButtonText = "Search";


            $scope.kyc_pan_grid_data = success.data;

            $timeout(() => {
                var data = $('#table_id').DataTable({
                    lengthMenu: [[5, 25, 50, 100, 200, -1], [5, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();



            })

            if (success.data.length <= 0) {

                alert("No Record Found !");
            }
        });



    }

    //PanSearchFactory.init(
    //    function (success) {
    //        ////debugger;
    //        //.employee_list = success[0].data;
    //        //$scope.totalCount = success[0].data;

    //    });
}]);
app.factory("PanSearchFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    //this.init = function (success, failure) {

    //    $q.all([
    //       // this.getEmployees(),

    //    ]).then(function (msg) {
    //        //success(msg);
    //    }, failure);
    //}
    this.getGridDetail = function (PanNo) {
        var config = {
            headers: {
                'X-Button-Name': 'Search'
            }
        };
        return $http.post(('/PanSearch/GetGridDetails/'), { PanNo: PanNo }, config);
    }
   

    return this;
}]);