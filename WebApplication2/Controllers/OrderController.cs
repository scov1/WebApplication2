using AutoMapper;
using BL.BO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        //public ActionResult Link(Orders order)
        //{
        //    Users user;

        //    using (Model1 db = new Model1())
        //    {
        //        user = db.Users.Where(x => x.Id == order.Id).FirstOrDefault();
        //    }

        //    MailAddress from = new MailAddress("seda8888-92@mail.ru", "Верните книгу");
        //    MailAddress to = new MailAddress(user.Email);
        //    MailMessage m = new MailMessage(from, to);
        //    m.Subject = "Верните книгу";
        //    m.Body = string.Format("Верните книгу,вы просрочили книгу");
        //    m.IsBodyHtml = true;
        //    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
        //    smtp.Credentials = new NetworkCredential("seda8888-92@mail.ru", "seda8899");
        //    smtp.EnableSsl = true;
        //    smtp.Send(m);
        //    return RedirectToAction("Index");

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
            //var orderBO = DependencyResolver.Current.GetService<OrderBO>();
            //var userBO = DependencyResolver.Current.GetService<UserBO>();
            //var bookBO = DependencyResolver.Current.GetService<BookBO>();

            //ViewBag.Orders = orderBO.GetOrdersList().Select(m => mapper.Map<OrderView>(m)).ToList();
            //ViewBag.Users = userBO.GetUsersList().Select(m => mapper.Map<UserView>(m)).ToList();
            //ViewBag.Books = bookBO.GetBooksList().Select(m => mapper.Map<BookView>(m)).ToList();

            //return View();
            var orderBO = DependencyResolver.Current.GetService<OrderBO>();
            var userBO = DependencyResolver.Current.GetService<UserBO>();
            var bookBO = DependencyResolver.Current.GetService<BookBO>();

            ViewBag.Users = userBO.GetUsersList().Select(m => mapper.Map<UserView>(m)).ToList();
            ViewBag.Books = bookBO.GetBooksList().Select(m => mapper.Map<BookView>(m)).ToList();

            if (Request.IsAjaxRequest())
            {
                if (sort == "Creation Date")
                {
                    var orders = orderBO.GetOrdersList().Select(m => mapper.Map<OrderView>(m)).ToList();
                    ViewBag.Orders = orders.OrderBy(o => o.CreationDate);
                }
                else if (sort == "None")
                    ViewBag.Orders = orderBO.GetOrdersList().Select(m => mapper.Map<OrderView>(m)).ToList();
                return PartialView("Partial/OrderPartialView");
            }
            else
            {
                ViewBag.Orders = orderBO.GetOrdersList().Select(m => mapper.Map<OrderView>(m)).ToList();
            }

            return View();
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            //var userBO = DependencyResolver.Current.GetService<UserBO>();
            //var bookBO = DependencyResolver.Current.GetService<BookBO>();

            //var orderBO = DependencyResolver.Current.GetService<OrderBO>();
            //var OrdersView = orderBO.GetOrdersListById(id);
            //var model = mapper.Map<OrderView>(OrdersView);

            //if (id != null)
            //{
            //    var orderBOList = orderBO.GetOrdersListById(id);
            //    model = mapper.Map<OrderView>(orderBOList);
            //    ViewBag.Message = "Edit";
            //}
            //else ViewBag.Message = "Create";

            //ViewBag.Users = new SelectList(userBO.GetUsersList().Select(m => mapper.Map<UserView>(m)).ToList(), "Id", "FIO");
            //ViewBag.Books = new SelectList(bookBO.GetBooksList().Select(m => mapper.Map<BookView>(m)).ToList(), "Id", "Title");

            //return View(model);
            var userBO = DependencyResolver.Current.GetService<UserBO>();
            var bookBO = DependencyResolver.Current.GetService<BookBO>();

            var orderBO = DependencyResolver.Current.GetService<OrderBO>();
            var model = mapper.Map<OrderView>(orderBO);

            if (id != null)
            {
                var orderBOList = orderBO.GetOrdersListById(id);
                model = mapper.Map<OrderView>(orderBOList);
                ViewBag.Message = "Edit";
            }
            else ViewBag.Message = "Create";

            model.ReturnDate = null;

            ViewBag.Users = new SelectList(userBO.GetUsersList().Select(m => mapper.Map<UserView>(m)).ToList(), "Id", "FIO");
            ViewBag.Books = new SelectList(bookBO.GetBooksList().Select(m => mapper.Map<BookView>(m)).ToList(), "Id", "Title");

            return View(model);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(OrderView model)
        {
            //var orderBO = mapper.Map<OrderBO>(model);
            //if (model.Id == 0)
            //{
            //    var allow = orderBO.GetOrdersList().Select(m => mapper.Map<OrderView>(m)).Where(o => o.UserId == model.UserId).ToList();
            //    var list = allow.Where(a => a.Period < DateTime.Today && a.CreationDate == a.ReturnDate).ToList();

            //    if (list.Count == 0)
            //    {
            //        orderBO.CreationDate = DateTime.Today;
            //        if (model.ReturnDate == null) orderBO.ReturnDate = DateTime.Today;
            //        orderBO.Save();
            //    }
            //}
            //else
            //{
            //    orderBO.CreationDate = DateTime.Today;
            //    if (model.ReturnDate == null) orderBO.ReturnDate = DateTime.Today;
            //    orderBO.Save();
            //}

            //return RedirectToActionPermanent("Index", "Order");

            var orderBO = mapper.Map<OrderBO>(model);

            if (model.Id == 0)
            {
                var allow = orderBO.GetOrdersList().Select(m => mapper.Map<OrderView>(m)).Where(o => o.UserId == model.UserId).ToList();
                var list = allow.Where(a => a.Period < DateTime.Today && a.CreationDate == a.ReturnDate).ToList();

                if (list.Count == 0)
                {
                    orderBO.CreationDate = DateTime.Today;
                    if (model.ReturnDate == null) orderBO.ReturnDate = orderBO.CreationDate;
                    orderBO.Save();
                }
            }
            else
            {
                if (model.ReturnDate == null) orderBO.ReturnDate = orderBO.CreationDate;
                orderBO.Save();
            }

            return RedirectToActionPermanent("Index", "Order");
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            var orderBO = DependencyResolver.Current.GetService<OrderBO>().GetOrdersListById(id);
            orderBO.Delete(id);

            return RedirectToActionPermanent("Index", "Order");
        }

        public ActionResult SendEmail(int id)
        {
            var orderBO = DependencyResolver.Current.GetService<OrderBO>().GetOrdersListById(id);
            var order = mapper.Map<OrderView>(orderBO);
            var bookBO = DependencyResolver.Current.GetService<BookBO>().GetBooksListById(order.BookId);
            var titleBook = mapper.Map<BookView>(bookBO);
            var userBO = DependencyResolver.Current.GetService<UserBO>().GetUsersListById(order.UserId);
            var userMail = mapper.Map<UserView>(userBO);


          

            MailAddress from = new MailAddress("seda8888-92@mail.ru", "Верните книгу");
            MailAddress to = new MailAddress(userMail.Email);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Верните книгу";
            message.Body = string.Format("Верните книгу,вы ее уже просрочили");
            message.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            smtp.Credentials = new NetworkCredential("seda8888-92@mail.ru", "");
            smtp.EnableSsl = true;
            smtp.Send(message);

            return RedirectToActionPermanent("Index", "Order");
        }

        public ActionResult FileDeadline()
        {
            List<OrderView> deadlines = new List<OrderView>();

            var orderBO = DependencyResolver.Current.GetService<OrderBO>().GetOrdersList();
            var userBO = DependencyResolver.Current.GetService<UserBO>();

            deadlines = orderBO.Select(m => mapper.Map<OrderView>(m)).Where(o => o.CreationDate == o.ReturnDate && DateTime.Today > o.Period).ToList();
            string path = @"C:\Test\deadline.txt";

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Unicode))
                {
                    foreach (var item in deadlines)
                    {
                        var user = mapper.Map<UserView>(userBO.GetUsersListById(item.UserId));
                        string fio = user.FIO;
                        sw.WriteLine($"User: {fio}   CreationDate: {item.CreationDate}  Period: {item.Period}");
                    }
                }
            }

            #region MemoryStream
            //    //byte[] data = new byte[5000];
            //    //MemoryStream ms = new MemoryStream(data);
            //    //StreamWriter sw = new StreamWriter(ms);

            //    //foreach (var item in links)
            //    //    if (item.Deadline < DateTime.Now)
            //    //    {
            //    //        string fio = db.Users.Where(u => u.Id == item.UserId).FirstOrDefault().FIO;
            //    //        sw.WriteLine($"User: {fio}   CreationDate: {item.CreationDate}  Deadline: {item.Deadline}");
            //    //    }
            //    //sw.Flush();
            //    //sw.Close();
            //    ////sr.Close();
            //    //string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //    //FileStream file = new FileStream($@"{dir}\test.txt", FileMode.OpenOrCreate);
            //    //ms.CopyTo(file);
            //    ////return File(ms, "text/plain");
            #endregion

            return RedirectToActionPermanent("Index", "Order");
        }
    }
}