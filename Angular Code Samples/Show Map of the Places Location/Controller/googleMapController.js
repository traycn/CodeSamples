(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('geoMapsController', GeoMapsController);

    GeoMapsController.$inject = ['$scope', '$baseController'];

    function GeoMapsController(
        $scope
        , $baseController) {

        var vm = this;

        // DEFAULT LOCATION: New York
        vm.map = {
            center: {
                latitude: 40.730610
                , longitude: -73.935242
            }
            , zoom: 15
            , options: {
                draggable: false
                , scrollwheel: false
            }
        }

        vm.$scope = $scope;

        // INHERIT: vm with $baseController            pro-tip - $systemEventService is part of $baseController
        $baseController.merge(vm, $baseController);

        vm.$systemEventService.listen("placeLoaded", _storeLatLng);

        function _storeLatLng(event, payload) {
            //console.log("payload: ", payload);

            vm.map.center.latitude = payload[1].address.latitude;
            vm.map.center.longitude = payload[1].address.longitude;
        }
    }
})();