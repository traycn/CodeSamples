using GWIG.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// UserFavoritePlaces - Table that deals with a users bookmark lists of "Want To Go" && "Have Been"

namespace GWIG.Web.Domain
{
    public class UserFavoritePlacesDomain
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public int PlaceId { get; set; }
        public UserFavoritePlacesType FavoriteType { get; set; }
        public int PointScore { get; set; }
    }
}