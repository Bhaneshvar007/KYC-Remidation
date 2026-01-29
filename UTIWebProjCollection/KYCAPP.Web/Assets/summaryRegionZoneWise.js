app.controller("summaryRegionZoneWise", ["$scope", "summaryRegionZoneWiseReportFactory", "$timeout", "$location", "$window", function ($scope, summaryRegionZoneWiseReportFactory, $timeout, $location, $window) {

    $scope.searchButtonText = "Search";

    $scope.from_date = new Date(new Date().setHours(0, 0, 0, 0));
    $scope.report_date = new Date(new Date().setHours(0, 0, 0, 0));

    $scope.GetGridRecord = function (report_date) {
        debugger;
        $scope.searchButtonText = "Searching";
        $scope.asOnDate = new Date(new Date().setHours(0, 0, 0, 0));

        summaryRegionZoneWiseReportFactory.getGridDetail(report_date).then(function (success) {
            $scope.searchButtonText = "Search";
            debugger;
            $scope.summary_regionzone_grid_data = success.data;
            //$timeout(() => {
            //  var data = $('#table_id').DataTable({
            //    lengthMenu: [[5, 25, 50, 100, 200, -1], [5, 25, 50, 100, 200, "All"]],
            //  bDestroy: true
            //}).row(this).data();
            //})
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


}]);
app.factory("summaryRegionZoneWiseReportFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        $q.all([
            // write here to page load
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getGridDetail = function (report_date) {
        return $http.post(('/KYC_uat/SummaryRegionZoneWiseRpt/GetGridDetail/'), { report_date: report_date });
    }
    return this;
}]);