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
            return View("~/Views/User/Register/Register.cshtml");
        }
        [Route("Login")]
        public ActionResult Login()
        {
            return View("~/Views/User/Login/Login.cshtml");
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
                    Session["UserId"] = account.Id.ToString();
                    Session["UserName"] = account.Name.ToString();
                    Session["Surname"] = account.Surname.ToString();
                    ModelState.Clear();
                    if (Session["UserId"] != null)
                    {
                        return View("~/Views/Shared/MainView.cshtml", db.userAccount.ToList());
                    }
                }
            }
            return View("~/Views/User/Register/Register.cshtml");
        }

    }
}