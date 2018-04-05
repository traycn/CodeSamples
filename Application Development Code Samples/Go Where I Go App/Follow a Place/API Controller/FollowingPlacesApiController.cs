using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Practices.Unity;
using GWIG.Web.Enums;
using GWIG.Web.Models.SystemEvents;
using GWIG.Web.Services.Interface;
using GWIG.Web.Domain;
using GWIG.Web.Models.Requests.FollowingPlaces;
using GWIG.Web.Models.Responses;
using GWIG.Web.Services;

namespace GWIG.Web.Controllers.Api
{
    [RoutePrefix("api/follow/place")]
    public class FollowingPlacesApiController : BaseApiController
    {
        [Dependency]
        public SystemEventService SystemEventsService { get; set; }

        [Dependency]
        public IPlacesService _PlacesService { get; set; }

        [Dependency]
        public IFollowingPlacesService _FollowingPlacesService { get; set; }

        // CACHE: Places data
        private void _clearPlacesCache(int PlaceId)
        {
            var place = _PlacesService.GetPlace(PlaceId);

            RemoveCacheItem((PlacesApiController c) => c.Get(PlaceId));
            RemoveCacheItem((PlacesApiController c) => c.GetSlug(place.Slug));
        }

        // POST: Follows a new Place
        [Route, HttpPost]
        [Authorize]
        public HttpResponseMessage FollowingPlacesInsert(FollowingPlacesRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if(UserService.IsLoggedIn())
            {
                model.UserId = UserService.GetCurrentUserId();

                _FollowingPlacesService.FollowingPlacesInsert(model);

            } else
            {

                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "");
            }

            ItemResponse<bool> response = new ItemResponse<bool>();
            response.IsSuccessful = true;

            SystemEventsService.AddSystemEvent(new AddSystemEventModel
            {
                ActorUserId = model.UserId,
                ActorType = ActorType.User,
                EventType = SystemEventType.UserFollowPlace,
                TargetId = model.PlacesId,
                TargetType = TargetType.Place
            });

            _clearPlacesCache(model.PlacesId);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        // GETS: All the Places being Followed
        [Route, HttpGet]
        public HttpResponseMessage FollowingPlacesGetAll()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            List<FollowingPlacesDomain> ListDomain = _FollowingPlacesService.FollowingPlacesGetAll();

            ItemsResponse<FollowingPlacesDomain> response = new ItemsResponse<FollowingPlacesDomain>();

            response.Items = ListDomain;

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        // GET: All Following Users of a single Place by ID
        [Route("placesId/{placesId:int}"), HttpGet]
        public HttpResponseMessage FollowingPlacesGetbyPlacesId(int placesId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            List<FollowingPlacesDomain> ListDomain = _FollowingPlacesService.FollowingPlacesGetPlacesById(placesId);

            ItemsResponse<FollowingPlacesDomain> response = new ItemsResponse<FollowingPlacesDomain>();

            response.Items = ListDomain;

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        // DELETE: Removes the Follow of a User by PlaceID and UserID
        [Route(), HttpDelete]
        public HttpResponseMessage FollowingPlacesDeletebyUserId(FollowingPlacesRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            model.UserId = UserService.GetCurrentUserId();

            _FollowingPlacesService.FollowingPlacesDelete(model);

            ItemResponse<bool> response = new ItemResponse<bool>();
            response.IsSuccessful = true;

            _clearPlacesCache(model.PlacesId);

            return Request.CreateResponse(HttpStatusCode.OK, response.IsSuccessful);
        }

        // GET: All Users Following a Place by PlaceID
        [Route("{placesId:int}"), HttpGet]
        [Authorize]
        public HttpResponseMessage FollowingPlacesApiGetUserInfo(int placesId)
        {
            List<FollowingPlacesDomain> ListDomain = _FollowingPlacesService.FollowingPlacesGetUserInfo(placesId);

            ItemsResponse<FollowingPlacesDomain> response = new ItemsResponse<FollowingPlacesDomain>();

            response.Items = ListDomain;

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


        /*---------------- Changing Following function and utilizing cache ----------------
         
        // GET: All of a single place using userId
        [Route("{userId}"), HttpGet]
        public HttpResponseMessage FollowingPlacesUserId(string userId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            List<FollowingPlacesDomain> ListDomain = _FollowingPlacesService.FollowingPlacesGetByUserId(userId);

            ItemsResponse<FollowingPlacesDomain> response = new ItemsResponse<FollowingPlacesDomain>();

            response.Items = ListDomain;

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        // GET: All of a single place using Current UserID
        [Route("current"), HttpGet]
        [Authorize]
        public HttpResponseMessage FollowingPlacesCurrentUserId()
        {
            var UserId = UserService.GetCurrentUserId();

            List<FollowingPlacesDomain> ListDomain = _FollowingPlacesService.FollowingPlacesGetDataByUserId(UserId);

            ItemsResponse<FollowingPlacesDomain> response = new ItemsResponse<FollowingPlacesDomain>();

            response.Items = ListDomain;

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


        // POST: Following of the followed places from the userId
        [Route("current"), HttpPost]
        [Authorize]
        public HttpResponseMessage FollowingPlacesPostbyUserId(FollowingPlacesRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            model.UserId = UserService.GetCurrentUserId();

            List<FollowingPlacesDomain> ListDomain = _FollowingPlacesService.FollowingPlacesGetUserById(model);

            ItemsResponse<FollowingPlacesDomain> response = new ItemsResponse<FollowingPlacesDomain>();

            response.Items = ListDomain;

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        // DELETE: by User ID
        [Route(), HttpDelete]
        [Authorize]
        public HttpResponseMessage FollowingPlacesDeletebyUserId(FollowingPlacesRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            model.UserId = UserService.GetCurrentUserId();

            _FollowingPlacesService.FollowingPlacesDelete(model);

            ItemResponse<bool> response = new ItemResponse<bool>();
            response.IsSuccessful = true;

            return Request.CreateResponse(HttpStatusCode.OK, response.IsSuccessful);
        }

        // POST: A Follow by User ID and Place ID
        //[Route("userId/placesId"), HttpPost]
        //public HttpResponseMessage FollowingPlacesGetbyUserIdAndPlacesId(FollowingPlacesRequest model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //    model.UserId = UserService.GetCurrentUserId();

        //    FollowingPlacesDomain Domain = _FollowingPlacesService.FollowingPlacesGetUserByIdAndPlacesId(model);

        //    ItemResponse<FollowingPlacesDomain> response = new ItemResponse<FollowingPlacesDomain>();

        //    response.Item = Domain;

        //    return Request.CreateResponse(HttpStatusCode.OK, response);
        //}

        */
    }
}
