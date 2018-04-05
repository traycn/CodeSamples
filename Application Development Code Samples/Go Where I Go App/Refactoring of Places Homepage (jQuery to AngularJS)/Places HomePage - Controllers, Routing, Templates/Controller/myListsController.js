(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('userFavoritePlacesController', UserFavoritePlacesController);

    // INJECT: $scope && $baseController property
    UserFavoritePlacesController.$inject = ['$scope', '$baseController', '$userFavoritePlacesService','$placesService'];

    // DECLARE: Controller Function
    function UserFavoritePlacesController(
        $scope
        , $baseController
        , $userFavoritePlacesService
        , $placesService) {

        // DECLARE: Merge, Services, Variables, and Functions
        var vm = this;
        $baseController.merge(vm, $baseController);

        vm.slug = $("#singlePlaceSlug").val();

        vm.placeId = null;
        vm.placeUserId = null;
        vm.favoriteTypeData = {};
        vm.favoriteType;
        vm.dataCheck;

        // INITIALIZE: $userFavoritePlacesService && $scope property
        vm.$userFavoritePlacesService = $userFavoritePlacesService;
        vm.$scope = $scope;
        vm.$placesService = $placesService;
        vm.notify = vm.$placesService.getNotifier($scope);

        // DECLARE: HaveBeen/Been && Success/Error handlers Functions
        vm.onFavoriteSuccess = _onFavoriteSuccess;
        vm.onGetCurrentUserIdSuccess = _onGetCurrentUserIdSuccess;
        vm.wantToGoFunction = _wantToGoFunction;
        vm.onFavoriteError = _onFavoriteError;

        _init();

        function _init() {
            vm.$placesService.getBySlug(vm.slug, _receivePlacesItems);

            vm.$systemEventService.listen("checkInData", _changeToHaveBeen);
        }

        // GET: Places data and assign to variables >> GET: FavoritePlaces data
        function _receivePlacesItems(data) {
            vm.place = data;
            vm.placeId = vm.place.id;
            vm.placeUserId = vm.place.userId;

            vm.$userFavoritePlacesService.apiSelectByCurrentUserIdAndPlaceId(vm.placeId, vm.onGetCurrentUserIdSuccess);
        }

        // LISTEN: GETcheckInData to ng-if "Want To Go" button to "Have Been"
        function _changeToHaveBeen(getCheckInData, data) {
            vm.notify(function () {
                console.log("getCheckInData: ", data);

                if (data.PlacesId = vm.placeId) {

                    vm.favoriteType = 1;
                }
            });
        }

        // wantToGo Button: Conditional to check whether in "Want To Go" list or not.
        function _wantToGoFunction() {

            if (vm.dataCheck) {

                for (var i = 0; i < vm.dataCheck.length; i++) {

                    if (vm.dataCheck[i].favoriteType == 2) {

                        vm.$alertService.warning('Already in your "Want To Go" list!');

                    }
                }

            } else
            {

                vm.favoriteTypeData = {
                    favoriteType: 2,
                    placeId: vm.placeId
                }

                vm.$userFavoritePlacesService.apiPostUserFavoritePlaces(vm.favoriteTypeData, vm.onFavoriteSuccess, vm.onFavoriteError);
            }
        };

        // SUCCESS: Show Success alertService
        function _onFavoriteSuccess() {

            vm.$alertService.info('Added to your "Want To Go" list');

            _init();
        };

        // SUCCESS: GET FavoritePlaces data and assign vm.favoriteType value for ng-if spans
        function _onGetCurrentUserIdSuccess(data) {
            console.log("Get Current UserId Have Been: ", data);

            vm.dataCheck = data.items;

            if (vm.dataCheck) {

                for (var i = 0; i < data.items.length; i++) {

                    if (data.items[i].favoriteType == 1) {

                        vm.favoriteType = 1;

                    } else {

                        vm.favoriteType = 2;
                    }
                }
            }

        };

        // ERROR: Show Error alertService
        function _onFavoriteError() {

            vm.$alertService.warning('Must be Logged In to add to your list');
        }
    }
})();
