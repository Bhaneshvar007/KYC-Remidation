app.controller("AllIndiaSummaryRpt", ["$scope", "AllIndiaSummaryRptFactory", "$timeout", "$location", "$window", function ($scope, AllIndiaSummaryRptFactory, $timeout, $location, $window) {

    $scope.searchButtonText = "Search";
    $scope.is_diplay_zonal_smry_tbl = false;
    $scope.is_display_regionalsmryTblZoneWise = false;
    $scope.is_display_UFCSumryTblRegionWise = false;
    $scope.is_display_EMPSumryTblUFCWise = false;

    $scope.is_dis_reportdate = false;
    $scope.is_aum_disable = false;
    $scope.is_dis_search_btn = false;
    $scope.Screen_Mode = null;
    $scope.is_show_countwise_grid = false;
    $scope.is_display_regionalsmryTblZoneWise_popup = false;
    $scope.is_diplay_zonal_smry_tbl__popup = false;
    $scope.is_display_UFCSumryTblRegionWise_pop = false;
    $scope.is_display_EMPSumryTblUFCWise_popup = false;
    $scope.is_show_Historytbl_popup = false;
    $scope.is_show_back_btn_CountWise = false;
    $scope.is_show_back_btn_main = false;

    // $scope.report_date = new Date(new Date().setHours(0, 0, 0, 0));

    var now = new Date();

    $scope.report_date = new Date(now.setDate(now.getDate() - 1));

    $scope.GetGridRecord = function (report_date, p_aum_bracket) {
        ////debugger;
        if (p_aum_bracket == '' || p_aum_bracket == undefined) {
            p_aum_bracket = 'All'
        }

        document.getElementById("report_dt").value = report_date;

        $scope.searchButtonText = "Searching";

        $('#table_id').dataTable().fnDestroy();

        AllIndiaSummaryRptFactory.getGridDetail(report_date, p_aum_bracket).then(function (success) {
            $scope.searchButtonText = "Search";
            ////debugger;
            $scope.AllIndia_Summary_Report_Data = success.data;

            //Here check the role bases data
            let EMP_ROLE = $('#SessionValue').val();


            if (EMP_ROLE == '' || EMP_ROLE == null) {
                EMP_ROLE = "RM";
                //$('#screen_mode').val() = "EMPL_LEVEL";
                //$scope.Screen_Mode = "EMPL_LEVEL";
                document.getElementById("screen_mode").value = "EMPL_LEVEL";
            }

            if (EMP_ROLE == "RH") {
                document.getElementById("screen_mode").value = "RH_LEVEL";
                $scope.getGridDetailGet_RegionalSummary_RH_Login();

            }
            else if (EMP_ROLE == "CM") {
                document.getElementById("screen_mode").value = "UFC_LEVEL";
                $scope.getGridDetailGet_UFCSummary_CM_Login();
            }
            else if (EMP_ROLE == "RM") {
                document.getElementById("screen_mode").value = "EMPL_LEVEL";
                $scope.getGridDetailGet_EmployeeSummary_RM_Login();
            }
            else if (EMP_ROLE == "ZH") {
                document.getElementById("screen_mode").value = "ZH_LEVEL";
                $scope.Zonal_Summary_data = [];
                $scope.Total_ZonalSummary = [];
                for (var i = 0; i < $scope.AllIndia_Summary_Report_Data.length; i++) {
                    if ($scope.AllIndia_Summary_Report_Data[i].ZONE_UTI !== 'Grand Total') {

                        $scope.Zonal_Summary_data.push($scope.AllIndia_Summary_Report_Data[i]);
                    }
                    if ($scope.AllIndia_Summary_Report_Data[i].ZONE_UTI == 'Grand Total') {

                        $scope.Total_ZonalSummary.push($scope.AllIndia_Summary_Report_Data[i]);
                    }
                }


                $timeout(() => {
                    var data = $('#table_id').DataTable({
                        lengthMenu: [[5, 25, 50, 100, 200, -1], [5, 25, 50, 100, 200, "All"]],
                        bDestroy: true
                    }).row(this).data();

                })



                if (success.data.length > 0) {
                    $('#table_id1_wrapper').hide();
                    $('#table_id2_wrapper').hide();
                    $('#table_id3_wrapper').hide();
                    $scope.is_diplay_zonal_smry_tbl = true;

                    $scope.is_dis_reportdate = true;
                    $scope.is_aum_disable = true;
                    $scope.is_dis_search_btn = true;

                }
                else {

                    $('#table_id_wrapper').hide();
                    alert("No Record Found !");
                }
            }

            else if (EMP_ROLE == "ADMIN") {
                document.getElementById("screen_mode").value = "ZH_LEVEL";
                $scope.Zonal_Summary_data = [];
                $scope.Total_ZonalSummary = [];

                for (var i = 0; i < $scope.AllIndia_Summary_Report_Data.length; i++) {
                    if ($scope.AllIndia_Summary_Report_Data[i].ZONE_UTI !== 'Grand Total') {

                        $scope.Zonal_Summary_data.push($scope.AllIndia_Summary_Report_Data[i]);
                    }
                    if ($scope.AllIndia_Summary_Report_Data[i].ZONE_UTI == 'Grand Total') {

                        $scope.Total_ZonalSummary.push($scope.AllIndia_Summary_Report_Data[i]);
                    }
                }


                $timeout(() => {
                    var data = $('#table_id').DataTable({
                        lengthMenu: [[5, 25, 50, 100, 200, -1], [5, 25, 50, 100, 200, "All"]],
                        bDestroy: true
                    }).row(this).data();

                })



                if (success.data.length > 0) {
                    $('#table_id1_wrapper').hide();
                    $('#table_id2_wrapper').hide();
                    $('#table_id3_wrapper').hide();
                    $scope.is_diplay_zonal_smry_tbl = true;

                    $scope.is_dis_reportdate = true;
                    $scope.is_aum_disable = true;
                    $scope.is_dis_search_btn = true;

                }
                else {

                    $('#table_id_wrapper').hide();
                    alert("No Record Found !");
                }
            }
        });

    }

    $scope.getGridDetailOF_Reginal_Smry_ZoneWiseData = function (ZONE_UTI) {
        ////debugger;
        $scope.searchButtonText = "Searching";
        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));
        $('#table_id1').dataTable().fnDestroy();

        document.getElementById("screen_mode").value = "RH_LEVEL";
        document.getElementById("zone_name").value = ZONE_UTI;

        AllIndiaSummaryRptFactory.getGridDetailOF_Reginal_Smry_ZoneWiseData(ZONE_UTI).then(function (success) {
            $scope.searchButtonText = "Search";
            ////debugger;
            $scope.Regional_SummaryGrid_ZoneWiseData = success.data;

            $scope.RegionalSummaryDataZoneWise = [];
            $scope.Total_RegionalSummaryDataZoneWise = [];
            for (var i = 0; i < $scope.Regional_SummaryGrid_ZoneWiseData.length; i++) {
                if ($scope.Regional_SummaryGrid_ZoneWiseData[i].REGION_NAME_UTI !== 'Grand Total') {

                    $scope.RegionalSummaryDataZoneWise.push($scope.Regional_SummaryGrid_ZoneWiseData[i]);
                }
                if ($scope.Regional_SummaryGrid_ZoneWiseData[i].REGION_NAME_UTI == 'Grand Total') {

                    $scope.Total_RegionalSummaryDataZoneWise.push($scope.Regional_SummaryGrid_ZoneWiseData[i]);
                }
            }


            $timeout(() => {
                var data = $('#table_id1').DataTable({
                    lengthMenu: [[5, 25, 50, 100, 200, -1], [5, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {

                $('#table_id_wrapper').hide();
                $scope.is_display_regionalsmryTblZoneWise = true;
                $scope.is_diplay_zonal_smry_tbl = false;
                $scope.is_display_EMPSumryTblUFCWise = false;

            }
            else {
                $('#table_id1_wrapper').hide();
                alert("No Record Found !");
            }
        });
    }



    $scope.getGridDetailUFCSummaryReportRegionWise = function (REGION_NAME_UTI, ZONE_UTI) {
        ////debugger;
        $scope.searchButtonText = "Searching";

        $('#table_id2').dataTable().fnDestroy();

        document.getElementById("screen_mode").value = "UFC_LEVEL";
        document.getElementById("zone_name").value = ZONE_UTI;
        document.getElementById("region_name").value = REGION_NAME_UTI;

        AllIndiaSummaryRptFactory.getGridDetailUFCSummaryReportRegionWise(REGION_NAME_UTI, ZONE_UTI).then(function (success) {
            $scope.searchButtonText = "Search";
            ////debugger;
            $scope.UFC_Summary_Report_Data = success.data;

            $scope.UFC_Summary_ReportData = [];
            $scope.Total_UFC_Summary_ReportData = [];
            for (var i = 0; i < $scope.UFC_Summary_Report_Data.length; i++) {
                if ($scope.UFC_Summary_Report_Data[i].UFC_NAME !== 'Grand Total') {

                    $scope.UFC_Summary_ReportData.push($scope.UFC_Summary_Report_Data[i]);
                }
                if ($scope.UFC_Summary_Report_Data[i].UFC_NAME == 'Grand Total') {

                    $scope.Total_UFC_Summary_ReportData.push($scope.UFC_Summary_Report_Data[i]);
                }
            }


            $timeout(() => {
                var data = $('#table_id2').DataTable({
                    lengthMenu: [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $('#table_id_wrapper').hide();
                $('#table_id1_wrapper').hide();
                $scope.is_display_regionalsmryTbl = false;
                $scope.is_diplay_zonal_smry_tbl = false;
                $scope.is_display_EMPSumryTblUFCWise = false;
                $scope.is_display_UFCSumryTblRegionWise = true;
            }
            else {
                $('#table_id2_wrapper').hide();
                alert("No Record Found !");
            }
        });


    }


    $scope.getGridDetailEmployeeSummaryReport_UFCWise = function (REGION_NAME_UTI, ZONE_UTI, UFC_NAME) {
        ////debugger;
        $scope.searchButtonText = "Searching";

        $('#table_id3').dataTable().fnDestroy();

        document.getElementById("screen_mode").value = "EMPL_LEVEL";
        document.getElementById("zone_name").value = ZONE_UTI;
        document.getElementById("region_name").value = REGION_NAME_UTI;
        document.getElementById("ufc_name").value = UFC_NAME;

        AllIndiaSummaryRptFactory.getGridDetailEmployeeSummaryReportUFCWise(REGION_NAME_UTI, ZONE_UTI, UFC_NAME).then(function (success) {
            $scope.searchButtonText = "Search";
            ////debugger;
            $scope.EMP_Summary_Report_Data = success.data;

            $scope.Employee_Summary_ReportData = [];
            $scope.Total_Employee_Summary_ReportData = [];
            for (var i = 0; i < $scope.EMP_Summary_Report_Data.length; i++) {
                if ($scope.EMP_Summary_Report_Data[i].NAME !== 'Grand Total') {

                    $scope.Employee_Summary_ReportData.push($scope.EMP_Summary_Report_Data[i]);
                }
                if ($scope.EMP_Summary_Report_Data[i].NAME == 'Grand Total') {

                    $scope.Total_Employee_Summary_ReportData.push($scope.EMP_Summary_Report_Data[i]);
                }
            }

            $timeout(() => {
                var data = $('#table_id3').DataTable({
                    lengthMenu: [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {

                $('#table_id_wrapper').hide();
                $('#table_id1_wrapper').hide();
                $('#table_id2_wrapper').hide();
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


    // New Function For Role Based
    //For RH Login
    $scope.getGridDetailGet_RegionalSummary_RH_Login = function () {
        ////debugger;
        $scope.searchButtonText = "Searching";
        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));
        $('#table_id1').dataTable().fnDestroy();

        AllIndiaSummaryRptFactory.getGridDetailGet_RegionalSummary_RH_Login().then(function (success) {
            $scope.searchButtonText = "Search";
            ////debugger;
            $scope.Regional_SummaryGrid_ZoneWiseData = success.data;

            $scope.RegionalSummaryDataZoneWise = [];
            $scope.Total_RegionalSummaryDataZoneWise = [];

            for (var i = 0; i < $scope.Regional_SummaryGrid_ZoneWiseData.length; i++) {
                if ($scope.Regional_SummaryGrid_ZoneWiseData[i].REGION_NAME_UTI !== 'Grand Total') {

                    $scope.RegionalSummaryDataZoneWise.push($scope.Regional_SummaryGrid_ZoneWiseData[i]);
                }
                if ($scope.Regional_SummaryGrid_ZoneWiseData[i].REGION_NAME_UTI == 'Grand Total') {

                    $scope.Total_RegionalSummaryDataZoneWise.push($scope.Regional_SummaryGrid_ZoneWiseData[i]);
                }
            }


            if ($scope.is_diplay_zonal_smry_tbl = true) {

                $('#table_id_wrapper').hide();

            }


            $timeout(() => {
                var data = $('#table_id1').DataTable({
                    lengthMenu: [[5, 25, 50, 100, 200, -1], [5, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $scope.is_dis_reportdate = true;
                $scope.is_aum_disable = true;
                $scope.is_dis_search_btn = true;

                $scope.is_display_regionalsmryTblZoneWise = true;
                $scope.is_diplay_zonal_smry_tbl = false;
                $scope.is_display_EMPSumryTblUFCWise = false;

            }
            else {
                $('#table_id1_wrapper').hide();
                alert("No Record Found !");
            }
        });
    }


    // For CM login
    $scope.getGridDetailGet_UFCSummary_CM_Login = function () {
        ////debugger;
        $scope.searchButtonText = "Searching";

        $('#table_id2').dataTable().fnDestroy();

        AllIndiaSummaryRptFactory.getGridDetailGet_UFCSummary_CM_Login().then(function (success) {
            $scope.searchButtonText = "Search";
            ////debugger;
            $scope.UFC_Summary_Report_Data = success.data;

            $scope.UFC_Summary_ReportData = [];
            $scope.Total_UFC_Summary_ReportData = [];
            for (var i = 0; i < $scope.UFC_Summary_Report_Data.length; i++) {
                if ($scope.UFC_Summary_Report_Data[i].UFC_NAME !== 'Grand Total') {

                    $scope.UFC_Summary_ReportData.push($scope.UFC_Summary_Report_Data[i]);
                }

                if ($scope.UFC_Summary_Report_Data[i].UFC_NAME == 'Grand Total') {

                    $scope.Total_UFC_Summary_ReportData.push($scope.UFC_Summary_Report_Data[i]);
                }
            }


            $timeout(() => {
                var data = $('#table_id2').DataTable({
                    lengthMenu: [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $scope.is_dis_reportdate = true;
                $scope.is_aum_disable = true;
                $scope.is_dis_search_btn = true;
                $('#table_id_wrapper').hide();
                $('#table_id1_wrapper').hide();
                $scope.is_display_regionalsmryTbl = false;
                $scope.is_diplay_zonal_smry_tbl = false;
                $scope.is_display_EMPSumryTblUFCWise = false;
                $scope.is_display_UFCSumryTblRegionWise = true;
            }
            else {
                $('#table_id2_wrapper').hide();
                alert("No Record Found !");
            }
        });

    }


    //For RM Login
    $scope.getGridDetailGet_EmployeeSummary_RM_Login = function () {
        ////debugger;
        $scope.searchButtonText = "Searching";

        $('#table_id3').dataTable().fnDestroy();

        AllIndiaSummaryRptFactory.getGridDetailGet_EmployeeSummary_RM_Login().then(function (success) {
            $scope.searchButtonText = "Search";
            ////debugger;
            $scope.EMP_Summary_Report_Data = success.data;

            $scope.Employee_Summary_ReportData = [];
            $scope.Total_Employee_Summary_ReportData = [];
            for (var i = 0; i < $scope.EMP_Summary_Report_Data.length; i++) {
                if ($scope.EMP_Summary_Report_Data[i].NAME !== 'Grand Total') {

                    $scope.Employee_Summary_ReportData.push($scope.EMP_Summary_Report_Data[i]);
                }
                if ($scope.EMP_Summary_Report_Data[i].NAME == 'Grand Total') {

                    $scope.Total_Employee_Summary_ReportData.push($scope.EMP_Summary_Report_Data[i]);
                }
            }


            $timeout(() => {
                var data = $('#table_id3').DataTable({
                    lengthMenu: [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $scope.is_dis_reportdate = true;
                $scope.is_aum_disable = true;
                $scope.is_dis_search_btn = true;
                $('#table_id_wrapper').hide();
                $('#table_id1_wrapper').hide();
                $('#table_id2_wrapper').hide();
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


    // Get Grid details of count wise data
    //CountWise_Data

    $scope.getGridCountWise_Data = function (count_type, p_zone, p_region, p_ufc_name, p_emp_id) {
        ////debugger;
        document.getElementById("CountWise_screen").value = "COUNT_WISE_PAGE";
        if (count_type == "A") {
            $scope.is_show_ActionFolio = true;
        }
        else {

            $scope.is_show_ActionFolio = false;
        }
        $scope.searchButtonText = "Searching";
        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));
        $('#table_id').dataTable().fnDestroy();

        AllIndiaSummaryRptFactory.getGridCountWise_Data(count_type, p_zone, p_region, p_ufc_name, p_emp_id).then(function (success) {

            $scope.searchButtonText = "Search";
            ////debugger;
            $scope.CountWise_Data = success.data;


            $timeout(() => {
                var data = $('#table_id_count').DataTable({
                    lengthMenu: [[5, 25, 50, 100, 200, -1], [5, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $('#table_id2_wrapper').hide();
                $('#table_id3_wrapper').hide();
                $scope.is_show_countwise_grid = true;
                $scope.is_display_UFCSumryTblRegionWise = false;
                $scope.is_display_EMPSumryTblUFCWise = false;
                $scope.is_show_back_btn_CountWise = true;
                $scope.is_show_back_btn_main = false;
            }
            else {
                alert("No Record Found !");
            }
        });
    }

    // Popup Zone Summary zonewise
    $scope.GetPopup_ZoneSummary_Rpt_zonewise = function (p_zone_uti) {
        ////debugger;
        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));
        $('#table_id_popup').dataTable().fnDestroy();

        AllIndiaSummaryRptFactory.GetPopup_ZoneSummary_Rpt_zonewise(p_zone_uti).then(function (success) {
            $scope.searchButtonText = "Search";
            ////debugger;
            $scope.ZoneSummaryDataZoneWise_pop = success.data;

            $scope.ZoneSummaryDataZone_Wise_pop = [];
            $scope.Total_ZoneSummaryDataZone_Wise_pop = [];

            for (var i = 0; i < $scope.ZoneSummaryDataZoneWise_pop.length; i++) {
                if ($scope.ZoneSummaryDataZoneWise_pop[i].ZONE_UTI !== 'Grand Total') {

                    $scope.ZoneSummaryDataZone_Wise_pop.push($scope.ZoneSummaryDataZoneWise_pop[i]);
                }
                if ($scope.ZoneSummaryDataZoneWise_pop[i].ZONE_UTI == 'Grand Total') {

                    $scope.Total_ZoneSummaryDataZone_Wise_pop.push($scope.ZoneSummaryDataZoneWise_pop[i]);
                }
            }



            $timeout(() => {
                var data = $('#table_id_popup').DataTable({
                    lengthMenu: [[5, 25, 50, 100, 200, -1], [5, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {

                $(".modal").css("display", "block");
                $scope.is_diplay_zonal_smry_tbl__popup = true;

            }
            else {

                alert("No Record Found !");
            }
        });
    }
    // Popup Regional Summary zonewise
    $scope.GetPopup_Regional_Summary_zonewise = function (p_zone_uti, p_region_name) {
        ////debugger;
        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));
        $('#table_id1_popup').dataTable().fnDestroy();

        AllIndiaSummaryRptFactory.GetPopup_Regional_Summary_zonewise(p_zone_uti, p_region_name).then(function (success) {
            $scope.searchButtonText = "Search";
            ////debugger;
            $scope.RegionalSummaryDataZoneWise_pop = success.data;

            $scope.RegionalSummaryDataZone_Wise_pop = [];
            $scope.Total_RegionalSummaryDataZone_Wise_pop = [];

            for (var i = 0; i < $scope.RegionalSummaryDataZoneWise_pop.length; i++) {
                if ($scope.RegionalSummaryDataZoneWise_pop[i].REGION_NAME_UTI !== 'Grand Total') {

                    $scope.RegionalSummaryDataZone_Wise_pop.push($scope.RegionalSummaryDataZoneWise_pop[i]);
                }
                if ($scope.RegionalSummaryDataZoneWise_pop[i].REGION_NAME_UTI == 'Grand Total') {

                    $scope.Total_RegionalSummaryDataZone_Wise_pop.push($scope.RegionalSummaryDataZoneWise_pop[i]);
                }
            }



            $timeout(() => {
                var data = $('#table_id1_popup').DataTable({
                    lengthMenu: [[5, 25, 50, 100, 200, -1], [5, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $('#table_id_popup_wrapper').hide();
                $(".modal").css("display", "block");
                $scope.is_display_regionalsmryTblZoneWise_popup = true;
                $scope.is_diplay_zonal_smry_tbl__popup = false;

            }
            else {

                alert("No Record Found !");
            }
        });
    }

    // Popup UFC Summary regionwise
    $scope.Get_PopupDetail_UFC_Summary = function (p_zone_uti, p_region_name, p_ufc_name) {
        ////debugger;
        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));
        $('#table_id2_popup').dataTable().fnDestroy();

        AllIndiaSummaryRptFactory.Get_PopupDetail_UFC_Summary(p_zone_uti, p_region_name, p_ufc_name).then(function (success) {
            $scope.searchButtonText = "Search";
            ////debugger;
            $scope.UFC_Summary_Report_Data_popup = success.data;

            $scope.UFC_Summary_Report_Data_pop = [];
            $scope.Total_UFC_Summary_ReportData_pop = [];

            for (var i = 0; i < $scope.UFC_Summary_Report_Data_popup.length; i++) {
                if ($scope.UFC_Summary_Report_Data_popup[i].UFC_NAME !== 'Grand Total') {

                    $scope.UFC_Summary_Report_Data_pop.push($scope.UFC_Summary_Report_Data_popup[i]);
                }
                if ($scope.UFC_Summary_Report_Data_popup[i].UFC_NAME == 'Grand Total') {

                    $scope.Total_UFC_Summary_ReportData_pop.push($scope.UFC_Summary_Report_Data_popup[i]);
                }
            }



            $timeout(() => {
                var data = $('#table_id2_popup').DataTable({
                    lengthMenu: [[5, 25, 50, 100, 200, -1], [5, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $('#table_id1_popup_wrapper').hide();
                $('#table_id_popup_wrapper').hide();
                $(".modal").css("display", "block");
                $scope.is_display_regionalsmryTblZoneWise_popup = false;
                $scope.is_diplay_zonal_smry_tbl__popup = false;
                $scope.is_display_UFCSumryTblRegionWise_pop = true;

            }
            else {

                alert("No Record Found !");
            }
        });
    }

    // pop up for RM 
    $scope.Get_POP_EmployeeSummary = function (p_zone_uti, p_region_name, p_ufc_name, p_emp_id) {
        ////debugger;
        $scope.searchButtonText = "Searching";

        $('#table_id3_popup').dataTable().fnDestroy();

        AllIndiaSummaryRptFactory.Get_POP_EmployeeSummary(p_zone_uti, p_region_name, p_ufc_name, p_emp_id).then(function (success) {
            $scope.searchButtonText = "Search";
            ////debugger;
            $scope.EMP_Summary_Report_Data_popup = success.data;

            $scope.Employee_Summary_ReportData_pop = [];
            $scope.Total_Employee_Summary_ReportData_pop = [];
            for (var i = 0; i < $scope.EMP_Summary_Report_Data_popup.length; i++) {
                if ($scope.EMP_Summary_Report_Data_popup[i].NAME !== 'Grand Total') {

                    $scope.Employee_Summary_ReportData_pop.push($scope.EMP_Summary_Report_Data_popup[i]);
                }
                if ($scope.EMP_Summary_Report_Data_popup[i].NAME == 'Grand Total') {

                    $scope.Total_Employee_Summary_ReportData_pop.push($scope.EMP_Summary_Report_Data_popup[i]);
                }
            }


            $timeout(() => {
                var data = $('#table_id3_popup').DataTable({
                    lengthMenu: [[5, 25, 50, 100, 200, -1], [5, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $('#table_id_popup_wrapper').hide();
                $('#table_id1_popup_wrapper').hide();
                $('#table_id2_popup_wrapper').hide();
                $(".modal").css("display", "block");
                $scope.is_display_regionalsmryTblZoneWise_popup = false;
                $scope.is_diplay_zonal_smry_tbl__popup = false;
                $scope.is_display_UFCSumryTblRegionWise_pop = false;
                $scope.is_display_EMPSumryTblUFCWise_popup = true;
            }
            else {

                alert("No Record Found !");
            }
        });


    }
    // Get Popup History table
    $scope.GetHistory_popup_acno_wise = function (p_acno) {
        ////debugger;
        $scope.searchButtonText = "Searching";

        $('#table_id_History_popup').dataTable().fnDestroy();

        AllIndiaSummaryRptFactory.GetHistory_popup_acno_wise(p_acno).then(function (success) {
            $scope.searchButtonText = "Search";
            ////debugger;
            $scope.Historytbl_data = success.data;



            $timeout(() => {
                var data = $('#table_id_History_popup').DataTable({
                    lengthMenu: [[5, 25, 50, 100, 200, -1], [5, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })

            if (success.data.length > 0) {
                $('#table_id_popup_wrapper').hide();
                $('#table_id1_popup_wrapper').hide();
                $('#table_id2_popup_wrapper').hide();
                $('#table_id3_popup_wrapper').hide();
                $(".modal").css("display", "block");
                $scope.is_display_regionalsmryTblZoneWise_popup = false;
                $scope.is_diplay_zonal_smry_tbl__popup = false;
                $scope.is_display_UFCSumryTblRegionWise_pop = false;
                $scope.is_display_EMPSumryTblUFCWise_popup = false;
                $scope.is_show_Historytbl_popup = true;
            }
            else {

                alert("No Record Found !");
            }
        });


    }



    //Back Functionality
    $scope.back_Button = function () {
        ////debugger;
        var screen_mode = document.getElementById("screen_mode").value;
        $scope.report_date = new Date(new Date().setHours(0, 0, 0, 0));
        let EMP_ROLE = $('#SessionValue').val();



        // For RH Level
        if (EMP_ROLE == "RH") {
            if (screen_mode == "UFC_LEVEL") {
                document.getElementById("screen_mode").value = "RH_LEVEL";
                $('#table_id2_wrapper').hide();
                $('#table_id1_wrapper').css("display", "block");
                $('#table_id1').removeClass("ng-hide");


            }
            else if (screen_mode == "EMPL_LEVEL") {
                document.getElementById("screen_mode").value = "UFC_LEVEL";
                $('#table_id3_wrapper').hide();
                $('#table_id2_wrapper').css("display", "block");
                $('#table_id2').removeClass("ng-hide");

            }
        }

        // For CM Level
        if (EMP_ROLE == "CM") {
            if (screen_mode == "EMPL_LEVEL") {
                document.getElementById("screen_mode").value = "UFC_LEVEL";
                $('#table_id3_wrapper').hide();
                $('#table_id2_wrapper').css("display", "block");
                $('#table_id2').removeClass("ng-hide");

            }
        }

        // For ZH Level
        if (EMP_ROLE == "ZH" || EMP_ROLE == "ADMIN") {

            if (screen_mode == "RH_LEVEL") {
                document.getElementById("screen_mode").value = "ZH_LEVEL";
                $('#table_id').removeClass("ng-hide");
                $('#table_id_wrapper').css("display", "block");
                $('#table_id1_wrapper').hide();

            }
            else if (screen_mode == "UFC_LEVEL") {
                document.getElementById("screen_mode").value = "RH_LEVEL";
                $('#table_id2_wrapper').hide();
                $('#table_id1_wrapper').css("display", "block");
                $('#table_id1').removeClass("ng-hide");


            }
            else if (screen_mode == "EMPL_LEVEL") {
                document.getElementById("screen_mode").value = "UFC_LEVEL";
                $('#table_id3_wrapper').hide();
                $('#table_id2_wrapper').css("display", "block");
                $('#table_id2').removeClass("ng-hide");

            }
        }
    }

    $scope.back_Button_Countwise = function () {

        let EMP_ROLE = $('#SessionValue').val();
        if (EMP_ROLE == '' || EMP_ROLE == null) {
            EMP_ROLE = "RM";
        }
        if (EMP_ROLE == "CM") {
            if (screen_mode == "UFC_LEVEL" && CountWisePage == "COUNT_WISE_PAGE") {
                document.getElementById("screen_mode").value = "UFC_LEVEL";
                $('#table_id_count_wrapper').hide();
                $('#table_id2_wrapper').css("display", "block");
                $('#table_id2').removeClass("ng-hide");

            }

            if (screen_mode == "EMPL_LEVEL" && CountWisePage == "COUNT_WISE_PAGE") {
                document.getElementById("screen_mode").value = "EMPL_LEVEL";
                $('#table_id_count_wrapper').hide();
                $('#table_id3_wrapper').css("display", "block");
                $('#table_id3').removeClass("ng-hide");

            }

        }

        if (EMP_ROLE == "RM") {
            if (screen_mode == "EMPL_LEVEL" && CountWisePage == "COUNT_WISE_PAGE") {
                document.getElementById("screen_mode").value = "EMPL_LEVEL";
                $('#table_id_count_wrapper').hide();
                $('#table_id3_wrapper').css("display", "block");
                $('#table_id3').removeClass("ng-hide");

            }
        }
    }


    AllIndiaSummaryRptFactory.init(
        function (success) {
            ////debugger;
            $scope.AUM_Bracket_list = success[0].data;

        });

}]);
app.factory("AllIndiaSummaryRptFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {

        $q.all([

            this.getAUMBraket(),
            // write here to page load
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getGridDetail = function (report_date, p_aum_bracket) {
        var config = {
            headers: {
                'X-Button-Name': 'Search'
            }
        };
        return $http.post(('/ZoneSummaryReport/GetGridDetail_AllIndiaSummaryRpt/'), { report_date: report_date, p_aum_bracket: p_aum_bracket }, config);
    }
    this.getGridDetailOF_Reginal_Smry_ZoneWiseData = function (ZONE_UTI) {
        return $http.post(('/ZoneSummaryReport/GetGridDetail_Regional_Summary_zonewise/'), { zone_uti: ZONE_UTI });
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
    this.getGridDetailGet_RegionalSummary_RH_Login = function () {
        return $http.post(('/ZoneSummaryReport/Get_RegionalSummary_RH_Login/'));
    }
    this.getGridDetailGet_UFCSummary_CM_Login = function () {
        return $http.post(('/ZoneSummaryReport/Get_UFCSummary_CM_Login/'));
    }
    this.getGridDetailGet_EmployeeSummary_RM_Login = function () {
        return $http.post(('/ZoneSummaryReport/Get_EmployeeSummary_RM_Login/'));
    }
    this.getGridCountWise_Data = function (count_type, p_zone, p_region, p_ufc_name, p_emp_id) {
        return $http.post(('/ZoneSummaryReport/GetGridDetail_CountWise_ZonalSummaryRept/'), { count_type: count_type, p_zone: p_zone, p_region: p_region, p_ufc_name: p_ufc_name, p_emp_id: p_emp_id });
    }
    //POP UP DATA
    this.GetPopup_ZoneSummary_Rpt_zonewise = function (p_zone_uti) {
        return $http.post(('/ZoneSummaryReport/GetPopup_ZoneSummary_Rpt_zonewise/'), { p_zone_uti: p_zone_uti });
    }
    this.GetPopup_Regional_Summary_zonewise = function (p_zone_uti, p_region_name) {
        return $http.post(('/ZoneSummaryReport/GetPopup_Regional_Summary_zonewise/'), { p_zone_uti: p_zone_uti, p_region_name: p_region_name });
    }
    this.Get_PopupDetail_UFC_Summary = function (p_zone_uti, p_region_name, p_ufc_name) {
        return $http.post(('/ZoneSummaryReport/Get_PopupDetail_UFC_Summary/'), { p_zone_uti: p_zone_uti, p_region_name: p_region_name, p_ufc_name: p_ufc_name });
    }
    this.Get_POP_EmployeeSummary = function (p_zone_uti, p_region_name, p_ufc_name, p_emp_id) {
        return $http.post(('/ZoneSummaryReport/Get_POP_EmployeeSummary/'), { p_zone_uti: p_zone_uti, p_region_name: p_region_name, p_ufc_name: p_ufc_name, p_emp_id: p_emp_id });
    }
    this.GetHistory_popup_acno_wise = function (p_acno) {
        return $http.post(('/ZoneSummaryReport/GetHistory_popup_acno_wise/'), { p_acno: p_acno });
    }





    return this;
}]);


//app.controller("CountWiseData", ["$scope", "CountWiseDataFactory", "$timeout", "$location", "$window", function ($scope, CountWiseDataFactory, $timeout, $location, $window) {
//    //debugger;

//    CountWiseDataFactory.init(
//        function (success) {
//            //debugger; //
//            $scope.CountWise_Data = success[0].data;


//            $timeout(() => {
//                var data = $('#table_id').DataTable({
//                    lengthMenu: [[5, 10, -1], [5, 10, "All"]],
//                    bDestroy: true
//                }).row(this).data();
//            })


//        });
//}]);
//app.factory("CountWiseDataFactory", ["$rootScope", "$http", "$q", function ($rootScope, $http, $q) {
//    this.init = function (success, failure) {
//        //debugger;
//        var unique_no = 0;
//        var urls = window.location.href.split('?')[0].split('/');
//        if (urls.indexOf(urls[urls.length - 1]) > -1)

//            unique_no = urls[urls.length - 1];

//        $q.all([
//            this.getGridDetail(unique_no)
//        ]).then(function (msg) {
//            success(msg);
//        }, failure);
//    }
//    this.getGridDetail = function (unique_no) {
//        return $http.post(('/KYC_UAT/ZoneSummaryReport/GetGridDetail_CountWise_ZonalSummaryRept1/'), { unique_no: unique_no });
//    }

//    return this;
//}]);