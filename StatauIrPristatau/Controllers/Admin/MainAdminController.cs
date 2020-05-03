using StatauIrPristatau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatauIrPristatau.Controllers.Admin
{
    public class MainAdminController : Controller
    {
        public ActionResult Details(int Id)
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                if (Session["UserId"] != null)
                {
                    var usr = db.userAccount.Single(u => u.Id == Id);
                    return View("~/Views/Admin/MainAdmin/UserView.cshtml", usr);
                }
                else
                {
                    return View("~/Views/User/Login/Login.cshtml");
                }

            }
        }
    }
}