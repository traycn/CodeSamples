(function () {

    "use strict";

    angular.module(APPNAME)
        .factory('$UserCheckinService', UserCheckinServiceFactory);

    UserCheckinServiceFactory.$inject = ['$baseService', '$gwig'];

    function UserCheckinServiceFactory($baseService, $gwig) {

        var userCheckinOject = gwig.services.usercheckin;

        var inheritedService = $baseService.merge(true, {}, userCheckinOject, $baseService);

        return inheritedService;
    }

})();