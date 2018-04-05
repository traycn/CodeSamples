(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('userCheckinController', UserCheckinController);

    UserCheckinController.$inject = ['$scope', '$baseController', '$UserCheckinService', '$placesService'];

    function UserCheckinController(
        $scope
        , $baseController
        , $UserCheckinService
        , $placesService) {

        var vm = this;
        vm.$placesService = $placesService;
        vm.$UserCheckinService = $UserCheckinService;
        vm.$scope = $scope;

        vm.latitude = null;
        vm.longitude = null;
        vm.Checkbutton = "Check In";
        vm.placeId = null;
        vm.slug = $("#singlePlaceSlug").val();
        vm.checkInData = null;

        vm.Geolocation = _GeoLocation;
        vm.success = _success;
        vm.onEmpError = _onEmpError;

        vm.notify = vm.$UserCheckinService.getNotifier($scope);


        $baseController.merge(vm, $baseController);

        _init();

        function _init() {
            vm.$placesService.getBySlug(vm.slug, _receivePlacesItems);
        }

        function _receivePlacesItems(data) {
            vm.placeId = data.id

            vm.placeLat1 = data.address.latitude;
            vm.placeLng1 = data.address.longitude;
        }

        function _GeoLocation() {

            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(_insertPosition);
            } else {

                vm.$alertService.error("Geolocation is not supported by this browser");
            }
        }

        function _insertPosition(position) {

            var json = {
                PlacesId: vm.placeId,
                Latitude: position.coords.latitude,
                Longitude: position.coords.longitude
            }

            vm.$UserCheckinService.insert(json, vm.success, vm.onEmpError);
        }

        function _onEmpError(jqXhr, error) {
            console.error(error);

            var warningMessage = JSON.parse(jqXhr.responseText);

            vm.$alertService.warning(warningMessage.message);
        }

        function _success(data) {

            if (data) {

                vm.$alertService.success("Checked In!");

                vm.Checkbutton = "Good to go!";

                vm.$systemEventService.broadcast("checkInData", vm.checkInData);

            }
        }
    }

})();
