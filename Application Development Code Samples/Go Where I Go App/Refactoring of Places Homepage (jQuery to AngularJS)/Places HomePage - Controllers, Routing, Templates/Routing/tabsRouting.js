(function () {
    "use strict";

    angular.module(APPNAME)
        .config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {

            $routeProvider.when('/home', {
                templateUrl: '/scripts/app/singleplace/templates/home.html',
                controller: 'singlePlaceHomeTabController',
                controllerAs: 'main'
            }).when('/reviews', {
                templateUrl: '/scripts/app/singleplace/templates/reviews.html',
                controller: 'singlePlaceReviewsTabController',
                controllerAs: 'dashboard'
            }).when('/media', {
                templateUrl: '/scripts/app/singleplace/templates/media.html',
                controller: 'singlePlaceGalleryTabController',
                controllerAs: 'MediaCtrl'
            }).when('/followers', {
                templateUrl: '/scripts/app/singleplace/templates/followers.html',
                controller: 'singlePlaceFollowingPlacesTabController',
                controllerAs: 'following'
            }).otherwise('/home');

            $locationProvider.html5Mode(false);

        }]);

})();