using GWIG.Web.Domain;
using GWIG.Web.Models.Requests.FollowingPlaces;
using GWIG.Web.Models.Requests.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWIG.Web.Services.Interface
{
    public interface IFollowingPlacesService
    {
        // POST: Current Users Follow to a Place
        string FollowingPlacesInsert(FollowingPlacesRequest model);

        // GET: All Places being Following in FollowingPlaces Table
        List<FollowingPlacesDomain> FollowingPlacesGetAll();

        // GET: All Users Following a place
        List<FollowingPlacesDomain> FollowingPlacesGetPlacesById(int placesId);

        // GET: All Places a User is Following
        List<FollowingPlacesDomain> FollowingPlacesGetUserById(FollowingPlacesRequest model);

        // GET/CHECK: If User is Following a Place
        bool FollowingPlacesGetUserByIdAndplacesId(int PlacesId, string UserId);

        // GET: All Users Following a Place by Place ID
        List<FollowingPlacesDomain> FollowingPlacesGetUserInfo(int placesId);

        // DELETE: Remove a Users follow to a Place
        void FollowingPlacesDelete(FollowingPlacesRequest model);
    }
}
