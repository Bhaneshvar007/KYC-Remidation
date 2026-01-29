/** @type {import('ag-grid-community').GridOptions} */

agGrid.LicenseManager.setLicenseKey('');
const gridOptions = {
    columnDefs: [
        { field: 'ZONE', rowGroup: true, enableRowGroup: true },
        { field: 'REGION', rowGroup: true, enableRowGroup: true, enablePivot: true },
        //{ field: 'UFC_NAME', rowGroup: true, enableRowGroup: true, enablePivot: true },
        //{ field: 'sport' },
        { field: 'FOLIO_COUNT', aggFunc: 'sum' }
        //{ field: 'silver', aggFunc: 'sum' },
        //{ field: 'bronze', aggFunc: 'sum' },
    ],
    defaultColDef: {
        flex: 1,
        minWidth: 150,
        sortable: true,
        resizable: true,
    },
    autoGroupColumnDef: {
        minWidth: 250,
    },
    sideBar: 'columns',
};

function onBtNormal() {
    gridOptions.columnApi.setPivotMode(false);
    gridOptions.columnApi.applyColumnState({
        state: [
            { colId: 'ZONE', rowGroup: true },
            { colId: 'REGION', rowGroup: true },
            //{ colId: 'UFC_NAME', rowGroup: true },
        ],
        defaultState: {
            pivot: false,
            rowGroup: false,
        },
    });
}

function onBtPivotMode() {
    gridOptions.columnApi.setPivotMode(true);
    gridOptions.columnApi.applyColumnState({
        state: [
            { colId: 'ZONE', rowGroup: true },
            { colId: 'REGION', rowGroup: true },
            //{ colId: 'UFC_NAME', rowGroup: true },
        ],
        defaultState: {
            pivot: false,
            rowGroup: false,
        },
    });
}

function onBtFullPivot() {
    gridOptions.columnApi.setPivotMode(true);
    gridOptions.columnApi.applyColumnState({
        state: [
            { colId: 'ZONE', rowGroup: true },
            { colId: 'REGION', pivot: true },
            //{ colId: '', pivot: true },
        ],
        defaultState: {
            pivot: false,
            rowGroup: false,
        },
    });
}

// setup the grid after the page has finished loading
document.addEventListener('DOMContentLoaded', function () {
    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);

//    $.ajax({
//        type: "POST",
//        url: "/SummaryRegionZoneWiseRpt/GetPivotTable",
//        data: '{}',
//        //headers: { 'X-Button-Name': 'LeaderBoardAll' },
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: OnSuccess,
//        failure: function (response) {
//            alert(response.d);
//        },
//        error: function (response) {
//            alert(response.d);
//        }
//    });
//});


    fetch('/SummaryRegionZoneWiseRpt/GetPivotTable')
        .then((response) => response.json())
        .then((data) => {
            let resData = data;

            gridOptions.api.setRowData(data), console.log(resData)
        });
    
});
