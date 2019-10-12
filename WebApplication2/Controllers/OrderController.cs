using AutoMapper;
using BL.BO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        //public ActionResult Index()
        //{
        //    List<Orders> orders;

        //    using (Model1 db = new Model1())
        //    {

        //        orders = db.Orders.ToList();


        //    }

        //    return View(orders);
        //}



        //[HttpGet]
        //public ActionResult EditOrCreateOrder(int? id)
        //{
        //    Orders order = new Orders();


        //    if (id == null)
        //        using (Model1 db = new Model1())
        //        {
        //            SelectList users = new SelectList(db.Users.ToList(), "Id", "FIO");
        //            ViewBag.Users = users;

        //            SelectList books = new SelectList(db.Books.ToList(), "Id", "Title");
        //            ViewBag.Books = books;

        //            //order = db.Orders.Where(x => x.Id == id).FirstOrDefault();
        //        }

        //    return View(order);
        //}

        //[HttpPost]
        //public ActionResult EditOrCreateOrder(Orders order)
        //{
        //    using (Model1 db = new Model1())
        //    {
        //        SelectList users = new SelectList(db.Users.ToList(), "Id", "FIO");
        //        ViewBag.Users = users;

        //        SelectList books = new SelectList(db.Books.ToList(), "Id", "Title");
        //        ViewBag.Books = books;

        //        if (order.Id != 0)
        //        {
        //            var Oldorder = db.Orders.Where(x => x.Id == order.Id).FirstOrDefault();
        //            Oldorder.BookId = order.BookId;
        //            Oldorder.UserId = order.UserId;
        //            //Oldorder.AuthorName = order.AuthorName;
        //            //Oldorder.BookName = order.BookName;
        //        }

        //        else
        //        {
        //            db.Orders.Add(order);

        //        }
        //        db.SaveChanges();
        //    }

        //    return RedirectToActionPermanent("Index", "Order");
        //}

        //public ActionResult Delete(int id)
        //{
        //    using (Model1 db = new Model1())
        //    {
        //        var order = db.Orders.Where(x => x.Id == id).FirstOrDefault();
        //        db.Orders.Remove(order);
        //        db.SaveChanges();
        //    }

        //    return RedirectToAction("Index", "Order");
        //}

     

        //public ActionResult Download()
        //{
        //    using (Model1 db = new Model1())
        //    {
        //        List<Orders> orders = db.Orders.ToList();
        //        foreach (var item in orders)
        //        {
        //            List<Users> users = db.Users.Where(a => a.Id == item.UserId).ToList();
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}

        protected IMapper mapper;

        public OrderController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index(string sort)
        {
     
            var orderBO = DependencyResolver.Current.GetService<OrderBO>();
            var userBO = DependencyResolver.Current.GetService<UserBO>();
            var bookBO = DependencyResolver.Current.GetService<BookBO>();

            ViewBag.Users = userBO.GetUsersList().Select(m => mapper.Map<UserView>(m)).ToList();
            ViewBag.Books = bookBO.GetBooksList().Select(m => mapper.Map<BookView>(m)).ToList();

            if (Request.IsAjaxRequest())
            {
                if (sort == "Period")
                {
                    var orders = orderBO.GetOrdersList().Select(x => mapper.Map<OrderView>(x)).ToList();
                    ViewBag.Orders = orders.OrderBy(o => o.Period);

                }
                else if (sort == "None")
                {
                    ViewBag.Orders = orderBO.GetOrdersList().Select(x => mapper.Map<OrderView>(x)).ToList();
                }
                 
                return PartialView("Partial/OrderPartialView");
            }
            else
            {
                ViewBag.Orders = orderBO.GetOrdersList().Select(m => mapper.Map<OrderView>(m)).ToList();
            }

            return View();
        }

     
        public ActionResult Edit(int? id)
        {
            var userBO = DependencyResolver.Current.GetService<UserBO>();
            var bookBO = DependencyResolver.Current.GetService<BookBO>();

            var orderBO = DependencyResolver.Current.GetService<OrderBO>();
            var model = mapper.Map<OrderView>(orderBO);

            if (id != null)
            {
                var orderBOList = orderBO.GetOrdersListById(id);
                model = mapper.Map<OrderView>(orderBOList);
               
            }

            model.ReturnDate = null;

            ViewBag.Users = new SelectList(userBO.GetUsersList().Select(m => mapper.Map<UserView>(m)).ToList(), "Id", "FIO");
            ViewBag.Books = new SelectList(bookBO.GetBooksList().Select(m => mapper.Map<BookView>(m)).ToList(), "Id", "Title");

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(OrderView model)
        {
            if (model.Id == 0)
            {
                var orderBO = mapper.Map<OrderBO>(model);
                var check = orderBO.GetOrdersList().Select(x => mapper.Map<OrderView>(x)).Where(z => z.UserId == model.UserId).ToList();
                var list = check.Where(y => y.Period < DateTime.Today && y.CreationDate == y.ReturnDate).ToList();

                if (list.Count == 0)
                {
                    orderBO.CreationDate = DateTime.Today;

                    if (model.ReturnDate == null)
                    {
                        orderBO.ReturnDate = orderBO.CreationDate;
                    } 
                    orderBO.Save();
                }
            }
            else
            {
                if (model.ReturnDate == null)
                {
                    model.ReturnDate = DateTime.Now;
                }

                model.CreationDate = DateTime.Today;
                var orderBO = mapper.Map<OrderBO>(model);
                orderBO.Save();
            }

            return RedirectToActionPermanent("Index", "Order");
        }

        public ActionResult Delete(int id)
        {
            var orderBO = DependencyResolver.Current.GetService<OrderBO>().GetOrdersListById(id);
            orderBO.Delete(id);

            return RedirectToActionPermanent("Index", "Order");
        }

        public ActionResult Email(int id)
        {
            var orderBO = DependencyResolver.Current.GetService<OrderBO>().GetOrdersListById(id);
            var order = mapper.Map<OrderView>(orderBO);
            var bookBO = DependencyResolver.Current.GetService<BookBO>().GetBooksListById(order.BookId);
            var titleBook = mapper.Map<BookView>(bookBO);
            var userBO = DependencyResolver.Current.GetService<UserBO>().GetUsersListById(order.UserId);
            var userMail = mapper.Map<UserView>(userBO);


            MailAddress from = new MailAddress("seda8888-92@mail.ru", "Верните книгу!");
            MailAddress to = new MailAddress(userMail.Email);
            MailMessage mes = new MailMessage(from, to);
            mes.Subject = "Приветики,пистолетики";
            mes.Body = string.Format("Верните книгу,вы ее уже просрочили,у вас совесть есть??");
            mes.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            smtp.Credentials = new NetworkCredential("seda8888-92@mail.ru", "zvezda8899");
            smtp.EnableSsl = true;
            smtp.Send(mes);

            return RedirectToActionPermanent("Index", "Order");
        }

        public ActionResult Dowloand()
        {
            List<OrderView> debtor = new List<OrderView>();

            var orderBO = DependencyResolver.Current.GetService<OrderBO>().GetOrdersList();
            var userBO = DependencyResolver.Current.GetService<UserBO>();

           
            debtor = orderBO.Select(m => mapper.Map<OrderView>(m)).Where(o =>DateTime.Today > o.Period).ToList();
  
            StringBuilder sb = new StringBuilder();
            string about = "\tUser\tBook\tReturn";
            sb.Append(about);
     
            foreach (var item in debtor)
            {
                var user = mapper.Map<UserView>(userBO.GetUsersListById(item.UserId));
                string fio = user.FIO;
                sb.Append($"\n\tUser: {fio}  \tCreationDate: {item.CreationDate}  \tPeriod: {item.Period}");
            }
            byte[] data = Encoding.ASCII.GetBytes(sb.ToString());

            string contentType = "text/plain";
            return File(data, contentType, "orders.txt");
        }
    }
}