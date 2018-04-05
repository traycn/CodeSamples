(function () {
    "use strict";


    angular.module(APPNAME)
        .controller('singlePlaceFollowingPlacesTabController', SinglePlaceFollowingPlacesTabController);

    SinglePlaceFollowingPlacesTabController.$inject = ['$scope', '$baseController', '$followingPlacesService', '$placesService'];

    function SinglePlaceFollowingPlacesTabController(
        $scope
        , $baseController
        , $followingPlacesService
        , $placesService) {

        var vm = this;
        vm.placeId = null;
        vm.slug = $("#singlePlaceSlug").val();

        vm.$scope = $scope;
        vm.$baseController = $baseController;
        vm.$followingPlacesService = $followingPlacesService;
        vm.$placesService = $placesService;

        $baseController.merge(vm, $baseController);

        vm.notify = vm.$followingPlacesService.getNotifier($scope);



        vm.onFollowingPlacesSuccess = _onFollowingPlacesSuccess;
        vm.onFollowingPlacesError = _onFollowingPlacesError;
        //vm.changeToFollowingPlaces = _changeToFollowingPlaces;


        /// ATTEMPT 2: Using Angular-Responsive
        //vm.displayMode = ''; // default value


        //vm.$watch('displayMode', function (value) {

        //    switch (value) {
        //        case 'mobile':
        //            // do stuff for mobile mode
        //            console.log(value);
        //            break;
        //        case 'tablet':
        //            // do stuff for tablet mode
        //            console.log(value);
        //            break;
        //    }
        //});

        _init();

        // GET PLACE ID
        function _init() {
            vm.$placesService.getBySlug(vm.slug, _receivePlacesItems);

            vm.$systemEventService.listen("followingPlaceData", _changeToFollowingPlaces);
        }

        // GET PLACE ID: f'n
        function _receivePlacesItems(data) {

            vm.placeId = data.id;

            $followingPlacesService.GetByPlacesId(vm.placeId, vm.onFollowingPlacesSuccess, vm.onFollowingPlacesError);

        }

        function _onFollowingPlacesSuccess(data) {
            vm.notify(function () {

                /// ATTEMPT 2: Using Angular-Responsive
                //vm.myInterval = 0;
                //vm.followersSlides = data.items;

                //var i, first = [],
                //second, third;
                //var many = 1;

                //vm.displayMode = vm.$watch;

                //if (vm.displayMode == "mobile") { many = 1; }
                //else if (vm.displayMode == "tablet") { many = 2; }
                //else { many = 3; }

                //for (i = 0; i < vm.followersSlides.length; i += many) {
                //    second = {
                //        image1: vm.followersSlides[i]
                //    };
                //    if (many == 1) { }
                //    if (vm.followersSlides[i + 1] && (many == 2 || many == 3)) {
                //        second.image2 = vm.followersSlides[i + 1];

                //    }
                //    if (vm.followersSlides[i + (many - 1)] && many == 3) {
                //        second.image3 = vm.followersSlides[i + 2];
                //    }
                //    first.push(second);
                //}
                //vm.groupedSlides = first;

                vm.user = data.items;



                //ATTEMPT 1: make second index
                vm.getSecondIndex = function (i) {
                    if (i - data.items.length >= 0)
                        return i - data.items.length;
                    else
                        return index;
                }





            })

        }

        function _onFollowingPlacesError(error) {
            console.error("FOLLOWING ERROR: ", error);

        }

        function _changeToFollowingPlaces(getFollowersData, data) {

            if (data.placesId != null) {

                console.log("HIT: ", data.placesId);

                vm.placeId = data.placesId;

                $followingPlacesService.GetByPlacesId(vm.placeId, vm.onFollowingPlacesSuccess, vm.onFollowingPlacesError);

            }
        }
    }
})();