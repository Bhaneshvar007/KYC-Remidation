app.controller("UpdateStatusFolio", ["$scope", "UpdateStatusFolioFactory", "$timeout", "$location", "$window", function ($scope, UpdateStatusFolioFactory, $timeout, $location, $window) {


    $scope.isUpdatebutton = true;
    $scope.isUpdateBatchStart = false;
    $scope.searchButtonText = "Search";
    $scope.updateButtonText = "Submit";

    $scope.action_status = "Save";
    $scope.P_REMARK_DATE = new Date(new Date().setHours(0, 0, 0, 0));


    $scope.update_remark = function (index, item) {

        if (item.REMARK_DATE === undefined || item.REMARK_DATE == "") {
            alert("Please Select Remark date");
            return;
        }
        if (item.REMARK_CODE === undefined || item.REMARK_CODE == "") {
            alert("Please Select Remark");
            return;
        }
        if (item.REMARK_COMMENT === undefined || item.REMARK_COMMENT == "") {
            alert("Please Fill Comment");
            return;
        }

        ////debugger;
        //let remark_desc = $('#remark_desc').text();
        //console.log("Selected Remark Comment ",remark_desc);
        //item.REMARK_COMMENT = remark_desc;

        UpdateStatusFolioFactory.update_remark(item).then(function (success) {
            ////debugger;

            if (success.data.is_success) {
                alert(success.data.msg);

                $scope.UpdateStatus_Folio_Data[index].is_disable_button = true;
                //$(".disable_color").css("background-color")

                //window.location.href = "/KYC/UpdateStatusFolio";
            }
            else {
                alert(success.data.msg);
                return;
            }



        });
    }



    $scope.P_Selected_dt = new Date(new Date().setHours(0, 0, 0, 0));
    $scope.GetGridRecord = function (P_Selected_dt, P_ACNO, P_INVNAME) {
        ////debugger;

        $('#table_id').dataTable().fnDestroy();
        $scope.searchButtonText = "Searching";

        UpdateStatusFolioFactory.getGridDetail(P_Selected_dt, P_ACNO, P_INVNAME).then(function (success) {
            $scope.searchButtonText = "Search";
            $scope.UpdateStatus_Folio_Data = success.data;

            for (var k = 0; k < $scope.UpdateStatus_Folio_Data.length; k++) {

                if ($scope.UpdateStatus_Folio_Data[k].REMARK_DATE !== undefined) {
                    if ($scope.UpdateStatus_Folio_Data[k].REMARK_DATE !== "") {
                        if ($scope.UpdateStatus_Folio_Data[k].REMARK_DATE !== null) {


                            //let date = new Date($scope.UpdateStatus_Folio_Data[k].REMARK_DATE);
                            //let year = date.getFullYear();
                            //if (year !== 1900) {
                            $scope.UpdateStatus_Folio_Data[k].REMARK_DATE = new Date($scope.UpdateStatus_Folio_Data[k].REMARK_DATE);
                            //}
                        }
                    }
                }
            }

            $timeout(() => {
                var data = $('#table_id').DataTable({
                    lengthMenu: [[5, 25, 50, 100, 200, -1], [5, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

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


    UpdateStatusFolioFactory.init(
        function (success) {
            ////debugger;
            $scope.remarks_list = success[0].data;

        });
}]);

app.factory("UpdateStatusFolioFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {

        $q.all([
            this.getRemark(),

        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getGridDetail = function (P_Selected_dt, P_ACNO, P_INVNAME) {
        var config = {
            headers: {
                'X-Button-Name': 'Search'
            }
        };

        return $http.post(('/UpdateStatusFolio/GetGridDetail/'), { P_Selected_dt: P_Selected_dt, P_ACNO: P_ACNO, P_INVNAME: P_INVNAME }, config);
    }
    this.getRemark = function () {
        return $http.post(('/UpdateStatusFolio/GetRemarks/'));
    }
    this.update_remark = function (model) {
        var config = {
            headers: {
                'X-Button-Name': 'Update'
            }
        };
        return $http.post(('/UpdateStatusFolio/update_remark/'), { model: model }, config);
    }
    return this;
}]);