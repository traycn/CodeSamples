using Microsoft.Practices.Unity;
using GWIG.Web.Domain;
using GWIG.Web.Models.Requests.UserFavoritePlaces;
using GWIG.Web.Models.Responses;
using GWIG.Web.Services;
using GWIG.Web.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// UserFavoritePlaces - Table that deals with a users bookmark lists of "Want To Go" && "Have Been"

namespace GWIG.Web.Controllers.Api
{
    [RoutePrefix("api/userfavoriteplaces")]

    public class UserFavoritePlacesApiController : BaseApiController
    {
        [Dependency]
        public IUserFavoritePlacesService _UserFavoritePlacesService { get; set; }

        // POST: Favorite Place Type
        [Route(), HttpPost]
        public HttpResponseMessage InsertUserFavoritePlaces(UserFavoritePlacesDomain model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            model.UserId = UserService.GetCurrentUserId();

            _UserFavoritePlacesService.InsertUserFavoritePlaces(model);

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        // GET: Favorite Places by Favorite Places ID
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetByIdUserFavoritePlaces(int id)
        {
            if(!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<UserFavoritePlacesDomain> response = new ItemResponse<UserFavoritePlacesDomain>();

            UserFavoritePlacesDomain favoritePlaces = _UserFavoritePlacesService.SelectByIdUserFavoritePlaces(id);

            response.Item = favoritePlaces;

            return Request.CreateResponse(HttpStatusCode.OK, response.Item);
        }

        // GET: Favorite Places by User ID
        [Route("{userId}"), HttpGet]
        public HttpResponseMessage GetByUserIdUserFavoritePlaces(string userId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<UserFavoritePlacesDomain> response = new ItemResponse<UserFavoritePlacesDomain>();

            UserFavoritePlacesDomain favoritePlaces = _UserFavoritePlacesService.SelectByUserIdUserFavoritePlaces(userId);

            response.Item = favoritePlaces;

            return Request.CreateResponse(HttpStatusCode.OK, response.Item);
        }

        // GET: Favorite Places by Current User ID
        [Route("current"), HttpGet]
        [Authorize]
        public HttpResponseMessage GetByCurrentUserIdUserFavoritePlaces()
        {

            var UserId = UserService.GetCurrentUserId();

            List<UserFavoritePlacesDomain> ListDomain = _UserFavoritePlacesService.SelectCurrentUserIdFavoritePlaces(UserId);

            ItemsResponse<UserFavoritePlacesDomain> response = new ItemsResponse<UserFavoritePlacesDomain>();

            response.Items = ListDomain;

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        // GET: Favorite Places by Place ID
        [Route("list/{PlaceIdValue:int}"), HttpGet]
        [Authorize]
        public HttpResponseMessage GetByCurrentUserIdAndPlaceId(int PlaceIdValue)
        {
            var UserIdValue = UserService.GetCurrentUserId();

            List<UserFavoritePlacesDomain> ListDomain = _UserFavoritePlacesService.SelectByCurrentUserIdAndPlaceId(UserIdValue, PlaceIdValue);

            ItemsResponse<UserFavoritePlacesDomain> response = new ItemsResponse<UserFavoritePlacesDomain>();

            response.Items = ListDomain;

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
