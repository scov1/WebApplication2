using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Entities;

namespace WebApplication2.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            List<Orders> orders;

            using (Model1 db = new Model1())
            {
                orders = db.Orders.ToList();
            }

            return View();
        }

        [HttpGet]
        public ActionResult EditOrCreateOrder(int? id)
        {
            Orders order = new Orders();


            if (id != null)
                using (Model1 db = new Model1())
                {
                    order = db.Orders.Where(x => x.Id == id).FirstOrDefault();
                }

            return View(order);
        }

        [HttpPost]
        public ActionResult EditOrCreateOrder(Orders order)
        {
            using (Model1 db = new Model1())
            {
                if (order.Id != 0)
                {
                    var Oldorder = db.Orders.Where(x => x.Id == order.Id).FirstOrDefault();
                    order.BookId = Oldorder.BookId;
                    order.UserId = Oldorder.UserId;
                }

                else
                {
                    db.Orders.Add(order);

                }
                db.SaveChanges();
            }

            return RedirectToActionPermanent("Index", "Order");
        }

        public ActionResult Delete(int id)
        {
            using (Model1 db = new Model1())
            {
                var order = db.Orders.Where(x => x.Id == id).FirstOrDefault();
                db.Orders.Remove(order);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Order");
        }
    }
}