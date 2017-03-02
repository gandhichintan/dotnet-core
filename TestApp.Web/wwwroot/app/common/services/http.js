'use strict';

angular
    .module('TestAppTestApp')
    .factory('http', http);

http.$inject = ['$http'];

function http($http) {
    var service = {
        postSecure: postSecure,
        post: post,
        getSecure: getSecure
    };


    function postSecure(url, params) {

        return $http({
            method: 'POST',
            url: url,
            headers: {
                '__RequestVerificationToken': getAccessToken(),
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            data: $.param(params)
        });
    };

    function post(url, params) {

        return $http({
            method: 'POST',
            url: url,
            data:$.param(params)
        });
    };

    function getSecure(url, params) {

        return $http({
            method: 'GET',
            url: url,
            headers: {
                '__RequestVerificationToken': getAccessToken(),
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        });
    };

    function getAccessToken() {
        return $('[name=__RequestVerificationToken]').val();
    }

    return service;
}
