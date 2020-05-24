using StatauIrPristatau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatauIrPristatau.Controllers
{
    public class RegisterController : Controller
    {
        [Route("Register")]
        public ActionResult Register()
        {
            return View("~/Views/User/Register.cshtml");
        }
        [Route("Login")]
        public ActionResult Login()
        {
            return View("~/Views/User/Login.cshtml");
        }
        [HttpPost]
        public ActionResult Register(User account)
        {
            if (ModelState.IsValid)
            {
                using (SIPDbContext db = new SIPDbContext())
                {
                    db.userAccount.Add(account);
                    db.SaveChanges();
                    Session["UserId"] = account.Id;
                    Session["UserName"] = account.Name.ToString();
                    Session["Surname"] = account.Surname.ToString();
                    ModelState.Clear();
                    if (Session["UserId"] != null)
                    {
                        return View("~/Views/Shared/MainView.cshtml", db.userAccount.ToList());
                    }
                    else
                    {
                        return View("~/Views/User/Register.cshtml");
                    }
                }
            }
            return View("~/Views/User/Register.cshtml");
        }

    }
}