//var app = angular.module('myApp', [])
app.controller("PollingData", ["$scope", "PollingDataFactory", "$timeout", "$location", "$window", function ($scope, PollingDataFactory, $timeout, $location, $window) {

    $scope.allCheckBoxStatus = "Select All";
    $scope.BATCH_NUMBER = 1001;

    $scope.isAssignBtnHide = true;

    $scope.isAssignBatchStart = false;
    $scope.searchButtonText = "Search";
    $scope.action_status = "Save";
    $scope.AS_ON_DATE = new Date(new Date().setHours(0, 0, 0, 0));



    //$scope.$watchGroup(['AS_ON_DATE', 'CLAIM_TYPE'], function (newVal, oldVal) {

    //    //debugger;

    //    if (typeof newVal == "undefined" || newVal == 0)
    //        return;
    //    if (newVal == oldVal)
    //        return;

    //    var asondate = newVal[0];
    //    var claimtype = newVal[1];

    //    $scope.allCheckBoxStatus = "Select All";
    //    $scope.IsCheckedStatus = "checked";

    //    PollingDataFactory.getGridDetail("2020-04-15", claimtype).then(function (success) {
    //        //debugger;
    //        $scope.RevisedExpenseGridData = success.data;
    //    });

    //});



    $scope.save = function (model, index) {
        //debugger;
        if (model.Comment_Type_ID !== 1) {
            if ((model.Polling_Level === "" || model.Polling_Level === undefined) && model.Polling_Range === "" || model.Polling_Range === undefined) {
                alert("Please enter either the polling level or the polling range or both !");
                return;
            }
        }
        else {
            model.Polling_Level = 0;
        }
        if (model.Comment_Type_ID === 0) {
            alert("Please select comment type !")
            return
        }
        if (model.Polling_Level !== null && model.Polling_Level !== "" && model.Comment_Type_ID !== 1) {
            var decimal = /^[1-9]\d*(\.\d+)?$/;
            if (!model.Polling_Level.match(decimal)) {
                alert('Please enter proper value !  numeric value allowed')
                return;
            }
        }

        $scope.Polling_data[index].action_status = "Saving...";

        PollingDataFactory.save(model).then(function (success) {

            $timeout(function () {
                $scope.Polling_data[index].action_status = "Save";
            }, 1000);
            if (success.data.indexOf("Fail") !== -1) {
                alert(success.data);
            }

            //alert(success.data);
            //$window.location.reload();
        });
    }

    $scope.exportToExcel = function (asOnDate, claimType) {

        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));

        //$.ajax(
        //    {
        //        url: '@Url.Action("GetExportToExcel", "PollingData")',
        //        contentType: 'application/json; charset=utf-8',
        //        datatype: 'json',
        //        data: {
        //            studentId: 123
        //        },
        //        type: "POST",
        //        success: function () {
        //            window.location = '@Url.Action("GetExportToExcel", "PollingData", "++")';
        //        }
        //    });

        PollingDataFactory.getExportToExcel(asOnDate, claimType).then(function (success) {
            alert("success");

            // window.location = '@Url.Action("DownloadAttachment", "PostDetail")';
        });
    }

    $scope.GetGridRecord = function (SecurityType) {
        //debugger;
        $scope.searchButtonText = "Searching";
        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));

        PollingDataFactory.getGridDetail(SecurityType).then(function (success) {
            $scope.searchButtonText = "Search";
            $scope.Polling_data = success.data;
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
        //debugger;
        if (status === 1) {
            for (var k = 0; k < $scope.RevisedExpenseGridData.length; k++) {
                if (ischecked) {
                    $scope.RevisedExpenseGridData[k].SELECT_STATUS = true;
                    $scope.isAssignBatchStart = true;
                }
                else {
                    $scope.RevisedExpenseGridData[k].SELECT_STATUS = false;
                }
            }
        }
        else {
            if (ischecked) {
                $scope.RevisedExpenseGridData[index].SELECT_STATUS = true;
                $scope.isAssignBatchStart = true;
            }
            else {
                $scope.RevisedExpenseGridData[index].SELECT_STATUS = false;
            }

        }

        //debugger;
    };
    //$scope.claimTypes = ["Conveyance"]

    $scope.AssignBatchNumber = function (RevisedExpenseGridData) {

        PollingDataFactory.assingBatchNumber(RevisedExpenseGridData).then(function (success) {
            //debugger;
            if (!$scope.isAssignBatchStart) {

                alert("Please select  at least one !");
                return;
            }
            if (success.data.length < 1) {
                alert("Batch Number Assigned Successfully !")
                $window.location.reload();
            }
            else {
                alert(success.data.ERR_MSG);
            }
            // $scope.RevisedExpenseGridData = success.data;
        });
    }

    PollingDataFactory.init(
        function (success) {
            // $scope.model = success[0].data;
            //debugger;
            $scope.securityTypes = success[0].data;
            $scope.commentTypes = success[1].data;
            $scope.SecurityType = $scope.securityTypes[0];
        });



}]);
app.factory("PollingDataFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        $q.all([
            CommonFactory.getSecurityTypes(),
            CommonFactory.getCommentTypes()
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getGridDetail = function (SecurityType) {
        return $http.post(('/PollingData/GetGridDetail/'), { SecurityType: SecurityType });
    }
    this.getExportToExcel = function (AsOnDate, ClaimType) {
        return $http.post(('/PollingData/GetExportToExcel/'), { AsOnDate: AsOnDate.toJSON(), ClaimType: ClaimType });
    }
    this.save = function (model) {
        return $http.post(('/PollingData/Save/'), { model: model });
    }
    this.getClaimType = function () {
        return $http.get('/PollingData/GetDetails/');
    }
    return this;
}]);