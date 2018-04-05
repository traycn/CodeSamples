(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('followingPlacesController', FollowingPlacesController);

    FollowingPlacesController.$inject = ['$scope', '$baseController', '$followingPlacesService', '$placesService'];

    // DECLARE: FollowingPlacesController Function
    function FollowingPlacesController(
        $scope
        , $baseController
        , $followingPlacesService
        , $placesService) {

        var vm = this;

        // INIT: scope and services
        vm.$scope = $scope;
        vm.$followingPlacesService = $followingPlacesService;
        vm.$placesService = $placesService;

        $baseController.merge(vm, $baseController);

        vm.notify = vm.$followingPlacesService.getNotifier($scope);

        vm.slug = $("#singlePlaceSlug").val();

        vm.followingPlacesData = null;
        vm.userLogInCheck = false;

        // DECLARE: Functions
        vm.changeToFollowing = _changeToFollowing;
        vm.onFollowingError = _onFollowingError;

        _init();

        function _init(data) {

            vm.$placesService.getBySlug(vm.slug, _receivePlacesItems);

            vm.userLogInCheck = data;
            //vm.userLogInCheck = true;
        }

        function _receivePlacesItems(data) {
            console.log(data)
            vm.notify(function () {
                vm.place = data;

                vm.placeId = data.id;

                vm.placeName = data.name

                if (data.isFollower == true) {
                    vm.showFollowing = "Following";
                } else {
                    vm.showFollowing = "Not Following";
                }
            });
        }

        function _changeToFollowing() {
            if (vm.place.isFollower == true) {

                vm.followingPlacesData = {
                    placesId: vm.placeId
                }

                vm.$systemEventService.broadcast("followingPlaceData", vm.followingPlacesData);

                vm.$followingPlacesService.DeleteByUserId(vm.followingPlacesData, _init, vm.onFollowingError);

                vm.$alertService.error("Unfollowing ", vm.placeName);

            } else {
                vm.followingPlacesData = {
                    placesId: vm.placeId
                }

                vm.$systemEventService.broadcast("followingPlaceData", vm.followingPlacesData);

                vm.$followingPlacesService.Insert(vm.followingPlacesData, _init, vm.onFollowingError);

                if (vm.userLogInCheck) {

                    vm.$alertService.success("Following " + vm.placeName + "!");
                }

            }
        }

        function _onFollowingError(error) {
            console.error("Following ERROR: ", error);

            vm.$alertService.warning("Must be logged in to Follow " + vm.placeName + "!");

        }
    };
})();
