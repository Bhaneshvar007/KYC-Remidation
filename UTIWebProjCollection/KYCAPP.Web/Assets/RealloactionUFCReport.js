app.controller("ReallocationUFCRpt", ["$scope", "ReallocationUFCRptFactory", "$timeout", "$location", "$window", function ($scope, ReallocationUFCRptFactory, $timeout, $location, $window) {

    $scope.allCheckBoxStatus = "Select All";
    $scope.is_show_assign_e = false;
    $scope.is_show_assign_z = false;

    $scope.isAssignBtnHide = true;

    $scope.isAssignBatchStart = false;
    $scope.searchButtonText = "Search";
    $scope.updateButtonText = "Confirm";

    $scope.action_status = "Save";
    $scope.is_disable_ufc_dropdown = true;
    $scope.is_disable_region_dropdown = true;
    // $scope.AS_ON_DATE = new Date(new Date().setHours(0, 0, 0, 0));

    $scope.refresh_page = function () {
        if ($scope.Reallocation_Grid_data.length > 0) {

            window.location.reload();
        }
    }

    $scope.save = function (p_zone, p_region, p_ufc, p_slcted_code_email) {
        debugger;
        var selected_employee_code = "";
        var selected_email_id = "";
        if (p_slcted_code_email != null) {
            selected_employee_code = p_slcted_code_email.Code;
            selected_email_id = p_slcted_code_email.EMAIL_ID;
        }
        var ufcname = "";
        var emaiL = "";
        if (p_ufc != null) {

            ufcname = p_ufc.UFC_NAME;
            emaiL = p_ufc.EMAIL_ID;
        }

        var arr_data = [];
        var SRNO_arr_data = [];

        $(".row-checkbox").each(function () {


            if ($(this).is(":checked")) {

                var SRNO = $(this).attr("data-srno");
                var ACNO = $(this).attr("data-acno");
                var INVNAME = $(this).attr("data-invname");
                SRNO_arr_data.push(SRNO);
                arr_data.push({ SRNO: SRNO, ACNO: ACNO, INVNAME: INVNAME })


            }

        });

        if (arr_data.length == 0) {
            alert("Press Assign Button After Selection Of Records For Campaign !");
            return;
        }
        var radio_btn_val = $("input[name='ufc']:checked").val();

        //if ((p_zone === undefined || p_zone === "") && (p_region === undefined || p_region === "") && (p_ufc === undefined || p_ufc === "")) {
        if (radio_btn_val == "sameufc") {

            if (selected_employee_code === undefined || selected_employee_code === "") {
                alert("Please select employee");
                return;
            }
        }
        else {

            if (p_zone === undefined || p_zone === "" || p_zone === null) {

                alert("Please select Zone");
                return;
            }
            if (p_region === undefined || p_region === "" || p_region === null) {

                alert("Please select Region");
                return;
            }
            if (p_ufc === undefined || p_ufc === "" || p_ufc === null) {

                alert("Please select UFC");
                return;
            }

        }


        $scope.Update_data_diff_Ufc = { SRNO: SRNO_arr_data, MAIL_DATA: arr_data, ZONE_UTI: p_zone, REGION_NAME_UTI: p_region, UFC_NAME: ufcname, EMAIL_ID: emaiL }

        $scope.updateButtonText = "Please wait..";
        ReallocationUFCRptFactory.save($scope.Update_data_diff_Ufc, selected_employee_code, selected_email_id).then(function (success) {
            $scope.updateButtonText = "Confirm";
            if (success.data.is_success) {
                alert(success.data.msg);
                window.location.reload();
            }
            else {
                alert(success.data.msg);
                return;
            }

        });

    }


    $scope.GetGridRecord = function (Search_Text, p_aum_bracket) {
        $('#table_id').dataTable().fnDestroy();
        //debugger;
        $scope.searchButtonText = "Searching";
        //debugger;



        ReallocationUFCRptFactory.getGridDetail(Search_Text, p_aum_bracket).then(function (success) {
            $scope.searchButtonText = "Search";
            $scope.Reallocation_Grid_data = success.data;

            var y = document.getElementById("empdetails");
            var z = document.getElementById("grddetails");
            var x = document.getElementById("empgrddetails");

            x.style.display = "block";
            if (document.getElementById('same').checked) {
                y.style.display = "block";
            }
            else {
                y.style.display = "none";
            }
            if (document.getElementById('diff').checked) {
                z.style.display = "block";
            }
            else {
                z.style.display = "none";
            }



            $timeout(() => {
                var data = $('#table_id').DataTable({
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [1] }
                    ],
                    lengthMenu: [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();


                $('select[name="table_id_length"]').on('change', function () {
                    $('#select-all').removeAttr('checked');
                    $(".sub_chk").prop('checked', false);
                });
            })

            if (success.data.length > 0) {
                $scope.is_show_assign_e = true;
                $scope.is_show_assign_z = true;
            }
            else {
                alert("No Record Found !");
            }
        });
    }

    //Region Dropdown
    $scope.region_dropdown = function (p_zone) {


        ReallocationUFCRptFactory.region_dropdown(p_zone).then(function (success) {
            $scope.is_disable_region_dropdown = false;
            $scope.Region_list = success.data;

        });

    }
    //UFC dropdown
    $scope.ufc_dropdown = function (p_zone, p_region) {

        ReallocationUFCRptFactory.ufc_dropdown(p_zone, p_region).then(function (success) {
            $scope.is_disable_ufc_dropdown = false;
            $scope.ufc_list = success.data;

        });

    }



    ReallocationUFCRptFactory.init(
        function (success) {
            //debugger;
            $scope.Zone_list = success[0].data;
            $scope.employee_list = success[1].data;
            //$scope.AUM_Bracket_list = success[2].data;

        });

}]);
app.factory("ReallocationUFCRptFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {

        $q.all([
            // write here to page load
            this.getZone_List(),
            this.getEmployees()
            //this.getAUMBraket(),

        ]).then(function (msg) {
            success(msg);
        }, failure);
    }

    this.getGridDetail = function (Search_Text, p_aum_bracket) {
        var config = {
            headers: {
                'X-Button-Name': 'Search'
            }
        };
        return $http.post(('/ReallocationUFCReport/GetGridDetail/'), { Search_Text: Search_Text, p_aum_bracket: p_aum_bracket }, config);
    }
    this.save = function (model, selected_employee_code, selected_email_id) {
        var config = {
            headers: {
                'X-Button-Name': 'Update'
            }
        };
        return $http.post(('/ReallocationUFCReport/UpdateReallocation/'), { model: model, selected_employee_code: selected_employee_code, selected_email_id: selected_email_id }, config);
    }
    //this.getAUMBraket = function () {
    //    return $http.post(('/KYC_UAT/ZoneSummaryReport/GetAumBracketList/'));
    //}
    this.getZone_List = function () {
        return $http.post(('/ReallocationUFCReport/GetZone_dropdown/'));
    }
    this.region_dropdown = function (p_zone) {
        return $http.post(('/ReallocationUFCReport/Get_region_dropdown/'), { p_zone: p_zone });
    }
    this.ufc_dropdown = function (p_zone, p_region) {
        return $http.post(('/ReallocationUFCReport/Get_ufc_dropdown/'), { p_zone: p_zone, p_region: p_region });
    }
    this.getEmployees = function () {
        return $http.post(('/KYCRemediationCM/GetEmpCodeDrodown/'));
    }
    return this;
}]);