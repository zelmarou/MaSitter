using MaSitter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MaSitter.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace MaSitter.Core
{
    public class Helper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public PersonalSpaceModel MyPersonnalSpace
        {
            get
            {
                var currentUserGUID = new Guid(HttpContext.Current.User.Identity.GetUserId());
                return db.PersonalSpaceModels.Single(e => e.user_id == currentUserGUID);
            }
        }

    }
}
