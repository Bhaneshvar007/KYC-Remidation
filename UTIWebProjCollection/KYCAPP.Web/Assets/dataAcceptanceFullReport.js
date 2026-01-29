app.controller("kycDataAcceptanceFullReport", ["$scope", "DataAcceptanceFullReportFactory", "$timeout", "$location", "$window", function ($scope, DataAcceptanceFullReportFactory, $timeout, $location, $window) {

    $scope.searchButtonText = "Search";

    //$scope.from_date = new Date(new Date().setHours(0, 0, 0, 0));
    //$scope.to_date = new Date(new Date().setHours(0, 0, 0, 0));

    //$scope.GetGridRecord = function (from_date, to_date) {
    //    //debugger;
    //    $('#table_id').dataTable().fnDestroy();
    //    $scope.searchButtonText = "Searching";
    //    //$scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));

    //    DataAcceptanceFullReportFactory.getGridDetail(from_date, to_date).then(function (success) {
    //        $scope.searchButtonText = "Search";
    //        $scope.data_acceptance_full_grid_data = success.data;


    //        $timeout(function () {
    //            $(document).ready(function () {
    //                $(".drop-down").remove();
    //                $(".text_box").remove();
    //                // Create the DataTable
    //                var table = $("#table_id").DataTable({
    //                    orderCellsTop: true,
    //                    initComplete: function () {
    //                        var table = this.api();

    //                        // Add filtering
    //                        table.columns().every(function () {
    //                            //debugger;
    //                            var column = this;
    //                            console.log(column);

    //                            var input = $('<input type="text" style="width:150px;" class="text_box"/>')
    //                                .appendTo($("thead tr:eq(1) td").eq(this.index()))
    //                                .on("keyup", function () {
    //                                    column.search($(this).val()).draw();
    //                                });

    //                            var select = $('<select class="drop-down" style="width:150px;"><option value=""> Select </option></select>')
    //                                .appendTo($("thead tr:eq(2) td").eq(this.index()))
    //                                .on('change', function () {
    //                                    var val = $.fn.dataTable.util.escapeRegex(
    //                                        $(this).val()
    //                                    );

    //                                    column
    //                                        .search(val ? '^' + val + '$' : '', true, false)
    //                                        .draw();
    //                                });

    //                            column.data().unique().sort().each(function (d, j) {
    //                                select.append('<option value="' + d + '">' + d + '</option>')
    //                            });

    //                        });
    //                    }

    //                });
    //            })
    //        });

    //        if (success.data.length > 0) {
    //        }
    //        else {
    //            alert("No Record Found !");
    //        }
    //    });
    //}

    $scope.GetGridRecord = function (FOLIONO_SEARCH, NOMINEEFLAG_SEARCH, KYCFLAG_SEARCH, UFC_NAME_SEARCH, EMP_NAME_SEARCH, P_SEARCH_TEXT, PRE_POST_2008) {
        debugger;

        var aadhar_flag = $('#aadhar_flag').val();
        var bank_flag = $('#bank_flag').val();
        var aum_br = $('#aum_br').val();
        var foliostatus = $('#folio_status').val();
        //console.log("aadhar_flag ", aadhar_flag);
        //console.log("bank_flag ", bank_flag);
        //console.log("aum_br ", aum_br);
        //console.log("folio_status ", foliostatus);

        $scope.search_data = { FOLIONO_SEARCH: FOLIONO_SEARCH, NOMINEEFLAG_SEARCH: NOMINEEFLAG_SEARCH, KYCFLAG_SEARCH: KYCFLAG_SEARCH, AADHARSEEDINGFLAG_SEARCH: aadhar_flag, BANK_FLAG_SEARCH: bank_flag, AUM_BRACKET_SEARCH: aum_br, UFC_NAME_SEARCH: UFC_NAME_SEARCH, EMP_NAME_SEARCH: EMP_NAME_SEARCH, P_SEARCH_TEXT: P_SEARCH_TEXT, PRE_POST_2008: PRE_POST_2008, FOLIO_STATUS: foliostatus }
        console.log("Search data ", $scope.search_data);

        $('#table_id').dataTable().fnDestroy();
        $scope.searchButtonText = "Searching";

        DataAcceptanceFullReportFactory.getGridDetail($scope.search_data).then(function (success) {
            $scope.searchButtonText = "Search";
            ////debugger;
            $scope.data_acceptance_full_grid_data = success.data;
            $timeout(function () {
                $(document).ready(function () {
                    $(".drop-down").remove();
                    $(".text_box").remove();
                    // Create the DataTable
                    var table = $("#table_id").DataTable({

                        //"aoColumnDefs": [
                        //    { 'bSortable': false, 'aTargets': [1] }
                        //],
                        //scrollY: 300,
                        //scrollX: true,
                        //scrollCollapse: true,
                        //fixedColumns: true,

                        lengthMenu: [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
                        orderCellsTop: true,
                        initComplete: function () {
                            var table = this.api();

                            // Add filtering
                            table.columns().every(function () {
                                ////debugger;
                                var column = this;


                                var input = $('<input type="text" style="width:150px;" class="text_box"/>')
                                    .appendTo($("thead tr:eq(1) td").eq(this.index()))
                                    .on("keyup", function () {
                                        column.search($(this).val()).draw();
                                    });

                                var select = $('<select class="drop-down" style="width:150px;"><option value=""> Select </option></select>')
                                    .appendTo($("thead tr:eq(2) td").eq(this.index()))
                                    .on('change', function () {
                                        var val = $.fn.dataTable.util.escapeRegex(
                                            $(this).val()
                                        );

                                        column
                                            .search(val ? '^' + val + '$' : '', true, false)
                                            .draw();
                                    });

                                column.data().unique().sort().each(function (d, j) {
                                    select.append('<option value="' + d + '">' + d + '</option>')
                                });

                            });
                        }

                    });
                })


            });

            if (success.data.length > 0) {
            }
            else {
                alert("No Record Found !");
            }
        });

    }
    // Get Employee Dropdown
    $scope.getEmployees = function (p_ufc_name) {

        DataAcceptanceFullReportFactory.getEmployees(p_ufc_name).then(function (success) {

            $scope.employee_list = success.data;

        });
    }



    DataAcceptanceFullReportFactory.init(
        function (success) {


            ////debugger;
            $scope.ufc_list = success[0].data;
            // $scope.AUM_Bracket_list = success[1].data;

        });



}]);
app.factory("DataAcceptanceFullReportFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        $q.all([
            this.getUFc(),
            // write here to page load
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getGridDetail = function (objSearch) {
        var config = {
            headers: {
                'X-Button-Name': 'Search'
            }
        };
        return $http.post(('/DataAcceptanceFullReport/GetGridDetail/'), { objSearch: objSearch }, config);
    }
    this.getEmployees = function (p_ufc_name) {
        return $http.post(('/KYCRecordStatusAbridgedRpt/Get_Employee_List/'), { p_ufc_name: p_ufc_name });
    }
    this.getUFc = function () {
        return $http.post(('/KYCRecordStatusAbridgedRpt/Get_UFC_List/'));
    }

    return this;
}]);