using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StatauIrPristatau.Models;
using System.Web.Mvc;
using System.Data.Entity;

namespace StatauIrPristatau.Controllers
{
    public class MainUserController : Controller
    {
        // GET: MainUser
        public ActionResult openOrderHistory()
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                var results = db.orders.Where(o => o.State == 3).Include(o => o.Parcels);
                return View("~/Views/User/OrderView.cshtml", results.ToList());
            }
        }
    }
}