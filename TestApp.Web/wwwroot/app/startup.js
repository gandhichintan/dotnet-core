'use strict';

var modules = [
    // Angular modules 
    'ngRoute',

    // Custom modules 
    'bm.Pages'
];

angular.module('TestAppTestApp', modules)

.config(['$routeProvider', '$pageProvider', routeConfigurator]);

function routeConfigurator($routeProvider, $pageProvider) {

    var pageProvider = $pageProvider.$get();

    angular.forEach(pageProvider.pages, function (page, key) {
        $routeProvider.when(page.path, page.route);
    });

    $routeProvider.otherwise({ redirectTo: pageProvider.login })
};