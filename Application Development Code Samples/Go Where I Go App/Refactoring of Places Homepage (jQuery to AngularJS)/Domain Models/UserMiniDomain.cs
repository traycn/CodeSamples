using GWIG.Web.Domain.MyMedia;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GWIG.Web.Domain
{
    public class UserMini
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileContent { get; set; }

        public string TagLine { get; set; }

        public string UserName { get; set; }
        
        public string UserId { get; set; }

        public Media Media { get; set; }

        public int PointScore { get; set; }
    }
}