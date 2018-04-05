(function () {
    "use strict";

    // CREATE: PlacesController
    angular.module(APPNAME)
        .controller('singlePlaceHomeTabController', PlacesHomeTabController);

    PlacesHomeTabController.$inject = ['$scope', '$baseController', '$placesService'];

    // CONTROLLER: PlacesController Function
    function PlacesHomeTabController(
        $scope
        , $baseController
        , $placesService) {

        // DECLARE: Moment in Time && variables/functions
        var vm = this;
        vm.slug = $("#singlePlaceSlug").val();
        vm.place = null;

        // INIT: placesService && scope
        vm.$placesService = $placesService;
        vm.$scope = $scope;

        // DECLARE: functions
        vm.receivePlacesItems = _receivePlacesItems;

        // INHERIT: View Model with $baseController
        $baseController.merge(vm, $baseController);

        // WRAPPER: small dependency on $scope
        vm.notify = vm.$placesService.getNotifier($scope);

        render();

        // STARTUP: Places Services getBySlug
        function render() {
            console.log("HOME Slug >>> ", vm.slug);

            vm.$placesService.getBySlug(vm.slug, vm.receivePlacesItems);
        }

        // GET: PlaceBySlug
        function _receivePlacesItems(data) {

            vm.place = data;
        }
    }
})();
