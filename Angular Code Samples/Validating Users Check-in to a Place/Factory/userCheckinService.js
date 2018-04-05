(function () {
    angular.module(APPNAME)
        .factory('$UserCheckinService', UserCheckinServiceFactory);

    UserCheckinServiceFactory.$inject = ['$baseService'];

    function UserCheckinServiceFactory($baseService) {
        var UserCheckinOject = gwig.services.usercheckin;

        var newService2 = $baseService.merge(true, {}, UserCheckinOject, $baseService);

        return newService2;
    }


})();