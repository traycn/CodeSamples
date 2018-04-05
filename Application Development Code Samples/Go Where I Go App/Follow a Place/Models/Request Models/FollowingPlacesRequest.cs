using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GWIG.Web.Domain;

namespace GWIG.Web.Models.Requests.FollowingPlaces
{
    public class FollowingPlacesRequest : FollowingPlacesDomain
    {
        public UserMini UserMini { get; set; }
    }
}