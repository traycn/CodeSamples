using GWIG.Web.Domain;
using GWIG.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWIG.Web.Services.Interface
{
    public interface IUserCheckIn
    {
        // POST: User Checkin and return value
        double Insert(UserCheckinRequest model);

        // GET: UserCheckin By User Id and Place Id
        UserCheckinRequest getByUserIdandPlacesId(UserCheckinRequest model);
    }
}
