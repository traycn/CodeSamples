(function () {

    "use strict";

    angular.module(APPNAME)
        .factory('$followingPlacesService', FollowingPlacesServiceFactory);

    FollowingPlacesServiceFactory.$inject = ['$baseService', '$gwig'];

    function FollowingPlacesServiceFactory($baseService, $gwig) {

        var followingPlacesObject = gwig.services.followingplaces;

        var inheritedService = $baseService.merge(true, {}, followingPlacesObject, $baseService);

        return inheritedService;
    }

})();