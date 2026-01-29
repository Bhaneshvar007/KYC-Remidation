app.controller("ZoneSummaryReport", ["$scope", "RegionalSummaryReportFactory", "$timeout", "$location", "$window", function ($scope, RegionalSummaryReportFactory, $timeout, $location, $window) {

    $scope.searchButtonText = "Search";
    $scope.is_hide = false;
    $scope.is_show = true;
    $scope.is_display_regionalsmryTbl = false;
    $scope.is_display_UFCSumryTblRegionWise = false;

    $scope.is_dis_reportdate = false;
    $scope.is_aum_disable = false;
    $scope.is_dis_search_btn = false;
    $scope.is_display_EMPSumryTblUFCWise = true;

    $scope.report_date = new Date(new Date().setHours(0, 0, 0, 0));

    $scope.GetGridRecord = function (report_date, p_aum_bracket) {
        //debugger;
        if (p_aum_bracket == '' || p_aum_bracket == undefined) {
            p_aum_bracket = 'All'
        }

        $scope.searchButtonText = "Searching";

        $('#table_id').dataTable().fnDestroy();

        RegionalSummaryReportFactory.getGridDetail(report_date, p_aum_bracket).then(function (success) {
            $scope.searchButtonText = "Search";
            //debugger;
            $scope.Zone_Summary_Report_Data = success.data;

            $scope.Regional_summary_data = [];
            for (var i = 0; i < $scope.Zone_Summary_Report_Data.length; i++) {
                if ($scope.Zone_Summary_Report_Data[i].REGION_NAME_UTI !== 'Grand Total') {

                    $scope.Regional_summary_data.push($scope.Zone_Summary_Report_Data[i]);
                }
            }

            $scope.Total_RegionalSummary = [];
            for (var i = 0; i < $scope.Zone_Summary_Report_Data.length; i++) {
                if ($scope.Zone_Summary_Report_Data[i].REGION_NAME_UTI == 'Grand Total') {

                    $scope.Total_RegionalSummary.push($scope.Zone_Summary_Report_Data[i]);
                }
            }

            $timeout(() => {
                var data = $('#table_id').DataTable({
                    lengthMenu: [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $scope.is_display_regionalsmryTbl = true;
                $('#table_id2_wrapper').hide();

                $scope.is_dis_reportdate = true;
                $scope.is_aum_disable = true;
                $scope.is_dis_search_btn = true;

            }
            else {
                $('#table_id_wrapper').hide();
                alert("No Record Found !");
            }
        });


    }
    $scope.getGridDetailUFCSummaryReportRegionWise = function (REGION_NAME_UTI, ZONE_UTI) {
        //debugger;
        $scope.searchButtonText = "Searching";

        $('#table_id2').dataTable().fnDestroy();

        RegionalSummaryReportFactory.getGridDetailUFCSummaryReportRegionWise(REGION_NAME_UTI, ZONE_UTI).then(function (success) {
            $scope.searchButtonText = "Search";
            //debugger;
            $scope.UFC_Summary_Report_Data = success.data;

            $scope.UFC_Summary_ReportData = [];
            for (var i = 0; i < $scope.UFC_Summary_Report_Data.length; i++) {
                if ($scope.UFC_Summary_Report_Data[i].UFC_NAME !== 'Grand Total') {

                    $scope.UFC_Summary_ReportData.push($scope.UFC_Summary_Report_Data[i]);
                }
            }

            $scope.Total_UFC_Summary_ReportData = [];
            for (var i = 0; i < $scope.UFC_Summary_Report_Data.length; i++) {
                if ($scope.UFC_Summary_Report_Data[i].UFC_NAME == 'Grand Total') {

                    $scope.Total_UFC_Summary_ReportData.push($scope.UFC_Summary_Report_Data[i]);
                }
            }

            if ($scope.is_display_regionalsmryTbl = true) {

                $('#table_id_wrapper').hide();

            }
            $timeout(() => {
                var data = $('#table_id2').DataTable({
                    lengthMenu: [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $scope.is_display_regionalsmryTbl = false;
                $scope.is_display_UFCSumryTblRegionWise = true;
            }
            else {
                $('#table_id2_wrapper').hide();
                alert("No Record Found !");
            }
        });
    }

    $scope.getGridDetailEmployeeSummaryReport_UFCWise = function (REGION_NAME_UTI, ZONE_UTI, UFC_NAME) {
        //debugger;
        $scope.searchButtonText = "Searching";

        $('#table_id3').dataTable().fnDestroy();

        RegionalSummaryReportFactory.getGridDetailEmployeeSummaryReportUFCWise(REGION_NAME_UTI, ZONE_UTI, UFC_NAME).then(function (success) {
            $scope.searchButtonText = "Search";
            //debugger;
            $scope.EMP_Summary_Report_Data = success.data;

            $scope.Employee_Summary_ReportData = [];
            for (var i = 0; i < $scope.EMP_Summary_Report_Data.length; i++) {
                if ($scope.EMP_Summary_Report_Data[i].NAME !== 'Grand Total') {

                    $scope.Employee_Summary_ReportData.push($scope.EMP_Summary_Report_Data[i]);
                }
            }

            $scope.Total_Employee_Summary_ReportData = [];
            for (var i = 0; i < $scope.EMP_Summary_Report_Data.length; i++) {
                if ($scope.EMP_Summary_Report_Data[i].NAME == 'Grand Total') {

                    $scope.Total_Employee_Summary_ReportData.push($scope.EMP_Summary_Report_Data[i]);
                }
            }
            if (($scope.is_diplay_zonal_smry_tbl = true) && ($scope.is_display_regionalsmryTbl = true)) {

                $('#table_id_wrapper').hide();

                $('#table_id2_wrapper').hide();

            }

            $timeout(() => {
                var data = $('#table_id3').DataTable({
                    lengthMenu: [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $scope.is_display_regionalsmryTbl = false;
                $scope.is_diplay_zonal_smry_tbl = false;
                $scope.is_display_UFCSumryTblRegionWise = false;
                $scope.is_display_EMPSumryTblUFCWise = true;
            }
            else {
                $('#table_id3_wrapper').hide();
                alert("No Record Found !");
            }
        });


    }


    RegionalSummaryReportFactory.init(
        function (success) {
            //debugger;
            $scope.AUM_Bracket_list = success[0].data;

        });


}]);
app.factory("RegionalSummaryReportFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        $q.all([
            // write here to page load
            this.getAUMBraket(),
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getGridDetail = function (report_date, p_aum_bracket) {
        return $http.post(('/ZoneSummaryReport/GetGridDetail/'), { report_date: report_date, p_aum_bracket: p_aum_bracket });
    }
    this.getAUMBraket = function () {
        return $http.post(('/ZoneSummaryReport/GetAumBracketList/'));
    }
    this.getGridDetailUFCSummaryReportRegionWise = function (REGION_NAME_UTI, ZONE_UTI) {
        return $http.post(('/ZoneSummaryReport/GetGridDetail_UFC_Summary_RegionWise/'), { region_name_uti: REGION_NAME_UTI, zone_uti: ZONE_UTI });
    }
    this.getGridDetailEmployeeSummaryReportUFCWise = function (REGION_NAME_UTI, ZONE_UTI, UFC_NAME) {
        return $http.post(('/ZoneSummaryReport/getGridDetailEmployeeSummaryReport_UFCWise/'), { region_name_uti: REGION_NAME_UTI, zone_uti: ZONE_UTI, ufc_name: UFC_NAME });
    }
    return this;
}]);