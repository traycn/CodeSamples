using Microsoft.Practices.Unity;
using GWIG.Data;
using GWIG.Web.Domain;
using GWIG.Web.Enums;
using GWIG.Web.Models.Requests.UserFavoritePlaces;
using GWIG.Web.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

// UserFavoritePlaces - Table that deals with a users bookmark lists of "Want To Go" && "Have Been"

namespace GWIG.Web.Services
{
    public class UserFavoritePlacesService : BaseService, IUserFavoritePlacesService
    {
        // POST: Favorite Places Type
        public UserFavoritePlacesDomain Insert(UserFavoritePlacesDomain model)
        {
            int Id = 0;
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.UserFavoritePlaces_Insert"
            , inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@UserId", model.UserId);
                paramCollection.AddWithValue("@PlaceId", model.PlaceId);
                paramCollection.AddWithValue("@FavoriteType", model.FavoriteType);
                paramCollection.AddWithValue("@PointScore", model.PointScore);
                SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                p.Direction = System.Data.ParameterDirection.Output;
                paramCollection.Add(p);
            }, returnParameters: delegate (SqlParameterCollection param)
            {
                int.TryParse(param["@Id"].Value.ToString(), out Id);
            });

            if (Id > 0)
            {
                return SelectByIdUserFavoritePlaces(Id);
            }
            else
            {
                return null;
            }
        }

        // GET: Favorite Places Type by ID
        public UserFavoritePlacesDomain SelectById(int Id)
        {
            UserFavoritePlacesDomain FavoritePlaceValues = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.UserFavoritePlaces_SelectById"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", Id);
                }
            , map: delegate (IDataReader reader, short set)
            {
                FavoritePlaceValues = new UserFavoritePlacesDomain();
                int startingIndex = 0;
                FavoritePlaceValues.Created = reader.GetSafeDateTime(startingIndex++);
                FavoritePlaceValues.UserId = reader.GetSafeString(startingIndex++);
                FavoritePlaceValues.PlaceId = reader.GetSafeInt32(startingIndex++);
                FavoritePlaceValues.FavoriteType = reader.GetSafeEnum<UserFavoritePlacesType>(startingIndex++);
                FavoritePlaceValues.PointScore = reader.GetSafeInt32(startingIndex++);
            });
            return FavoritePlaceValues;
        }

        // GET: Favorite Places Type by User ID
        public UserFavoritePlacesDomain SelectByUserId(string UserId)
        {
            UserFavoritePlacesDomain FavoritePlaceValues = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.UserFavoritePlaces_SelectByUserId"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@String", UserId);
                }
            , map: delegate (IDataReader reader, short set)
            {
                FavoritePlaceValues = new UserFavoritePlacesDomain();
                int startingIndex = 0;
                FavoritePlaceValues.Created = reader.GetSafeDateTime(startingIndex++);
                FavoritePlaceValues.UserId = reader.GetSafeString(startingIndex++);
                FavoritePlaceValues.PlaceId = reader.GetSafeInt32(startingIndex++);
                FavoritePlaceValues.FavoriteType = reader.GetSafeEnum<UserFavoritePlacesType>(startingIndex++);
                FavoritePlaceValues.PointScore = reader.GetSafeInt32(startingIndex++);
            });
            return FavoritePlaceValues;
        }

        // GET: All Favorite Places Type by User ID
        public List<UserFavoritePlacesDomain> SelectByUserId(string UserId)
        {
            List<UserFavoritePlacesDomain> UserFavoritesList = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.UserFavoritePlaces_SelectByUserId"
                , inputParamMapper: delegate (SqlParameterCollection ParamCollection)
                {
                    ParamCollection.AddWithValue("@UserId", UserId);
                }
                , map: delegate (IDataReader reader, short set)
                {
                    UserFavoritePlacesDomain FavoritePlaceValues = new UserFavoritePlacesDomain();
                    int startingIndex = 0;
                    FavoritePlaceValues.Id = reader.GetSafeInt32(startingIndex++);
                    FavoritePlaceValues.Created = reader.GetSafeDateTime(startingIndex++);
                    FavoritePlaceValues.UserId = reader.GetSafeString(startingIndex++);
                    FavoritePlaceValues.PlaceId = reader.GetSafeInt32(startingIndex++);
                    FavoritePlaceValues.FavoriteType = reader.GetSafeEnum<UserFavoritePlacesType>(startingIndex++);
                    FavoritePlaceValues.PointScore = reader.GetSafeInt32(startingIndex++);
                    if (UserFavoritesList == null)
                    {
                        UserFavoritesList = new List<UserFavoritePlacesDomain>();
                    }
                    UserFavoritesList.Add(FavoritePlaceValues);
                });
            return UserFavoritesList;
        }

        // GET: All Favorite Places Type by User ID and Place ID
        public List<UserFavoritePlacesDomain> SelectByUserIdPlaceId(string UserId, int PlaceId)
        {
            List<UserFavoritePlacesDomain> UserFavoritesList = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.UserFavoritePlaces_SelectByUserIdAndPlaceId"
                , inputParamMapper: delegate (SqlParameterCollection ParamCollection)
                {
                    ParamCollection.AddWithValue("@UserId", UserId);
                    ParamCollection.AddWithValue("@PlaceId", PlaceId);
                }
                , map: delegate (IDataReader reader, short set)
                {
                    UserFavoritePlacesDomain FavoritePlaceValues = new UserFavoritePlacesDomain();
                    int startingIndex = 0;
                    FavoritePlaceValues.Id = reader.GetSafeInt32(startingIndex++);
                    FavoritePlaceValues.Created = reader.GetSafeDateTime(startingIndex++);
                    FavoritePlaceValues.UserId = reader.GetSafeString(startingIndex++);
                    FavoritePlaceValues.PlaceId = reader.GetSafeInt32(startingIndex++);
                    FavoritePlaceValues.FavoriteType = reader.GetSafeEnum<UserFavoritePlacesType>(startingIndex++);
                    FavoritePlaceValues.PointScore = reader.GetSafeInt32(startingIndex++);
                    if (UserFavoritesList == null)
                    {
                        UserFavoritesList = new List<UserFavoritePlacesDomain>();
                    }
                    UserFavoritesList.Add(FavoritePlaceValues);
                });
            return UserFavoritesList;
        }

        // DELETE: Favorite Places Type by User Id, Place Id, and Favorite Type
        public void DeleteByUserIdPlaceIdFavoriteType(string UserId, int PlaceId, int FavoriteType)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.UserFavoritePlaces_DeleteByUserIdAndPlaceIdAndFavoriteType"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", UserId);
                   paramCollection.AddWithValue("@PlaceId", PlaceId);
                   paramCollection.AddWithValue("@FavoriteType", FavoriteType);
               });
        }
    }
}
