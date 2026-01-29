
//var app = angular.module('myApp', [])
app.controller("CorrectionModule", ["$scope", "CorrectionModuleFactory", "$timeout", "$location", "$window", function ($scope, CorrectionModuleFactory, $timeout, $location, $window) {

    $scope.searchButtonText = "Search";
    $scope.action_status = "Save";

    $scope.FROM_DATE = new Date(new Date().setHours(0, 0, 0, 0));
    $scope.TO_DATE = new Date(new Date().setHours(0, 0, 0, 0));


    $scope.save = function (model, index) {
        debugger;

        $scope.Polling_data_update[index].action_status = "Saving...";

        CorrectionModuleFactory.save(model).then(function (success) {
            $timeout(function () {
                $scope.Polling_data_update[index].action_status = "Save";
            }, 1000);


            //alert(success.data);
            //$window.location.reload();
        });
    }

    $scope.GetGridRecord = function (FROM_DATE, TO_DATE) {
        debugger;
        $scope.searchButtonText = "Searching";
        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));

        CorrectionModuleFactory.getGridDetail(FROM_DATE, TO_DATE).then(function (success) {
            $scope.searchButtonText = "Search";
            $scope.Polling_data_update = success.data;
            for (var k = 0; k < $scope.Polling_data_update.length; k++) {
                $scope.Polling_data_update[k].Maturity_Date = new Date($scope.Polling_data_update[k].Maturity_Date);
            };
            
            if (success.data.length > 0) {
                if (success.data.length < 1)
                    $scope.isAssignBtnHide = true;
                else
                    $scope.isAssignBtnHide = false;
            }
            else {
                alert("No Record Found !");
            }
        });
    }




    //CorrectionModuleFactory.init(
    //    function (success) {
    //        // $scope.model = success[0].data;
    //        debugger;
    //        $scope.commentTypes = success[0].data;

    //    });



}]);
app.factory("CorrectionModuleFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        $q.all([
            CommonFactory.getCommentTypes()
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getGridDetail = function (FROM_DATE, TO_DATE) {
        return $http.post(('/CorrectionModule/GetGridDetail/'), { FROM_DATE: FROM_DATE.toJSON(), TO_DATE: TO_DATE.toJSON() });
    }
    this.save = function (model) {
        return $http.post(('/CorrectionModule/Save/'), { model: model });
    }
    return this;
}]);