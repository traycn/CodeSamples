using GWIG.Web.Domain.MyMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GWIG.Web.Domain
{
    // USED ON SINGLE PLACES: Tab Places

    public class UserFavoritePlacesTypeDomain : Places
    {
        public UserFavoritePlacesDomain FavoriteTypePlace { get; set; }

        public CityPage CityObject { get; set; }

        //// Bool For Decorate: PlacesService_GetBySlug
        //bool hasBeen { get; set; }
        //bool wantToGo { get; set; }
        ////
    }
}