//var selectedCheckboxes = [];

//$(document).ready(function () {

//    $('#select-all-checkbox').on('change', function () {
//        var isChecked = $(this).is(':checked');
//        $('.row-checkbox').prop('checked', isChecked);
//        updateSelectedCheckboxes();
//    });

//    // Individual row checkbox change event handler
//    $(document).on('change', '.row-checkbox', function () {
//        var index = $(this).data('index');
//        if ($(this).is(':checked')) {
//            selectedCheckboxes[index] = true;
//        } else {
//            delete selectedCheckboxes[index];
//        }
//        updateSelectAllCheckbox();
//    });

//    // Update the Select All checkbox based on the selected checkboxes
//    function updateSelectAllCheckbox() {
//        var allChecked = $('.row-checkbox').length === Object.keys(selectedCheckboxes).length;
//        $('#select-all-checkbox').prop('checked', allChecked);
//    }

//    // Update the selected checkboxes based on the Select All checkbox
//    function updateSelectedCheckboxes() {
//        if ($('#select-all-checkbox').is(':checked')) {
//            $('.row-checkbox').each(function () {
//                var index = $(this).data('index');
//                selectedCheckboxes[index] = true;
//            });
//        } else {
//            selectedCheckboxes = [];
//        }
//    }

//    var table = $('#tbl_id_abridgedRpt').DataTable({
//        serverSide: true, // Enable server-side processing
//        ajax: {
//            url: '/KYCRecordStatusAbridgedRpt/GetEKYCAbrdigedDataListing', // Specify the server-side endpoint to fetch data
//            type: 'POST',
//            data: function (data) {
//                // Include search, filter, and sorting parameters
//                data.searchText = data.search.value;
//                data.sortColumn = data.columns[data.order[0].column].data;
//                data.sortDirection = data.order[0].dir;
//                data.start = data.start; // Pass the start parameter
//                data.length = data.length; // Pass the length parameter
//                data.p_search_text = $("#p_search_text").val();

//            },

//        },
//        "language": {
//            "emptyTable": "No record found.",
//            "processing":
//                '< div class= "custom-loader" > Loading...</div>'
//        },

//        drawCallback: function () {
//            var api = this.api();
//            api.rows().every(function (rowIdx, tableLoop, rowLoop) {
//                var data = this.data();
//                var index = rowIdx + api.page.info().start;
//                if (selectedCheckboxes[index]) {
//                    $('input.row-checkbox[data-index="' + index + '"]').prop('checked', true);
//                }
//            });
//        },
//        columns: [
//            /* { data: null },*/
//            {
//                data: 'ACNO',
//                render: function (data, type, row, index) {
//                    var currentPage = table.page.info().page; // Get the current page number
//                    var srno = index.row + currentPage * table.page.info().length + 1; // Calculate the serial number
//                    return '<label>' + srno + '</label>';
//                },
//            },
//            {
//                data: 'ACNO',
//                render: function (data, type, row, index) {
//                    //debugger;
//                    var link = '<input type="checkbox" name="single_check" class="row-checkbox sub_chk" data-folio_no="' + row.ACNO + '" />';

//                    return link;
//                }
//            },

//            { data: 'AUM_BRACKET' },
//            { data: 'ACNO' },
//            { data: 'FIRST_HOLDER_NAME' },
//            { data: 'ADDRESS_OF_FIRST_HOLDER' },
//            { data: 'UFC_NAME' },
//            { data: 'ZONEDESC' },
//            { data: 'REGIONDESC' },
//            { data: 'DAYS_OF_SELECTION' },
//            { data: 'FOLIO_STATUS' },
//            { data: 'SELECTED_EMPID' },
//            { data: 'EMPLOYEENAME' },
//            { data: 'SELECTION_DATE' },
//            { data: 'NOMINEEFLAG' },
//            { data: 'KYCFLAG' },
//            { data: 'AADHARSEEDINGFLAG' },
//            { data: 'BANK_FLAG' },
//            { data: 'ARN_CODE' },
//            { data: 'ARN_NAME' },
//            { data: 'ARN_MOBILE' },
//            { data: 'ARN_EMAIL' }


