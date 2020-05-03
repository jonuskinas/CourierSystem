using StatauIrPristatau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StatauIrPristatau.Controllers
{
    public class LoginController : Controller
    {
        [Route("Login")]
        public ActionResult Login()
        {
            return View("~/Views/User/Login/Login.cshtml");
        }
        [Route("Register")]
        public ActionResult Register()
        {
            return View("~/Views/User/Register/Register.cshtml");
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            using(SIPDbContext db = new SIPDbContext())
            {
                var usr = db.userAccount.Single(u => u.Email == user.Email && u.Password == user.Password);
                if (usr != null)
                {
                    Session["UserId"] = usr.Id.ToString();
                    Session["UserName"] = usr.Name.ToString();
                    Session["Surname"] = usr.Surname.ToString();
                    return RedirectToAction("MainView");
                }
                else
                {
                    ModelState.AddModelError("", "E-pašto adresas arba slaptažodis neteisingi");
                }
            }
            return View("~/Views/User/Login/Login.cshtml");
        }

        public ActionResult MainView()
        {
            if (Session["UserId"] != null)
            {
                using(SIPDbContext db = new SIPDbContext())
                {
                    return View("~/Views/Shared/MainView.cshtml", db.userAccount.ToList());
                }
                
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}