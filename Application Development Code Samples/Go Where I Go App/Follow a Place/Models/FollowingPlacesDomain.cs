using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GWIG.Web.Domain
{
    public class FollowingPlacesDomain
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int PlacesId { get; set; }
        public string UserId { get; set; }
        public decimal Rating { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int MediaId { get; set; }
        public string Url { get; set; }
    }
}