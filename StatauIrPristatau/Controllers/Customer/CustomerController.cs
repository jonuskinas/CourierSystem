using Microsoft.Ajax.Utilities;
using StatauIrPristatau.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatauIrPristatau.Controllers.Customer
{
    public class CustomerController : Controller
    {
        public ActionResult openCourierRatings(string searchString)
        {
            
            using (SIPDbContext db = new SIPDbContext())
            {
                if (!String.IsNullOrEmpty(searchString))
                {

                    var results = db.userAccount.Include(o => o.Rankings).Where(s => s.Email.Contains(searchString));
                  
                        
                    return View("~/Views/Customer/CourierRatingsView.cshtml", results.ToList());
                    
                }
                else
                {
                    var results = db.userAccount.Include(o => o.Rankings);
                    return View("~/Views/Customer/CourierRatingsView.cshtml", results.ToList());
                }

             
         
            }


        }
        [Route("openRatingsPop")]
        public ActionResult openRatingsPop()
        {
            PopulateDepartmentsDropDownList();
            return View("~/Views/Customer/openRatingsPop.cshtml");
        }
        [HttpPost]
        public ActionResult openRatingsPop(Ranking ranking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SIPDbContext db = new SIPDbContext())
                    {
                        db.rankings.Add(ranking);
                        db.SaveChanges();
                        ModelState.Clear();
                        var results = db.userAccount.Include(o => o.Rankings);
                        return View("~/Views/Customer/CourierRatingsView.cshtml", results.ToList());
                    }
                }
                else
                {
                    return View("~/Views/Customer/openRatingsPop.cshtml");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDepartmentsDropDownList(ranking.DepartmentID);
            return View("~/Views/Customer/openRatingsPop.cshtml");
        }
            // GET: Customer
            public ActionResult openOrderedShipments()
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                var results = db.orders.Where(o => o.State != 3).Include(o => o.Parcels);
                return View("~/Views/Customer/ShipmentsView.cshtml", results.ToList());
            }
        }
        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                var departmentsQuery = from d in db.userAccount
                                       orderby d.Email
                                       select d;
                ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "Email", selectedDepartment);
            }
        }

        public ActionResult openMap()
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                var results = db.orders.Where(o => o.State != 3).Include(o => o.Parcels);
                return View("~/Views/Customer/Map.cshtml", results.ToList());
            }
        }

        public JsonResult getPickUpAddress()
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                string[] currentOrder = new string[50];

                int k = 0;

                foreach (Parcel parcel in db.parcels)
                {
                    currentOrder[k] = parcel.Pickup_Address;
                    k++;
                }

                return Json(currentOrder, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getDeliveryAddress()
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                string[] currentOrder = new string[50];

                int k = 0;

                foreach (Parcel parcel in db.parcels)
                {
                    currentOrder[k] = parcel.DeliveryAddress;
                    k++;
                }

                return Json(currentOrder, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult openNewOrder()
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                int Id = (int)System.Web.HttpContext.Current.Session["UserId"];
                User currentUser = db.userAccount.Single(u => u.Id == Id);
                Order order = new Order();
                order.User = currentUser;
                order.Date = DateTime.Now;
                db.orders.Add(order);
                db.SaveChanges();
                ModelState.Clear();
                return View("~/Views/Customer/OrderShipmentView.cshtml", order);
            }

        }

        [HttpGet]
        public ActionResult updateOrder(string id, string value)
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                int ordId = int.Parse(id);
                int state = int.Parse(value);
                Order currentOrder = db.orders.Include(o => o.Parcels).Single(o => o.Id == ordId);
                currentOrder.Parcels.Select(cO => { cO.State = 1; return cO; }).ToList();
                currentOrder.State = state;
                db.SaveChanges();
                return View("~/Views/Customer/OrderShipmentView.cshtml", currentOrder);
            }
        }

        public ActionResult openAddParcelForm(int id)
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                ViewData["Message"] = id;
                return View("~/Views/Customer/addParcelView.cshtml");
            }
        }

        [HttpPost]
        public ActionResult fillOrderData(int id, Parcel parcel)
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                if (ModelState.IsValid)
                {
                    Order currentOrder = db.orders.Single(o => o.Id == id);
                    Tuple<Order, Parcel> tpl1 = PriceController.calculatePrice(currentOrder, parcel);
                    currentOrder = tpl1.Item1;
                    parcel = tpl1.Item2;
                    currentOrder.Parcels.Add(parcel);
                    db.SaveChanges();
                    return View("~/Views/Customer/OrderShipmentView.cshtml", currentOrder);
                }
                else
                {
                    ViewData["Message"] = id;
                    return View("~/Views/Customer/addParcelView.cshtml");
                }


            }

        }
    }
}