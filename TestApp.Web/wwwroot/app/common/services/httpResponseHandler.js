(function () {
    'use strict';

    var httpInterceptor = function ($provide, $httpProvider) {
        $provide.factory('httpInterceptor', function ($q) {
            return {
                response: function (response) {
                    return response || $q.when(response);
                },
                responseError: function (rejection) {
                    if (rejection.status === 401) {
                        // you are not autorized
                    }
                    var alert = angular.element('.alert-danger');
                    alert.show();
                    alert.fadeOut(4000);
                    angular.element('#message').text(rejection.data);
                    return $q.reject(rejection);
                }
            };
        });
        $httpProvider.interceptors.push('httpInterceptor');
    };
    angular.module("TestAppTestApp").config(httpInterceptor);
})();