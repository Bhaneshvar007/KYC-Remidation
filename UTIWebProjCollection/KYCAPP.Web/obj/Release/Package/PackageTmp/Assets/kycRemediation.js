app.controller("KYCRemediation", ["$scope", "KYCRemediationFactory", "$timeout", "$location", "$window", function ($scope, KYCRemediationFactory, $timeout, $location, $window) {

    $scope.allCheckBoxStatus = "Select All";
    $scope.BATCH_NUMBER = 1001;

    $scope.isAssignBtnHide = true;

    $scope.isAssignBatchStart = false;
    $scope.searchButtonText = "Search";
    $scope.updateButtonText = "Confirm";

    $scope.action_status = "Save";
    $scope.AS_ON_DATE = new Date(new Date().setHours(0, 0, 0, 0));

    $scope.save = function () {
        ////debugger;
        $scope.is_record_selected = $scope.kyc_remediation_grid_data.filter(function (el) {
            return el.SELECT_STATUS == true;
        });
        if ($scope.is_record_selected.length == 0) {
            alert("Press Confirm Button After Selection Of Records For Campaign !");
            return;
        }

        $scope.filter_data = [];

        for (var i = 0; i < $scope.is_record_selected.length; i++) {

            var sr_no = $scope.is_record_selected[i].SRNO;
            var slct_status = $scope.is_record_selected[i].SELECT_STATUS;
            $scope.filter_data.push({ SRNO: sr_no, SELECT_STATUS: slct_status });
        }
        console.log($scope.filter_data);

        $scope.updateButtonText = "Please wait..";
        KYCRemediationFactory.save($scope.filter_data).then(function (success) {
            $scope.updateButtonText = "Confirm";
            if (success.data.is_success) {
                alert(success.data.msg);
                window.location.href = "/KYC/KYCRemediation";
            }
            else {
                alert(success.data.msg);
                return;
            }

        });
    }


    $scope.GetGridRecord = function (Search_Text) {
        $('#table_id').dataTable().fnDestroy();
        ////debugger;
        $scope.searchButtonText = "Searching";
        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));

        KYCRemediationFactory.getGridDetail(Search_Text).then(function (success) {
            $scope.searchButtonText = "Search";
            $scope.kyc_remediation_grid_data = success.data;

            $timeout(() => {
                var data = $('#table_id').DataTable({
                    lengthMenu: [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();


                $('select[name="table_id_length"]').on('change', function () {
                    ////debugger;

                    var table = $('#table_id').dataTable();
                    $('input', table.fnGetNodes()).prop('checked', false);
                    $('input:checked').removeAttr('checked');
                    //$('input:checked').prop('checked', false);
                    for (var k = 0; k < $scope.kyc_remediation_grid_data.length; k++) {

                        if ($scope.kyc_remediation_grid_data[k].SELECT_STATUS == true) {

                            $scope.kyc_remediation_grid_data[k].SELECT_STATUS = false;
                        }
                    }





                    //$('input[type="checkbox"]', table.cells().nodes()).prop('checked',false); 


                    $scope.CheckboxActiity(0, false, 1);

                });
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


    $scope.CheckboxActiity = function (index, ischecked, status) {
        ////debugger;
        var records_selected = $('#table_id').DataTable().page.info().end;

        if (status === 1) {
            for (var k = 0; k < records_selected; k++) {
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
        var config = {
            headers: {
                'X-Button-Name': 'Search'
            }
        };
        return $http.post(('/KYCRemediation/GetGridDetail/'), { Search_Text: Search_Text }, config);
    }
    this.save = function (model) {
        var config = {
            headers: {
                'X-Button-Name': 'Update'
            }
        };
        return $http.post(('/KYCRemediation/Save/'), { model: model }, config);
    }
    return this;
}]);