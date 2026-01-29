app.controller("KYCRemediationCM", ["$scope", "KYCRemediationCMFactory", "$timeout", "$location", "$window", function ($scope, KYCRemediationCMFactory, $timeout, $location, $window) {

    $scope.allCheckBoxStatus = "Select All";
    $scope.BATCH_NUMBER = 1001;

    $scope.isAssignBtnHide = true;

    $scope.isAssignBatchStart = false;
    $scope.searchButtonText = "Search";
    $scope.updateButtonText = "Assign";
    $scope.isDropdownshow = false;

    $scope.action_status = "Save";
    $scope.Search_Text = null;
    //$scope.P_AUM_BRACKET = 'All';
    // $scope.arrP_AUM_BRACKET = ['All', 'Less Than 1 L', 'Between 1L and 2L', 'Between 2L and 5L', 'Between 5L and 10L', 'Between 10L and 15L', 'Between 15L and 20L', 'Between 20L and 25L', 'Between 25L and 1Cr', 'More than 1 Cr'];

    $scope.AS_ON_DATE = new Date(new Date().setHours(0, 0, 0, 0));

    $scope.save = function (selected_employee_code) {

        if (selected_employee_code === undefined || selected_employee_code === "") {
            alert("Please select employee");
            return;
        }
        $scope.is_record_selected = $scope.kyc_remediation_grid_data_cm.filter(function (el) {
            return el.SELECT_STATUS == true;
        });
        if ($scope.is_record_selected.length == 0) {
            alert("Press Assign Button After Selection Of Records For Campaign !");
            return;
        }

        $scope.filter_data = [];

        for (var i = 0; i < $scope.is_record_selected.length; i++) {

            var sr_no = $scope.is_record_selected[i].SRNO;
            var slct_status = $scope.is_record_selected[i].SELECT_STATUS;
            $scope.filter_data.push({ SRNO: sr_no, SELECT_STATUS: slct_status });
        }
        //console.log($scope.filter_data);

        $scope.updateButtonText = "Please wait..";
        KYCRemediationCMFactory.save($scope.filter_data, selected_employee_code).then(function (success) {
            $scope.updateButtonText = "Assign";
            if (success.data.is_success) {
                alert(success.data.msg);
                window.location.href = "/KYC/KYCRemediationCM";
            }
            else {
                alert(success.data.msg);
                return;
            }

        });
    }

    $scope.GetGridRecord = function (Search_Text, P_AUM_BRACKET) {
        ////debugger;

        if (P_AUM_BRACKET == '' || P_AUM_BRACKET == undefined) {
            P_AUM_BRACKET = 'All'
        }

        $('#table_id').dataTable().fnDestroy();
        $scope.searchButtonText = "Searching";
        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));

        KYCRemediationCMFactory.getGridDetail(Search_Text, P_AUM_BRACKET).then(function (success) {
            ////debugger;
            $scope.searchButtonText = "Search";

            if (success.data.length > 10000) {
                alert("Records not displayed due to huge number. Please add a search criteria to reduce the number of records")
                window.location.href = "/KYC/KYCRemediationCM";
                return;
            }
            $scope.kyc_remediation_grid_data_cm = success.data;

            $timeout(() => {
                var data = $('#table_id').DataTable({
                    lengthMenu: [[5, 25, 50, 100, 200, -1], [5, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();

                $('select[name="table_id_length"]').on('change', function () {
                    ////debugger;

                    var table = $('#table_id').dataTable();
                    $('input', table.fnGetNodes()).prop('checked', false);
                    $('input:checked').removeAttr('checked');
                    //$('input:checked').prop('checked', false);
                    for (var k = 0; k < $scope.kyc_remediation_grid_data_cm.length; k++) {

                        if ($scope.kyc_remediation_grid_data_cm[k].SELECT_STATUS == true) {

                            $scope.kyc_remediation_grid_data_cm[k].SELECT_STATUS = false;
                        }
                    }

                    //$('input[type="checkbox"]', table.cells().nodes()).prop('checked',false); 




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
            $scope.isAssignBtnHide = false;
            for (var k = 0; k < records_selected; k++) {
                if (ischecked) {
                    $scope.kyc_remediation_grid_data_cm[k].SELECT_STATUS = true;
                    $scope.isAssignBatchStart = true;
                }
                else {
                    $scope.kyc_remediation_grid_data_cm[k].SELECT_STATUS = false;
                }
            }
        }
        else {
            if (ischecked) {
                $scope.kyc_remediation_grid_data_cm[index].SELECT_STATUS = true;
                $scope.isAssignBatchStart = true;
            }
            else {
                $scope.kyc_remediation_grid_data_cm[index].SELECT_STATUS = false;
            }
        }
    };
    KYCRemediationCMFactory.init(
        function (success) {
            ////debugger;
            $scope.employee_list = success[0].data;
            //$scope.totalCount = success[0].data;

        });
}]);
app.factory("KYCRemediationCMFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {

        $q.all([
            this.getEmployees(),

        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getGridDetail = function (Search_Text, P_AUM_BRACKET) {
        var config = {
            headers: {
                'X-Button-Name': 'Search'
            }
        };
        return $http.post(('/KYCRemediationCM/GetGridDetail/'), { Search_Text: Search_Text, P_AUM_BRACKET: P_AUM_BRACKET }, config);
    }
    this.getEmployees = function () {
        return $http.post(('/KYCRemediationCM/GetEmpCodeDrodown/'));
    }
    this.save = function (model, selected_employee_code) {
        var config = {
            headers: {
                'X-Button-Name': 'Update'
            }
        };
        return $http.post(('/KYCRemediationCM/Save/'), { model: model, selected_employee_code: selected_employee_code }, config);
    }
    return this;
}]);