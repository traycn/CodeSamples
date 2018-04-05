(function () {
    "use strict";

    angular.module(APPNAME)
        .config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {

            $routeProvider.when('/home', {
                templateUrl: '/scripts/gwig/app/singleplace/templates/home.html',
                controller: 'singlePlaceHomeTabController',
                controllerAs: 'main'
            }).when('/reviews', {
                templateUrl: '/scripts/gwig/app/singleplace/templates/reviews.html',
                controller: 'singlePlaceReviewsTabController',
                controllerAs: 'dashboard'
            }).when('/media', {
                templateUrl: '/scripts/gwig/app/singleplace/templates/media.html',
                controller: 'singlePlaceGalleryTabController',
                controllerAs: 'MediaCtrl'
            }).when('/followers', {
                templateUrl: '/scripts/gwig/app/singleplace/templates/followers.html',
                controller: 'singlePlaceUsersFollowingTabController',
                controllerAs: 'following'
            }).otherwise('/home');

            $locationProvider.html5Mode(false);

        }]);

})();