//        ],
//        paging: true, // Enable pagination
//        pageLength: 10, // Set the number of records per page
//        initComplete: function () {
//            var table = this.api();

//            // Add filtering
//            table.columns().every(function () {

//                var column = this;


//                var input = $('<input type="text" style="width:150px;" class="text_box"/>')
//                    .appendTo($("thead tr:eq(1) td").eq(this.index()))
//                    .on("keyup", function () {
//                        column.search($(this).val()).draw();
//                    });

//                var select = $('<select class="drop-down" style="width:150px;"><option value=""> Select </option></select>')
//                    .appendTo($("thead tr:eq(2) td").eq(this.index()))
//                    .on('change', function () {
//                        var val = $.fn.dataTable.util.escapeRegex(
//                            $(this).val()
//                        );

//                        column
//                            .search(val ? '^' + val + '$' : '', true, false)
//                            .draw();
//                    });

//                column.data().unique().sort().each(function (d, j) {
//                    select.append('<option value="' + d + '">' + d + '</option>')
//                });

//            });
//        }
//    });


//})


app.controller("KYCRecordStatusAbridgedRpt", ["$scope", "KYCRecordStatusAbridgedRptFactory", "$timeout", "$location", "$window", function ($scope, KYCRecordStatusAbridgedRptFactory, $timeout, $location, $window) {

    $scope.searchButtonText = "Search";

    //$scope.from_date = new Date(new Date().setHours(0, 0, 0, 0));
    //$scope.to_date = new Date(new Date().setHours(0, 0, 0, 0));

    $scope.GetGridRecord = function (FOLIONO_SEARCH, NOMINEEFLAG_SEARCH, KYCFLAG_SEARCH, UFC_NAME_SEARCH, EMP_NAME_SEARCH, P_SEARCH_TEXT, PRE_POST_2008) {
        //debugger;

        var aadhar_flag = $('#aadhar_flag').val();
        var bank_flag = $('#bank_flag').val();
        var aum_br = $('#aum_br').val();
        var foliostatus = $('#folio_status').val();

        $scope.search_data = { FOLIONO_SEARCH: FOLIONO_SEARCH, NOMINEEFLAG_SEARCH: NOMINEEFLAG_SEARCH, KYCFLAG_SEARCH: KYCFLAG_SEARCH, AADHARSEEDINGFLAG_SEARCH: aadhar_flag, BANK_FLAG_SEARCH: bank_flag, AUM_BRACKET_SEARCH: aum_br, UFC_NAME_SEARCH: UFC_NAME_SEARCH, EMP_NAME_SEARCH: EMP_NAME_SEARCH, P_SEARCH_TEXT: P_SEARCH_TEXT, PRE_POST_2008: PRE_POST_2008, FOLIO_STATUS: foliostatus }
        console.log("Search data ", $scope.search_data);
        $('#table_id').dataTable().fnDestroy();
        //$('#tbl_id_abridgedRpt').dataTable().fnDestroy();
        $scope.searchButtonText = "Searching";

        KYCRecordStatusAbridgedRptFactory.getGridDetail($scope.search_data).then(function (success) {
            $scope.searchButtonText = "Search";
            //debugger;
            $scope.KYCRecordStatusAbridgedReport_Data = success.data;
            $timeout(function () {
                $(document).ready(function () {
                    $(".drop-down").remove();
                    $(".text_box").remove();
                    // Create the DataTable
                    var table = $("#table_id").DataTable({

                        "aoColumnDefs": [
                            { 'bSortable': false, 'aTargets': [1] }
                        ],
                        scrollY: 300,
                        scrollX: true,
                        scrollCollapse: true,
                        fixedColumns: true,

                        lengthMenu: [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
                        orderCellsTop: true,
                        initComplete: function () {
                            var table = this.api();

                            // Add filtering
                            table.columns().every(function () {
                                //debugger;
                                var column = this;
                                console.log(column);

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

                $('select[name="table_id_length"]').on('change', function () {
                    //debugger;

                    var table = $('#table_id').dataTable();
                    $('input', table.fnGetNodes()).prop('checked', false);
                    $('input:checked').removeAttr('checked');
                    //$('input:checked').prop('checked', false);
                    for (var k = 0; k < $scope.KYCRecordStatusAbridgedReport_Data.length; k++) {

                        if ($scope.KYCRecordStatusAbridgedReport_Data[k].SELECT_STATUS == true) {

                            $scope.KYCRecordStatusAbridgedReport_Data[k].SELECT_STATUS = false;
                        }
                    }

                });
            });

            if (success.data.length > 0) {
            }
            else {
                alert("No Record Found !");
            }
        });

    }

    //Cover Letter PDF
    $scope.CoverLetter_pdf_download = function () {
        //debugger;

        var Folio_arr_data = [];

        $(".row-checkbox").each(function () {


            if ($(this).is(":checked")) {

                var acno = $(this).attr("data-folio_no");

                Folio_arr_data.push(acno)


            }

        });



        if (Folio_arr_data.length == 0) {
            alert("Please Select atleast one record to download PDF!");
            return;
        }

        console.log(Folio_arr_data);



        KYCRecordStatusAbridgedRptFactory.pdf_download(Folio_arr_data).then(function (success) {

            $scope.Cover_letter_pdf = success.data;

            $window.open("KYCRecordStatusAbridgedRpt/DownloadCoverLetter/", "_blank");

        });
    }



    //For Download Pdf
    $scope.pdf_download = function () {
        //debugger;

        var Folio_arr_data = [];

        $(".row-checkbox").each(function () {


            if ($(this).is(":checked")) {

                var acno = $(this).attr("data-folio_no");

                Folio_arr_data.push(acno)


            }

        });


        //$scope.is_record_selected = $scope.KYCRecordStatusAbridgedReport_Data.filter(function (el) {
        //    return el.SELECT_STATUS == true;
        //});
        if (Folio_arr_data.length == 0) {
            alert("Please Select atleast one record to download PDF!");
            return;
        }

        console.log(Folio_arr_data);


        //$scope.pdf_downloadButtonText = "Please wait..";
        KYCRecordStatusAbridgedRptFactory.pdf_download(Folio_arr_data).then(function (success) {
            //$scope.pdf_downloadButtonText = "Downloading";
            $scope.PDF_DATA = success.data;

            $window.open("KYCRecordStatusAbridgedRpt/DownloadPDF/", "_blank");

        });
    }

    // Get Employee Dropdown
    $scope.getEmployees = function (p_ufc_name) {

        KYCRecordStatusAbridgedRptFactory.getEmployees(p_ufc_name).then(function (success) {

            $scope.employee_list = success.data;

        });
    }



    KYCRecordStatusAbridgedRptFactory.init(
        function (success) {


            //debugger;
            $scope.ufc_list = success[0].data;
            $scope.AUM_Bracket_list = success[1].data;

        });


}]);
app.factory("KYCRecordStatusAbridgedRptFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        $q.all([
            // write here to page load

            this.getUFc(),
            this.getAUMBraket()
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getGridDetail = function (objSe) {
        return $http.post(('/KYCRecordStatusAbridgedRpt/GetGridDetail/'), { objSe: objSe });
    }
    this.pdf_download = function (P_FOLIO_NO) {
        var config = {
            headers: {
                'X-Button-Name': 'PdfDownload'
            }
        };
        return $http.post(('/KYCRecordStatusAbridgedRpt/Bind_data/'), { P_FOLIO_NO: P_FOLIO_NO }, config);
    }
    this.getEmployees = function (p_ufc_name) {
        return $http.post(('/KYCRecordStatusAbridgedRpt/Get_Employee_List/'), { p_ufc_name: p_ufc_name });
    }
    this.getUFc = function () {
        return $http.post(('/KYCRecordStatusAbridgedRpt/Get_UFC_List/'));
    }
    this.getAUMBraket = function () {
        return $http.post(('/ZoneSummaryReport/GetAumBracketList/'));
    }
    return this;
}]);