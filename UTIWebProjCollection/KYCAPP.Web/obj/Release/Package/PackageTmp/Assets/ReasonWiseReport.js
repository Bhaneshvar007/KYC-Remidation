app.controller("ReasonWiseReport", ["$scope", "ReasonWiseReportFactory", "$timeout", "$location", "$window", function ($scope, ReasonWiseReportFactory, $timeout, $location, $window) {

    $scope.searchButtonText = "Search";
    $scope.is_hide = false;
    $scope.is_show = true;
    $scope.is_show_popup = false;
    $scope.is_show_region_grid = false;
    $scope.is_show_zone_grid = false;
    $scope.is_show_ufc_grid = false;
    $scope.is_show_emp_grid = false;

    $scope.report_date = new Date(new Date().setHours(0, 0, 0, 0));

    $scope.GetGridRecord = function (report_date) {
        document.getElementById("screen_mode").value = "ZH_LEVEL";
        $scope.searchButtonText = "Searching";

        $('#table_id').dataTable().fnDestroy();

        ReasonWiseReportFactory.getGridDetail(report_date).then(function (success) {
            $scope.searchButtonText = "Search";
            //debugger;
            $scope.ReasonWiseReport_DATA_Zone = success.data;

            $timeout(() => {
                var data = $('#table_id').DataTable({
                    lengthMenu: [[5, 10, 25, 50, 100, 200, -1], [5, 10, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {

                $scope.is_show_zone_grid = true;
                $scope.is_show_region_grid = false;
                $('#table_id_ufc_wrapper').hide();
            }
            else {
                $('#table_id_wrapper').hide();
                alert("No Record Found !");
            }
        });


    }


    $scope.back_Button = function () {
        //debugger;
        var screen_mode = document.getElementById("screen_mode").value;
        $scope.report_date = new Date(new Date().setHours(0, 0, 0, 0));

        var report_date = $scope.report_date;
        if (screen_mode == "RH_LEVEL") {
            document.getElementById("screen_mode").value = "ZH_LEVEL";
            $scope.is_show_zone_grid = true;
            $('.main').removeClass("ng-hide");
            $('#table_id_wrapper').css("display", "block");
            $('#table_id_Region_wrapper').hide();

        }
        else if (screen_mode == "UFC_LEVEL") {
            document.getElementById("screen_mode").value = "RH_LEVEL";
            $('#table_id_ufc_wrapper').hide();
            $('#table_id_Region_wrapper').css("display", "block");
            $('.main_region').removeClass("ng-hide");


        }
        else if (screen_mode == "EMPL_LEVEL") {
            document.getElementById("screen_mode").value = "UFC_LEVEL";
            $('#table_id_emp_wrapper').hide();
            $('#table_id_ufc_wrapper').css("display", "block");
            $('.main_ufc').removeClass("ng-hide");

        }
    }


    $scope.GetGridRecord_Region = function (report_date, p_zone) {
        //debugger;
        document.getElementById("screen_mode").value = "RH_LEVEL";
        $scope.searchButtonText = "Searching";

        $('#table_id_Region').dataTable().fnDestroy();

        ReasonWiseReportFactory.GetGridDetail_Region(report_date, p_zone).then(function (success) {
            $scope.searchButtonText = "Search";
            $scope.ReasonWiseReport_DATA_Region = success.data;

            $timeout(() => {
                var data = $('#table_id_Region').DataTable({
                    lengthMenu: [[5, 10, 25, 50, 100, 200, -1], [5, 10, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $scope.is_show_zone_grid = false;
                $scope.is_show_region_grid = true;
                $('#table_id_wrapper').hide();

            }
            else {
                $('#table_id_Region_wrapper').hide();
                alert("No Record Found !");
            }
        });


    }
    $scope.GetGridDetail_UFC = function (report_date, p_zone, p_region) {
        //debugger;
        document.getElementById("screen_mode").value = "UFC_LEVEL";
        $scope.searchButtonText = "Searching";

        $('#table_id_ufc').dataTable().fnDestroy();

        ReasonWiseReportFactory.GetGridDetail_UFC(report_date, p_zone, p_region).then(function (success) {
            $scope.searchButtonText = "Search";
            $scope.ReasonWiseReport_DATA_UFC = success.data;

            $timeout(() => {
                var data = $('#table_id_ufc').DataTable({
                    lengthMenu: [[5, 10, 25, 50, 100, 200, -1], [5, 10, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $scope.is_show_zone_grid = false;
                $scope.is_show_region_grid = false;
                $scope.is_show_ufc_grid = true;
                $('#table_id_wrapper').hide();
                $('#table_id_Region_wrapper').hide();

            }
            else {
                $('#table_id_ufc_wrapper').hide();
                alert("No Record Found !");
            }
        });


    }

    $scope.GetGridDetail_EMP = function (report_date, p_zone, p_region, p_ufc) {
        //debugger;
        document.getElementById("screen_mode").value = "EMPL_LEVEL";
        $scope.searchButtonText = "Searching";

        $('#table_id_emp').dataTable().fnDestroy();

        ReasonWiseReportFactory.GetGridDetail_EMP(report_date, p_zone, p_region, p_ufc).then(function (success) {
            $scope.searchButtonText = "Search";
            $scope.ReasonWiseReport_DATA_EMP = success.data;

            $timeout(() => {
                var data = $('#table_id_emp').DataTable({
                    lengthMenu: [[5, 10, 25, 50, 100, 200, -1], [5, 10, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $scope.is_show_zone_grid = false;
                $scope.is_show_region_grid = false;
                $scope.is_show_ufc_grid = false;
                $scope.is_show_emp_grid = true;
                $('#table_id_wrapper').hide();
                $('#table_id_Region_wrapper').hide();
                $('#table_id_ufc_wrapper').hide();
            }
            else {
                $('#table_id_emp_wrapper').hide();
                alert("No Record Found !");
            }
        });


    }

    $scope.GetPopupReasonwiseRept = function (report_date, p_zone, p_region, p_ufc, p_empId, p_remark_code) {


        $scope.searchButtonText = "Searching";

        $('#table_id_popup').dataTable().fnDestroy();

        ReasonWiseReportFactory.GetPopupReasonwiseRept(report_date, p_zone, p_region, p_ufc, p_empId, p_remark_code).then(function (success) {
            $scope.searchButtonText = "Search";
            //debugger;
            $scope.ReasonWiseReport_DATA_pop = success.data;



            $timeout(() => {
                var data = $('#table_id_popup').DataTable({
                    lengthMenu: [[5, 10, 25, 50, 100, 200, -1], [5, 10, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $(".modal").css("display", "block");
                $scope.is_show_popup = true;
            }
            else {
                $('#table_id_popup_wrapper').hide();
                alert("No Record Found !");
            }
        });


    }

}]);
app.factory("ReasonWiseReportFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        $q.all([
            // write here to page load

        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getGridDetail = function (report_date) {
        return $http.post(('/ReasonWiseReport/GetGridDetail/'), { report_date: report_date });
    }
    this.GetGridDetail_Region = function (report_date, p_zone) {
        return $http.post(('/ReasonWiseReport/GetGridDetail_Region/'), { report_date: report_date, p_zone: p_zone });
    }
    this.GetGridDetail_UFC = function (report_date, p_zone, p_region) {
        return $http.post(('/ReasonWiseReport/GetGridDetail_UFC/'), { report_date: report_date, p_zone: p_zone, p_region: p_region });
    }
    this.GetGridDetail_EMP = function (report_date, p_zone, p_region, p_ufc) {
        return $http.post(('/ReasonWiseReport/GetGridDetail_Emp/'), { report_date: report_date, p_zone: p_zone, p_region: p_region, p_ufc: p_ufc });
    }
    this.GetPopupReasonwiseRept = function (report_date, p_zone, p_region, p_ufc, p_empId, p_remark_code) {
        return $http.post(('/ReasonWiseReport/GetPopupReasonwiseRept/'), { report_date: report_date, p_zone: p_zone, p_region: p_region, p_ufc: p_ufc, p_empId: p_empId, p_remark_code: p_remark_code });
    }

    return this;
}]);