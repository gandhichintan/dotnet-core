
'use strict';

angular.module('bm.Pages', [])
.provider('$page', pageProvider);

function pageProvider() {
    var provider;

    var pages = [
        { path: '/', route: { templateUrl: './app/dashboard/dashboard.html', controller: dashboardController } },
    ];

    provider = {
        pages: pages,
        dashboard: '/'
    };

    this.$get = function () {
        return provider;
    };
}