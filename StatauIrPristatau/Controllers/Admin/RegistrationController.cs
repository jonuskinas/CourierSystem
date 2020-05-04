using StatauIrPristatau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatauIrPristatau.Controllers.Admin
{
    public class RegistrationController : Controller
    {
        [HttpPost]
        public ActionResult Registration(User account)
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                if (ModelState.IsValid)
                {
       
                        db.userAccount.Add(account);
                        db.SaveChanges();
                        ModelState.Clear();
                }
                return View("~/Views/Shared/MainView.cshtml", db.userAccount.ToList());

            }

        }
    }
}