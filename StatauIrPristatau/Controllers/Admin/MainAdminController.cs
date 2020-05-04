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

        [HttpPost]
        public ActionResult Edit(User account)
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                if (ModelState.IsValid)
                {
                    var result = db.userAccount.Single(u => u.Email.ToString() == account.Email.ToString());
                    if (result != null)
                    {
                        result.Name = account.Name;
                        result.Surname = account.Surname;
                        result.Password = account.Password;
                        result.YearOfBirth = account.YearOfBirth;
                        result.Address = account.Address;
                        db.SaveChanges();
                    }
               
                }
                return View("~/Views/Shared/MainView.cshtml", db.userAccount.ToList());
            }
        }

        public ActionResult Edit(int Id)
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                if (Session["UserId"] != null)
                {
                    var usr = db.userAccount.Single(u => u.Id == Id);
                    return View("~/Views/Admin/MainAdmin/EditView.cshtml", usr);
                }
                else
                {
                    return View("~/Views/Shared/MainView.cshtml");
                }
            }
        }

        public ActionResult Delete(int Id)
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                if (Session["UserId"] != null)
                {
                    db.userAccount.Remove(db.userAccount.Single(a => a.Id == Id));
                    db.SaveChanges();
                }
                return View("~/Views/Shared/MainView.cshtml", db.userAccount.ToList());
            }
        }

        [Route("Registration")]
        public ActionResult Registration()
        {
            return View("~/Views/Admin/Registration/RegistrationScreen.cshtml");
        }


        public ActionResult ToMainView()
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                return View("~/Views/Shared/MainView.cshtml", db.userAccount.ToList());
            }
        }
    }
}