(function () {

    "use strict";

    angular.module(APPNAME)
        .factory('$addressService', AddressServiceFactory);

    AddressServiceFactory.$inject = ['$baseService', '$gwig'];

    function AddressServiceFactory($baseService, $gwig) {

        var addressServiceObject = gwig.services.addresses;

        var inheritedService = $baseService.merge(true, {}, addressServiceObject, $baseService);

        return inheritedService;
    }

})();