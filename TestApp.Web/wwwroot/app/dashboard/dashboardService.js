'use strict';

angular
    .module('TestAppTestApp')
    .factory('dashboardService', ['http', dashboardService]);

function dashboardService($http) {

    var controller = 'Banking/';

    var service = {
        getCurrentBalance: getCurrentBalance,
        deposit: deposit,
        withdraw: withdraw,
        transferFund: transferFund,
        getStatement: getStatement
    };

    function getCurrentBalance() {
        var url = controller + 'GetCurrentBalance';
        return $http.getSecure(url);
    }

    function deposit(amount) {
        var url = controller + 'Deposit'
        return $http.postSecure(url, { Amount: amount });
    }

    function withdraw(amount) {
        var url = controller + 'Withdraw';
        return $http.postSecure(url, { Amount: amount });
    }

    function transferFund(accountName, amount) {
        var url = controller + 'TransferFund';
        return $http.postSecure(url, { Perticulars: accountName, Amount: amount });
    }

    function getStatement() {
        var url = controller + 'GetStatement';
        return $http.getSecure(url);

    }
    return service;
}
