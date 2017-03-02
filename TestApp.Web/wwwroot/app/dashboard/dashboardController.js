'use strict';

angular
    .module('TestAppTestApp')
    .controller('dashboardController', ['$scope', 'dashboardService', dashboardController]);


function dashboardController($scope, dashboardService) {
    $scope.title = 'dashboardController';
    $scope.show = 'none';

    activate();

    function activate() {
        $scope.getCurrentBalance = getCurrentBalance;

        $scope.getCurrentBalance();

    }

    $scope.showPanel = function (panel) {
        $scope.show = panel;
    }

    $scope.getStatement = function () {
        dashboardService.getStatement().then(function (response) {
            $scope.transactions = response.data;
        });
    }

    $scope.deposit = function (amount) {
        dashboardService.deposit(amount).then(function (response) {
            $scope.getCurrentBalance();
            $scope.depositAmount = '';
        });
    }

    $scope.withdraw = function (amount) {
        dashboardService.withdraw(amount).then(function (response) {
            $scope.getCurrentBalance();
            $scope.withdrawAmount = '';
        });
    }

    $scope.transferFund = function (toAccount, amount) {
        dashboardService.transferFund(toAccount, amount).then(function (response) {
            $scope.getCurrentBalance();
            $scope.transferAccount = '';
            $scope.transferAmount = '';
        });
    }

    function getCurrentBalance() {
        dashboardService.getCurrentBalance().then(function (response) {
            $scope.balance = response.data;
        });
    }
}
