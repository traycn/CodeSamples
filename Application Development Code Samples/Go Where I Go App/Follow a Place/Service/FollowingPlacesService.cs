using GWIG.Data;
using GWIG.Web.Domain;
using GWIG.Web.Domain.MyMedia;
using GWIG.Web.Enums;
using GWIG.Web.Models.Requests.FollowingPlaces;
using GWIG.Web.Models.Requests.Pagination;
using GWIG.Web.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GWIG.Web.Services
{
    public class FollowingPlacesService : BaseService, IFollowingPlacesService
    {
        // POST: A Follow to a Single Place by User ID and Place ID
        public string Insert(FollowingPlacesRequest model)
        {
            string userId = null;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.FollowingPlace_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                 {
                     paramCollection.AddWithValue("@PlaceId", model.PlaceId);
                     paramCollection.AddWithValue("@UserId", model.UserId);
                 }
                , returnParameters: delegate (SqlParameterCollection param)
                {
                    userId = (string)param["@UserId"].Value;
                }
                );

            return userId;
        }

        // GET: All Following Places
        public List<FollowingPlacesDomain> SelectAll()
        {
            List<FollowingPlacesDomain> followingplaces = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.FollowingPlaces_SelectAll"
                , inputParamMapper: null
                , map: delegate (IDataReader reader, short set)
                {
                    FollowingPlacesDomain p = new FollowingPlacesDomain();
                    int startingIndex = 0;
                    p.Id = reader.GetSafeInt32(startingIndex++);
                    p.Created = reader.GetDateTime(startingIndex++);
                    p.PlacesId = reader.GetSafeInt32(startingIndex++);
                    p.UserId = reader.GetSafeString(startingIndex++);

                    if (followingplaces == null)
                    {
                        followingplaces = new List<FollowingPlacesDomain>();
                    }
                    followingplaces.Add(p);
                }
                );
            return followingplaces;
        }

        // GET: All Users Following a Single Place by Places by Id
        public List<FollowingPlacesDomain> SelectAllByPlaceId(int PlaceId)
        {
            List<FollowingPlacesDomain> followingplaces = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.FollowingPlaces_SelectByPlaceId"
                , inputParamMapper: delegate (SqlParameterCollection ParamCollection)
                {
                    ParamCollection.AddWithValue("@PlaceId", PlaceId);
                }
                , map: delegate (IDataReader reader, short set)
                {
                    FollowingPlacesDomain p = new FollowingPlacesDomain();
                    int startingIndex = 0;
                    p.Id = reader.GetSafeInt32(startingIndex++);
                    p.Created = reader.GetSafeDateTime(startingIndex++);
                    p.PlacesId = reader.GetSafeInt32(startingIndex++);
                    p.UserId = reader.GetSafeString(startingIndex++);

                    if (followingplaces == null)
                    {
                        followingplaces = new List<FollowingPlacesDomain>();
                    }
                    followingplaces.Add(p);

                }
                );
            return followingplaces;
        }

        // GET: All Places Current User is Following
        public List<FollowingPlacesDomain> SelectAllByUserId(string userId)
        {
            List<FollowingPlacesDomain> ListDomain = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.FollowingPlaces_SelectByUserId"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                 {
                     paramCollection.AddWithValue("@UserId", userId);
                 }
                 , map: delegate (IDataReader reader, short set)
                  {
                      FollowingPlacesDomain p = new FollowingPlacesDomain();
                      int startingIndex = 0;
                      p.Id = reader.GetSafeInt32(startingIndex++);
                      p.Created = reader.GetSafeDateTime(startingIndex++);
                      p.PlacesId = reader.GetSafeInt32(startingIndex++);
                      p.UserId = reader.GetSafeString(startingIndex++);

                      if (ListDomain == null)
                      {
                          ListDomain = new List<FollowingPlacesDomain>();
                      }
                      ListDomain.Add(p);
                  }
                 );
            return ListDomain;
        }

        // GET: All UsersInformation that are Following a Single Place By Place Id
        public List<FollowingPlacesDomain> SelectUserInfoByPlaceId(int PlaceId)
        {
            List<FollowingPlacesDomain> followingplaces = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.FollowingPlaces_SelectUsersByPlaceId"
                , inputParamMapper: delegate (SqlParameterCollection ParamCollection)
                {
                    ParamCollection.AddWithValue("@PlaceId", PlaceId);
                }
                , map: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;

                    UserMini u = new UserMini();

                    u.Id = reader.GetSafeString(startingIndex++);
                    u.Email = reader.GetSafeString(startingIndex++);
                    u.FirstName = reader.GetSafeString(startingIndex++);
                    u.LastName = reader.GetSafeString(startingIndex++);
                    u.ProfileContent = reader.GetSafeString(startingIndex++);
                    u.TagLine = reader.GetSafeString(startingIndex++);
                    u.UserName = reader.GetSafeString(startingIndex++);
                    u.PointScore = reader.GetSafeInt32(startingIndex++);

                    Media m = new Media();

                    m.Id = reader.GetSafeInt32(startingIndex++);
                    m.Created = reader.GetSafeDateTime(startingIndex++);
                    m.UserId = reader.GetSafeString(startingIndex++);
                    m.MediaType = reader.GetSafeEnum<MediaType>(startingIndex++);
                    m.DataType = reader.GetSafeString(startingIndex++);
                    m.Title = reader.GetSafeString(startingIndex++);
                    m.Description = reader.GetSafeString(startingIndex++);
                    m.Url = reader.GetSafeString(startingIndex++);

                    if (u.Id != null)
                    {
                        u.Media = m;
                    }

                    FollowingPlacesDomain p = new FollowingPlacesDomain();

                    p.Id = reader.GetSafeInt32(startingIndex++);
                    p.Created = reader.GetSafeDateTime(startingIndex++);
                    p.PlacesId = reader.GetSafeInt32(startingIndex++);
                    p.UserId = reader.GetSafeString(startingIndex++);

                    if (p.PlacesId != 0)
                    {
                        p.UserMini = u;
                    }

                    if (followingplaces == null)
                    {
                        followingplaces = new List<FollowingPlacesDomain>();
                    }

                    followingplaces.Add(p);

                }
                );
            return followingplaces;
        }

        // DELETE: Remove a Follow of the Current User By User Id
        public void DeleteByUserId(FollowingPlacesRequest model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.FollowingPlaces_DeleteByUserId"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UserId", model.UserId);
                    paramCollection.AddWithValue("@PlaceId", model.PlaceId);
                }
                );
        }
        
        // CHECK: For data of Following Places table by User Id and Places Id
        public bool BoolSelectByPlaceIdUserId(int PlaceId, string UserId)
        {
            FollowingPlacesDomain p = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.FollowingPlaces_SelectByUserIdAndPlaceId"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UserId", UserId);
                    paramCollection.AddWithValue("@PlaceId", PlaceId);
                }
                 , map: delegate (IDataReader reader, short set)
                 {
                     p = new FollowingPlacesDomain();
                     int startingIndex = 0;
                     p.Id = reader.GetSafeInt32(startingIndex++);
                     p.Created = reader.GetSafeDateTime(startingIndex++);
                     p.PlacesId = reader.GetSafeInt32(startingIndex++);
                     p.UserId = reader.GetSafeString(startingIndex++);

                 }
                 );
            if (p != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
