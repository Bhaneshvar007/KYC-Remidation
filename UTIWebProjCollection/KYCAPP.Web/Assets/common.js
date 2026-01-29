app.factory("CommonFactory", ["$rootScope", "$http", "$q", function ($rootScope, $http, $q) {
    this.getSecurityTypes = function () {
        return $http.get('/Common/GetSecurityTypes/');
    },
    this.getCommentTypes = function () {
        return $http.get('/Common/GetCommentTypes/');
    },
    this.getRecordStatus = function () {
        return $http.get('/Common/GetRecordStatus/');
    },
    this.getMenus = function () {
        return $http.get('/Common/GetMenus/');
    },
    this.getRoles = function () {
        return $http.get('/Common/GetRoles/');
    },
    this.getReportAccess = function () {
        return $http.get('/Common/GetReportAccess/');
    },
        this.getfiles = function (from_date, to_date) {
            return $http.post(('/Common/GetEmailFiles/'), { from_date: from_date.toJSON(), to_date: to_date.toJSON() });
        }
    return this;
}]);