using GWIG.Web.Domain;
using GWIG.Web.Models.Requests.UserFavoritePlaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// UserFavoritePlaces - Table that deals with a users bookmark lists of "Want To Go" && "Have Been"

namespace GWIG.Web.Services.Interface
{
    public interface IUserFavoritePlacesService
    {
        // POST: Favorite Place Type
        UserFavoritePlacesDomain InsertUserFavoritePlaces(UserFavoritePlacesDomain model);

        // SelectByIdUserFavoritePlaces
        UserFavoritePlacesDomain SelectByIdUserFavoritePlaces(int UserFavoritePlacesSelectId);

        // SelectByUserIdUserFavoritePlaces
        UserFavoritePlacesDomain SelectByUserIdUserFavoritePlaces(string UserFavoritePlacesSelectUserId);

        // SelectCurrentUserIdFavoritePlaces
        List<UserFavoritePlacesDomain> SelectCurrentUserIdFavoritePlaces(string UserId);

        // SelectByCurrentUserIdAndPlaceId
        List<UserFavoritePlacesDomain> SelectByCurrentUserIdAndPlaceId(string UserId, int PlaceId);

        // DeleteByUserIdAndPlaceIdAndFavoriteType
        void DeleteByUserIdAndPlaceIdAndFavoriteType(string UserId, int PlaceId, int FavoriteType);

        List<UserFavoritePlaceName> SelectCurrentUserIdFavoritePlaceNames(string UserId, int MissionId);
    }
}
