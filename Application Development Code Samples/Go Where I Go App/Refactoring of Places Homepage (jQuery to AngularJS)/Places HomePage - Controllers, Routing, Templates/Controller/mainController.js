(function () {
    "use strict";

    // CREATE: PlacesController
    angular.module(APPNAME)
        .controller('placesSinglePlaceController', PlacesController);

    PlacesController.$inject = ['$scope', '$baseController', '$placesService', '$imageService', 'Lightbox', '$uibModal', '$reviewsService', '$voteService', '$userFavoritePlacesService', '$UserCheckinService'];

    // CONTROLLER: PlacesController Function
    function PlacesController(
        $scope
        , $baseController
        , $placesService
        , $imageService
        , Lightbox
        , $uibModal
        , $reviewsService
        , $voteService
        , $userFavoritePlacesService
        , $UserCheckinService) {

        // DECLARE: Moment in Time
        var vm = this;

        // INHERIT: View Model with $baseController
        $baseController.merge(vm, $baseController);

        //variables/functions
        vm.slug = $("#singlePlaceSlug").val();
        vm.placeOperatingHoursUnformatted = null;
        vm.place = null;
        vm.video = null;
        vm.medias = null;
        vm.options = {};
        vm.$imageService = $imageService
        vm.yelpIcon = vm.$imageService.getImageResizeUrl("Https://s3-us-west-2.amazonaws.com/gwig/b9f80d94-c236-46cf-ab2f-9e66eb868186yelp%20icon.png", 32, 32);
        vm.googleIcon = vm.$imageService.getImageResizeUrl("https://s3-us-west-2.amazonaws.com/gwig/629ba9a5-e569-42bc-b06b-0739ae651743google%20icon.png", 32, 32);

        // INHERIT: Scope and Services
        vm.$placesService = $placesService;
        vm.$scope = $scope;
        vm.Lightbox = Lightbox;
        vm.$uibModal = $uibModal;
        vm.$reviewsService = $reviewsService;
        vm.$voteService = $voteService;
        vm.$userFavoritePlacesService = $userFavoritePlacesService;
        vm.$UserCheckinService = $UserCheckinService;

        vm.CurrentPage = 1;
        vm.PerPage = 10;
        vm.totalGalleryItems = null;

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

        // DECLARE: Places Function
        vm.receivePlacesItems = _receivePlacesItems;

        // DECLARE: Lightbox Function
        vm.openLightboxModal = _openLightboxModal;
        vm.openRatingModal = _openRatingModal;

        // DECLARE: Reviews Function
        vm.reviewError = _reviewError;
        vm.openModal = _openModal;
        vm.receiveReviewsByPlaceId = _receiveReviewsByPlaceId;
        vm.receiveAvgRating = _receiveAvgRating;
        //vm.voteError = _voteError;
        //vm.upVote = _upVote;
        //vm.downVote = _downVote;

        // DECLARE: UserFavoritePlaces Function
        vm.onGetCurrentUserIdSuccess = _onGetCurrentUserIdSuccess;
        vm.wantToGoFunction = _wantToGoFunction;
        vm.onFavoriteSuccess = _onFavoriteSuccess;

        // DECLARE: CheckIn Function
        vm.checkInSuccess = _checkInSuccess;
        vm.onEmpError = _onEmpError;
        vm.Geolocation = _GeoLocation;
        vm.Checkbutton = "Check In";

        // WRAPPER: small dependency on $scope
        vm.notify = vm.$placesService.getNotifier($scope);

        render();

        // STARTUP: [1] Places Services getBySlug
        function render() {
            vm.$placesService.getBySlug(vm.slug, vm.receivePlacesItems);
        }

        function _openRatingModal() {
            var modalInstance = vm.$uibModal.open({
                animation: true,
                templateUrl: '/scripts/gwig/app/reviews/templates/submitReviewModal.html',
                controller: 'modalController as mc',
                resolve: {
                    placeId: function () {
                        return vm.place.id;
                    }
                }
            });

            // When modal closes it re-renders the reviews onto page
            modalInstance.result.then(render);
        }

        function _openLightboxModal(index) {
            vm.Lightbox.openModal(vm.medias, index);
        };

        // [2]
        function _loadMedia() {
            vm.$placesService.getMediaByplacesId(vm.place.id, vm.CurrentPage, vm.PerPage, _onLoadMediaSuccess);
        }

        // [3]
        function loadReview() {
            vm.$reviewsService.getByplacesId(vm.place.id, vm.receiveReviewsByPlaceId, vm.reviewError);
        }

        // [4]
        function loadAverageReview() {
            vm.$reviewsService.placesAvg(vm.place.id, vm.receiveAvgRating, vm.reviewError);
        }

        // [5]
        function loadUserFavorite() {
            vm.$userFavoritePlacesService.apiSelectByCurrentUserIdAndPlaceId(vm.place.userId, vm.place.id, vm.onGetCurrentUserIdSuccess, vm.userFavoritePlacesError);
        }

        //// [5.2] Check In Functionality
        function _GeoLocation() {

            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(_insertPosition);
            } else {

                vm.$alertService.error("Geolocation is not supported by this browser");
            }
        }

        // [5.2] Check In Location
        function _insertPosition(position) {

            var json = {
                PlacesId: vm.place.id,
                Latitude: position.coords.latitude,
                Longitude: position.coords.longitude
            }

            vm.$UserCheckinService.insert(json, vm.checkInSuccess, vm.onEmpError);
        }

        // ERROR Check In Handler
        function _onEmpError(jqXhr, error) {
            console.error("Check In Error: ", error);

            var warningMessage = JSON.parse(jqXhr.responseText);

            vm.$alertService.warning(warningMessage.message);
        }

        // SUCCESS Check In Handler
        function _checkInSuccess(data) {
            if (data) {
                vm.$alertService.success("Checked In!");

                vm.Checkbutton = "Good to go!";

                render();
            }
        }

        //// [5.1] 
        // wantToGo Button: Conditional to check whether in "Want To Go" list or not.
        function _wantToGoFunction() {
            if (vm.dataCheck) {

                for (var i = 0; i < vm.dataCheck.length; i++) {

                    if (vm.dataCheck[i].favoriteType == 2) {

                        vm.$alertService.warning('Already in your "Want To Go" list!');
                    }
                }

            } else {

                vm.favoriteTypeData = {
                    favoriteType: 2,
                    placeId: vm.place.id
                }

                vm.$userFavoritePlacesService.apiPostUserFavoritePlaces(vm.favoriteTypeData, vm.onFavoriteSuccess, vm.onFavoriteError);
            }
        };

        // SUCCESS UserFavoritePlaces Handler
        function _onFavoriteSuccess() {
            vm.$alertService.info('Added to your "Want To Go" list');

            render();
        };

        // ERROR UserFavoritePlaces Handler
        function _onFavoriteError() {
            vm.$alertService.warning('Error adding to your Want To Go list');
        };

        //// [5] LoadUserFavorite
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

        //// UPVOTE and DOWNVOTE for Reviews
        // Functions to handle upvote for review
        function _upVote(review) {

            vm.postVote.UserId = review.userId;
            vm.postVote.NetVote = 1;
            vm.postVote.ContentId = review.id;
            vm.postVote.UserName = review.userName;

            // Vote can only be submitted once per user and is permanent
            if (!review.hasVoted) {
                vm.$voteService.insert(vm.postVote, render, vm.voteError);

                vm.$alertService.success("Vote Submitted!");

            } else {
                vm.$alertService.error("You have already Voted!");
            }
        }

        // Function to handle downvote for review
        function _downVote(review) {

            vm.postVote.UserId = review.userId;
            vm.postVote.NetVote = -1;
            vm.postVote.ContentId = review.id;
            vm.postVote.UserName = review.userName;

            // Vote can only be submitted once per user and is permanent
            if (!review.hasVoted) {
                vm.$voteService.insert(vm.postVote, render, vm.voteError);

                vm.$alertService.success("Vote Submitted!");
            } else {
                vm.$alertService.error("You have already Voted!");
            }
        }

        //// [4] LoadAverageReview
        // Receive data for average rating
        function _receiveAvgRating(data) {

            // Average rating is rounded up to a whole number
            // bootstrap star rating has trouble with decimal numbers
            //vm.notify(function () {
            vm.avgRating = Math.round(data);
            //});

            loadUserFavorite();
        }

        //// [3.1] OpenModal to Submit Review
        // Function opens up modal and passes in placeId to submit review
        function _openModal() {
            var modalInstance = vm.$uibModal.open({
                animation: true,
                templateUrl: '/scripts/gwig/app/reviews/templates/submitReviewModal.html',
                controller: 'modalController as mc',
                resolve: {
                    placeId: function () {
                        return vm.placeId;
                    }
                }
            });

            // When modal closes it re-renders the reviews onto page
            modalInstance.result.then(render);

        }

        //// [3] LoadReview
        // Receive array of all reviews
        function _receiveReviewsByPlaceId(data) {
            vm.reviews = data;
            // dashboard.items >>> dashboard

            loadAverageReview();
        }

        function _reviewError(jqXhr, error) {
            vm.$alertService.error('There was an error! OH DEAR!');
        }


        //// [2] LoadMedia
        function _onLoadMediaSuccess(data) {
            var m = [];
            vm.totalGalleryItems = data.totalItemCount;
            if (data.items && data.items.length) {

                var i = 0;
                for (var i = 0; i < data.items.length; i++) {
                    if (data.items[i].mediaType == 1) {

                        var thegoods = {
                            //image
                            'url': data.items[i].url,
                            'thumbUrl': vm.$imageService.getImageResizeUrl(data.items[i].url, 64, 64),
                            'MediaType': 1,
                            'arryNumber': i,
                            'mediaId': data.items[i].mediaId,
                            'netVote': data.items[i].netVote,
                            'userId': data.items[i].userId,
                            'hasVoted': data.items[i].hasVoted,
                            'reviewPointScore': data.items[i].reviewPointScore,
                            'created': data.items[i].created
                        }

                        m.push(thegoods);
                    }
                    else if (data.items[i].mediaType == 2) {

                        // ATTEMPT 1: make second index
                        //vm.getSecondIndex = function (i) {
                        //    if (i - data.items.length >= 0)
                        //        return i - data.items.length;
                        //    else
                        //        return index;
                        //}


                        var thevideogoods = {
                            //video
                            'type': 'video',
                            'url': data.items[i].url,
                            'thumbUrl': vm.$imageService.getImageResizeUrl(data.items[i].thumbnailUrl, 64, 64),
                            'MediaType': 2,
                            'arryNumber': i,
                            'mediaId': data.items[i].mediaId,
                            'netVote': data.items[i].netVote,
                            'userId': data.items[i].userId,
                            'hasVoted': data.items[i].hasVoted,
                            'reviewPointScore': data.items[i].reviewPointScore,
                            'created': data.items[i].created
                        };
                        m.push(thevideogoods);
                    }
                }
            }
            vm.medias = m;
            console.log("SET MEDIAS >>> ", vm.medias);

            loadReview();
        }

        //// [1] GET: PlaceBySlug
        function _receivePlacesItems(data) {
            for (var i = 0; i < data.placeRating[i].length; i++) {
                if (data.placeRating[i].ratingType == 2) {
                    data.placeRating[0].ratingType = data.placeRating[i].ratingType;
                    data.placeRating[0].rating = data.placeRating[i].rating;
                }
                else if (data.placeRating[i].ratingType == 3) {
                    data.placeRating[1].ratingType = data.placeRating[i].ratingType;
                    data.placeRating[1].rating = data.placeRating[i].rating;
                }
            }

            vm.place = data;

            vm.placeOperatingHoursUnformatted = data.operatingHours;

            if (vm.placeOperatingHoursUnformatted) {

                var operatingHoursArray = vm.placeOperatingHoursUnformatted.split("\n");
            }

            vm.placeLat1 = data.address.latitude;
            vm.placeLng1 = data.address.longitude;

            _loadMedia();
        }

        function _renderSuccess(media) {
            console.log('in here', media)
            if (media.item != null) {
                if (media.item.mediaType == 1) {
                    console.log('media', media.item.url);
                    vm.place.media.url = media.item.url;
                }
                else if (media.item.mediaType == 2) {
                    console.log('its a video');
                    vm.video = media.item.url;

                    vm.options.videoURL = media.item.url;
                    vm.options.containment = '#header_section';
                    vm.options.quality = 'large';
                    vm.options.autoPlay = true;
                    vm.options.mute = true;
                    vm.options.opacity = 1;
                    vm.options.loop = true;
                    vm.options.stopAt = 15;
                    vm.options.startAt = 0;
                }
            }
            vm.notify(function () {
                vm.place = vm.place
                if (vm.video != null) {
                    _headerbackgroundStart();
                }
                _broadcastPlace();
            });
        }
        function _headerbackgroundStart() {
            console.log('backgroundStart');
            vm.Jformat = JSON.stringify(vm.options);
            vm.player = $("#header_section").attr('data-property', vm.Jformat);
            $("#header_section").mb_YTPlayer();
        }
        function _broadcastPlace() {
            vm.$systemEventService.broadcast("placeLoaded", vm.place);
        }

    }
})();
