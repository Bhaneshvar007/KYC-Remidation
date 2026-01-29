app.controller("KYCRemediation", ["$scope", "KYCRemediationFactory", "$timeout", "$location", "$window", function ($scope, KYCRemediationFactory, $timeout, $location, $window) {

    $scope.allCheckBoxStatus = "Select All";
    $scope.BATCH_NUMBER = 1001;

    $scope.isAssignBtnHide = true;

    $scope.isAssignBatchStart = false;
    $scope.searchButtonText = "Search";
    $scope.action_status = "Save";
    $scope.AS_ON_DATE = new Date(new Date().setHours(0, 0, 0, 0));

    $scope.save = function (model, index) {

        KYCRemediationFactory.save(model).then(function (success) {
            alert("ok");
        });
    }

   
    $scope.GetGridRecord = function (Search_Text) {
        debugger;
        $scope.searchButtonText = "Searching";
        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));

        KYCRemediationFactory.getGridDetail(Search_Text).then(function (success) {
            $scope.searchButtonText = "Search";
            $scope.kyc_remediation_grid_data = success.data;
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


    $scope.CheckboxActiity = function (index, ischecked, status) {
        debugger;
        if (status === 1) {
            for (var k = 0; k < $scope.kyc_remediation_grid_data.length; k++) {
                if (ischecked) {
                    $scope.kyc_remediation_grid_data[k].SELECT_STATUS = true;
                    $scope.isAssignBatchStart = true;
                }
                else {
                    $scope.kyc_remediation_grid_data[k].SELECT_STATUS = false;
                }
            }
        }
        else {
            if (ischecked) {
                $scope.kyc_remediation_grid_data[index].SELECT_STATUS = true;
                $scope.isAssignBatchStart = true;
            }
            else {
                $scope.kyc_remediation_grid_data[index].SELECT_STATUS = false;
            }
        }
    };
}]);
app.factory("KYCRemediationFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        $q.all([
            //CommonFactory.getSecurityTypes(),
            //CommonFactory.getCommentTypes()
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getGridDetail = function (Search_Text) {
        return $http.post(('/KYCRemediation/GetGridDetail/'), { Search_Text: Search_Text });
    }
    this.save = function (model) {
        return $http.post(('/KYCRemediation/Save/'), { model: model });
    }
    return this;
}]);