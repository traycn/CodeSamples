using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GWIG.Web.Models.Requests
{
    public class UserCheckinRequest
    {
        public string UserId { get; set; }
        public int PlacesId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int MissionId { get; set; }
    }
}