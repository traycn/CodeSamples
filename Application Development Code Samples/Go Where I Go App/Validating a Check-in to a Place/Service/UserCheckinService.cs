using Microsoft.Practices.Unity;
using GWIG.Data;
using GWIG.Web.Domain;
using GWIG.Web.Enums;
using GWIG.Web.Models.Requests;
using GWIG.Web.Models.Responses;
using GWIG.Web.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using GWIG.Web.Utils;

namespace GWIG.Web.Services
{
    public class UserCheckinService : BaseService, IUserCheckIn
    {
        [Dependency]
        public IUserFavoritePlacesService _UserFavoritePlacesService { get; set; }

        [Dependency]
        public IPlacesService _PlacesService { get; set; }

        // POST: User Checkin and return value
        // - CONDITIONAL: For Have Been/Want To Go button
        public double Insert(UserCheckinRequest model)
        {
            UserFavoritePlacesDomain Values = new UserFavoritePlacesDomain();
            UserCheckinDomain LatLngCheck = new UserCheckinDomain();
            decimal CheckInLatitude = 0;
            decimal CheckInLongitude = 0;
            decimal PlaceLatitude = 0;
            decimal PlaceLongitude = 0;
            double getDistance = 0;
            double getDistanceCheck = 0;
            bool haveBeenCheckIn = true;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.UserCheckin_Insert"
                , inputParamMapper: delegate (SqlParameterCollection param)
                 {
                     param.AddWithValue("@UserId", model.UserId);
                     param.AddWithValue("@PlacesId", model.PlacesId);
                     param.AddWithValue("@Latitude", model.Latitude);
                     param.AddWithValue("@Longitude", model.Longitude);
                     param.AddWithValue("@MissionId ", model.MissionId);

                     SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                     p.Direction = System.Data.ParameterDirection.Output;

                     param.Add(p);
                 }, returnParameters: delegate (SqlParameterCollection param)
                 {
                     Values.UserId = (string)param["@UserId"].Value;
                     Values.PlaceId = (int)param["@PlacesId"].Value;
                     LatLngCheck.Latitude = (decimal)param["@Latitude"].Value;
                     LatLngCheck.Longitude = (decimal)param["@Longitude"].Value;
                 }
                );
                
            CheckInLatitude = LatLngCheck.Latitude;
            CheckInLongitude = LatLngCheck.Longitude;

            Places placeValues = _PlacesService.GetPlace(PlaceIdValue);

            PlaceLatitude = placeValues.Address.Latitude;
            PlaceLongitude = placeValues.Address.Longitude;

            // UTIL: Get Distance between Users current location LatLng and the specific Places LatLng
            getDistance = Geography.getDistance(CheckInLatitude, CheckInLongitude, PlaceLatitude, PlaceLongitude);

            List<UserFavoritePlacesDomain> favoriteTypeValue = _UserFavoritePlacesService.SelectByCurrentUserIdAndPlaceId(Values.UserId, Values.PlaceId);

            // CONDITIONAL: If distance is less than 1610 meters
            if (getDistance < 1610)
            {
                // CONVERTS: Distance to feet 
                getDistanceCheck = getDistance * 3.28084;

                // CONDITIONAL: If distance is less than 200 feet than CheckIn:True && Have Been:True -Else HaveBeen:False
                if (getDistanceCheck < 200)
                {
                    haveBeen = true;
                }
                else
                {
                    haveBeen = false;
                }

                // ASSIGN: haveBeen:True value to singlePlace and CHECKS/DELETES data if wantToGo:True
                if (haveBeenCheckIn == true)
                {
                    if(favoriteTypeValue == null)
                    {
                        Values.FavoriteType = UserFavoritePlacesType.BeenThere;

                        _UserFavoritePlacesService.InsertUserFavoritePlaces(Values);
                    }
                    else
                    {
                        foreach (UserFavoritePlacesDomain FavoritePlace in favoriteTypeValue)
                        {
                            UserFavoritePlacesType FavoritePlaceCase = FavoritePlace.FavoriteType;

                            Values.FavoriteType = UserFavoritePlacesType.BeenThere;

                            _UserFavoritePlacesService.InsertUserFavoritePlaces(Values);

                            switch (FavoritePlaceCase)
                            {
                                case UserFavoritePlacesType.BeenThere:

                                    break;

                                case UserFavoritePlacesType.WantToGo:

                                    _UserFavoritePlacesService.DeleteByUserIdAndPlaceIdAndFavoriteType(UserIdValue, PlaceIdValue, 2);

                                    break;
                            }
                        }
                    }
                }
            }

            return getDistance;
        }

        // GET: UserCheckin By User Id and Place Id
        public UserCheckinDomain SelectByUserIdAndPlaceId(UserCheckinRequest model)
        {
            UserCheckinDomain User = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.UserCheckin_SelectByUserIdAndPlaceId"
                , inputParamMapper: delegate (SqlParameterCollection param)
                {
                    param.AddWithValue("@UserId", model.UserId);
                    param.AddWithValue("@PlacesId", model.PlacesId);
                }, map: delegate (IDataReader reader, short set)
                {
                    User = new UserCheckinDomain();
                    int startingIndex = 0;

                    User.Id = reader.GetSafeInt32(startingIndex++);
                    User.Created = reader.GetSafeDateTime(startingIndex++);
                    User.PlacesId = reader.GetSafeInt32(startingIndex++);
                    User.Latitude = reader.GetSafeDecimal(startingIndex++);
                    User.Longitude = reader.GetSafeDecimal(startingIndex++);
                    User.MissionId = reader.GetSafeInt32(startingIndex++);
                }
                );
            return User;
        }
    }
}
