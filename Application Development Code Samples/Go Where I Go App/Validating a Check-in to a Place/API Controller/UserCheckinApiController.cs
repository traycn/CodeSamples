using Microsoft.Practices.Unity;
using GWIG.Web.Domain;
using GWIG.Web.Models.Requests;
using GWIG.Web.Models.Responses;
using GWIG.Web.Services;
using GWIG.Web.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GWIG.Web.Controllers.Api
{
    [RoutePrefix("api/checkin")]
    public class UserCheckinApiController : ApiController
    {
        [Dependency]
        public IUserCheckIn _UserCheckinService { get; set; }

        [Authorize]
        [Route(),HttpPost]
        // POST: User Checkin and return value
        // - CONDITIONAL: For Success and Error Response Messages
        public HttpResponseMessage postCheckin(UserCheckinRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            model.UserId = UserService.GetCurrentUserId();

            double distanceCheck = 0;
            int distanceMessageInFeet = 0;

            double distanceCalculated = _UserCheckinService.Insert(model);

            // CONDITIONAL: If distance is less than 1610 meters
            if (distanceCalculated < 1610)
            {
                // CONVERTS: Distance to Feet 
                distanceCheck = distanceCalculated * 3.28084;

                // CASTS: Double to Int for Error response message
                distanceMessageInFeet = (int)distanceCheck;

                // CONDITIONAL: If distance is less than 200 feet than RETURN: DistanceCaculated
                if (distanceCheck < 200)
                {
                    ItemResponse<double> response = new ItemResponse<double>();
                    response.Item = distanceCalculated;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else          // ERROR/RETURN: In Feet
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unable to Check In. You're about " + distanceMessageInFeet + " feet away!");
                }
            }
            else           // ERROR/RETURN: In Miles
            {
                double distanceCheck2 = distanceCalculated * 0.000621371;

                int distanceMessageInMiles = (int)distanceCheck2;

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unable to Check In. You're about " + distanceMessageInMiles + " mile(s) away!");
            }
        }

        // GET: UserCheckin By User Id and Place Id
        [Route("{placesId:int}"),HttpGet]
        [Authorize]
        public HttpResponseMessage getSingleCheckinByUserIdAndPlacesId(int placesId, UserCheckinRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            model.UserId = UserService.GetCurrentUserId();

            model.PlacesId = placesId;

            UserCheckinDomain User = _UserCheckinService.getByUserIdandPlacesId(model);

            ItemResponse<UserCheckinDomain> respone = new ItemResponse<UserCheckinDomain>();

            respone.Item = User;

            return Request.CreateResponse(HttpStatusCode.OK, respone);
        }
    }
}
