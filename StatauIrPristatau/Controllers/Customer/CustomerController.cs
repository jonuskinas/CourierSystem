using StatauIrPristatau.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatauIrPristatau.Controllers.Customer
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult openOrderedShipments()
        {
            using (SIPDbContext db = new SIPDbContext())
            {
                var results = db.orders.Where(o => o.State != 3).Include(o => o.Parcels);
                return View("~/Views/Customer/ShipmentsView.cshtml", results.ToList());
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