app.controller("Report", ["$scope", "ReportFactory", "CommonFactory", "$timeout", "$location", "$window", function ($scope, ReportFactory, CommonFactory, $timeout, $location, $window) {
    $scope.searchButtonText = "Search";
    $scope.isExportBtnHide = true;
    $scope.FROM_DATE = new Date(new Date().setHours(0, 0, 0, 0));
    $scope.TO_DATE = new Date(new Date().setHours(0, 0, 0, 0));
    $scope.is_send_mail_allowed = false;
    $scope.is_download_excel_allowed = false;
    //$scope.reportTypes = [{ "ID": 0, "Name": "Select" }, { "ID": 1, "Name": "Non Participation" }, { "ID": 2, "Name": "Outlier" }, { "ID": 3, "Name": "Polling_Data" }]
    // $scope.ReportType = $scope.reportTypes[0];


    $scope.$watch("ReportType", function (newVal, oldVal) {
        //debugger;
        if (typeof newVal == "undefined" || newVal == 0)
            return;
        if (newVal == oldVal)
            return;
        $scope.report_grid_data = null;
        $scope.isExportBtnHide = true;
        $("#table_id_1_wrapper").hide();
        $("#table_id_2_wrapper").hide();
        $("#table_id_3_wrapper").hide();
        //if ($("#table_id_1_wrapper").length>0) {
        //    $("#table_id_1_wrapper").empty();
        //}
        //if ($("#table_id_2_wrapper").length > 0) {
        //    $("#table_id_2_wrapper").empty();
        //}
        //if ($("#table_id_2_wrapper").length > 0) {
        //    $("#table_id_3_wrapper").empty();
        //}




        if (newVal.Name === "Polling_Data") {
            CommonFactory.getfiles($scope.FROM_DATE, $scope.TO_DATE).then(function (success) {
                $scope.file_list = success.data;
            });
        }
    });
    $scope.reloadRoute = function () {
        $window.location.reload();
    }

    $scope.GetGridRecord = function (FROM_DATE, TO_DATE, ReportType) {
        //debugger;
        $scope.searchButtonText = "Searching";
        ReportFactory.getGridDetail(FROM_DATE, TO_DATE, ReportType).then(function (success) {
            //debugger;
            $scope.searchButtonText = "Search";
            $scope.report_grid_data = success.data;
            if (ReportType === "Polling_Data" && success.data.length > 0) {

                $("#table_id_3").dataTable({
                    bLengthChange: true,
                    lengthMenu: [[5, 10, -1], [5, 10, "All"]],
                    bFilter: true,
                    bSort: true,
                    bPaginate: true,
                    bDestroy: true,
                    data: success.data,
                    columns: [
                        {
                            'data': 'ID',
                            "render": function (data, type, full, meta) {
                                success.data[meta.row].id = meta.row + 1; // adds id to dataset
                                return meta.row + 1; // adds id to serial no
                            }
                        },
                        { 'data': 'Issuer_Name' },
                        { 'data': 'ISIN' },
                        {
                            'data': 'Maturity_Date',
                            "render": function (data) {
                                var date = new Date(data);
                                var month = date.getMonth() + 1;
                                return date.getDate() + "/" + (month.toString().length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                            }
                        },
                        { 'data': 'Put_Call' },
                        { 'data': 'Rating' },
                        { 'data': 'Structure' },
                        { 'data': 'Reason_for_poll' },
                        { 'data': 'Traded_Yeild' },
                        { 'data': 'Quantum_CR' },
                        { 'data': 'Yesterday_Valuation' },
                        { 'data': 'Clasification' },
                        { 'data': 'Polling_Level' },
                        { 'data': 'Polling_Range' },
                        { 'data': 'Comment_Type' },
                        { 'data': 'Comments_For_Poll_Provider' },
                        { 'data': 'Modify_By' },
                        { 'data': 'Modify_Disp_Date' }]
                });
            }
            else if (ReportType === "Outlier" && success.data.length > 0) {


                $("#table_id_2").dataTable({
                    bLengthChange: true,
                    lengthMenu: [[5, 10, -1], [5, 10, "All"]],
                    bFilter: true,
                    bSort: true,
                    bPaginate: true,
                    bDestroy: true,
                    data: success.data,
                    columns: [
                        {
                            'data': 'ID',
                            "render": function (data, type, full, meta) {
                                success.data[meta.row].id = meta.row + 1; // adds id to dataset
                                return meta.row + 1; // adds id to serial no
                            }
                        },
                        {
                            'data': 'Date_Of_Polling',
                            "render": function (data) {
                                var date = new Date(data);
                                var month = date.getMonth() + 1;
                                return date.getDate() + "/" + (month.toString().length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                            }
                        },
                        { 'data': 'Issuer_Name' },
                        { 'data': 'ISIN' },
                        {
                            'data': 'Maturity_date',
                            "render": function (data) {
                                var date = new Date(data);
                                var month = date.getMonth() + 1;
                                return date.getDate() + "/" + (month.toString().length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                            }
                        },
                        { 'data': 'Structure' },
                        { 'data': 'Reason_for_poll' },
                        { 'data': 'Traded_Yeild' },
                        { 'data': 'Quantum_CR' },
                        { 'data': 'Yesterday_Valuation' },
                        { 'data': 'Clasification' },
                        { 'data': 'Polling_Range' },
                        { 'data': 'Comments_For_Poll_Provider' },
                        { 'data': 'Val_IN_BPS' },
                        { 'data': 'Polling_Level' },
                        { 'data': 'Median' },
                        { 'data': 'diff_Level_N_Median' },
                        { 'data': 'basisValue' },
                        { 'data': 'Rating' },
                        { 'data': 'Rating_Value' },
                        { 'data': 'is_outlier' },]
                });


            }
            else if (ReportType === "Non Participation" && success.data.length > 0) {



                $("#table_id_1").dataTable({
                    bLengthChange: true,
                    lengthMenu: [[5, 10, -1], [5, 10, "All"]],
                    bFilter: true,
                    bSort: true,
                    bPaginate: true,
                    bDestroy: true,
                    data: success.data,
                    columns: [
                        {
                            'data': 'ID',
                            "render": function (data, type, full, meta) {
                                success.data[meta.row].id = meta.row + 1; // adds id to dataset
                                return meta.row + 1; // adds id to serial no
                            }
                        },
                        {
                            'data': 'Date_of_polling',
                            "render": function (data) {
                                var date = new Date(data);
                                var month = date.getMonth() + 1;
                                return date.getDate() + "/" + (month.toString().length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                            }
                        },
                        { 'data': 'Agency_CRISIL_ICRA' },
                        { 'data': 'Secutity_ISIN' },
                        { 'data': 'Security_description' },
                        { 'data': 'Rating' },
                        { 'data': 'Last_valuation_by_CRISIL_ICRA' },
                        { 'data': 'Yield_polled' },
                        { 'data': 'Comments_For_Poll_Provider' }]
                });
            }
            if (success.data.length < 1) {

                alert("No record found !")
            }
            else {
                $scope.isExportBtnHide = false;
            }
        });
    }
    $scope.sendmail = function (FROM_DATE, TO_DATE, ReportType, FileNM) {
        //debugger;
        $scope.todaysdate = new Date(new Date().setHours(0, 0, 0, 0));
        if ((FROM_DATE.getTime() !== TO_DATE.getTime()) || (FROM_DATE.getTime() !== $scope.todaysdate.getTime())) {
            $scope.ReportType = $scope.reportTypes[0];
            alert("From date and To date should be same and should be the today's date !");
            return;
        }
        if (ReportType === "Polling_Data" && FileNM === undefined) {
            alert("Please select the file !");
            return;
        }
        ReportFactory.sendmail(FROM_DATE, TO_DATE, ReportType, FileNM).then(function (success) {
            //debugger;
            if (success.data.length > 0) {
                if (success.data.ERR_MSG === "Success") {
                    alert("Email sent successfully !")
                    return;
                }
                else if (success.data.ERR_MSG === "Pending") {
                    alert("Unable to send mail ,Data is pending for update .Please update the data first !")
                    return;
                }
                else {
                    alert("Failed to send email !");
                    return;
                }

            }
        });
    }
    ReportFactory.init(
        function (success) {
            //debugger;
            $scope.reportTypes = success[0].data;
            $scope.ReportType = $scope.reportTypes[0];


            for (var i = 0; i < $scope.reportTypes.length; i++) {
                if ($scope.reportTypes[i].is_send_mail_allowed) {
                    $scope.is_send_mail_allowed = true;
                }
                if ($scope.reportTypes[i].is_download_excel_allowed) {
                    $scope.is_download_excel_allowed = true;
                }
            }
        });
}]);
app.factory("ReportFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        var urls = window.location.href.split('?')[0].split('/');
        if (!isNaN(urls[urls.length - 1]))
            id = eval(urls[urls.length - 1]);

        $q.all([
            CommonFactory.getReportAccess()
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getGridDetail = function (FROM_DATE, TO_DATE, ReportType) {
        return $http.post(('/Report/GetGridDetail/'), { FROM_DATE: FROM_DATE.toJSON(), TO_DATE: TO_DATE.toJSON(), ReportType: ReportType });
    }
    this.sendmail = function (FROM_DATE, TO_DATE, ReportType, FileNM) {
        return $http.post(('/Report/SendEmail/'), { FROM_DATE: FROM_DATE.toJSON(), TO_DATE: TO_DATE.toJSON(), ReportType: ReportType, FileNM: FileNM });
    }
    return this;
}]);