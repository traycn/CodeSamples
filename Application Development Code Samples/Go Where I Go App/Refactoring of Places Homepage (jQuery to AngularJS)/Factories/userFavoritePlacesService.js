(function () {

    "use strict";

    angular.module(APPNAME)
        .factory('$userFavoritePlacesService', userFavoritePlacesServiceFactory)

    userFavoritePlacesServiceFactory.$inject = ['$baseService', '$gwig'];

    function userFavoritePlacesServiceFactory($baseService, $gwig) {

        var favoritePlaceObject = gwig.services.userFavoritePlaces;

        var inheritedService = $baseService.merge(true, {}, favoritePlaceObject, $baseService);

        return inheritedService;
    }

})();