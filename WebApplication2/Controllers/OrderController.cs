using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

            return View(orders);
        }



        [HttpGet]
        public ActionResult EditOrCreateOrder(int? id)
        {
            Orders order = new Orders();


            if (id == null)
                using (Model1 db = new Model1())
                {
                    SelectList users = new SelectList(db.Users.ToList(), "Id", "FIO");
                    ViewBag.Users = users;

                    SelectList books = new SelectList(db.Books.ToList(), "Id", "Title");
                    ViewBag.Books = books;

                    //order = db.Orders.Where(x => x.Id == id).FirstOrDefault();
                }

            return View(order);
        }

        [HttpPost]
        public ActionResult EditOrCreateOrder(Orders order)
        {
            using (Model1 db = new Model1())
            {
                SelectList users = new SelectList(db.Users.ToList(), "Id", "FIO");
                ViewBag.Users = users;

                SelectList books = new SelectList(db.Books.ToList(), "Id", "Title");
                ViewBag.Books = books;

                if (order.Id != 0)
                {
                    var Oldorder = db.Orders.Where(x => x.Id == order.Id).FirstOrDefault();
                    Oldorder.BookId = order.BookId;
                    Oldorder.UserId = order.UserId;
                    //Oldorder.AuthorName = order.AuthorName;
                    //Oldorder.BookName = order.BookName;
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

        public ActionResult Link(Orders order)
        {
            Users user;

            using (Model1 db = new Model1())
            {
                user = db.Users.Where(x => x.Id == order.Id).FirstOrDefault();
            }

            MailAddress from = new MailAddress("seda8888-92@mail.ru", "Верните книгу");
            MailAddress to = new MailAddress(user.Email);
            MailMessage m = new MailMessage(from, to);
            m.Subject="Верните книгу";
            m.Body = string.Format("Верните книгу,вы просрочили книгу");
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            smtp.Credentials = new NetworkCredential("seda8888-92@mail.ru", "seda8899");
            smtp.EnableSsl = true;
            smtp.Send(m);
            return RedirectToAction("Index");

        }

        public ActionResult Download()
        {
            using (Model1 db = new Model1())
            {
                List<Orders> orders = db.Orders.ToList();
                foreach (var item in orders)
                {
                    List<Users> users = db.Users.Where(a => a.Id == item.UserId).ToList();
                }
            }
            return RedirectToAction("Index");
        }
    }
}