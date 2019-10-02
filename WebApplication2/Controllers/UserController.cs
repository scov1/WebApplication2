using AutoMapper;
using BL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        //public ActionResult Index()
        //{
        //    List<Users> users;

        //    using (Model1 db = new Model1())
        //    {
        //        users = db.Users.ToList();
        //    }

        //    return View(users);
        //}

        //public ActionResult History()
        //{

        //    return PartialView();
        //}

        //[HttpGet]
        //public ActionResult CreateOrEdit(int? id)
        //{
        //    Users user = new Users();

        //    if (id == 0)
        //    {
        //        using (Model1 db = new Model1())
        //        {
        //            user = db.Users.Where(x => x.Id == id).FirstOrDefault();
        //            List<Orders> historyBooks = db.Orders.Where(i => i.UserId == id).ToList();
        //            ViewBag.BooksList = historyBooks;
        //        }
        //    }
        //    else
        //    {
        //        using (Model1 db = new Model1())
        //        {

        //            ViewBag.Comment = "";
        //            List<Orders> historyBooks = db.Orders.Where(i => i.UserId == id).ToList();
        //            ViewBag.BooksList = historyBooks;
        //            ViewData["BookList"] = historyBooks;
        //        }
        //    }
        //    return View(user);
        //}

        //[HttpPost]
        //public ActionResult CreateOrEdit(Users user)
        //{
        //    using (Model1 db = new Model1())
        //    {
        //        if (user.Id != 0)
        //        {
        //            var oldUser = db.Users.Where(x => x.Id == user.Id).FirstOrDefault();
        //            oldUser.FIO = user.FIO;
        //            oldUser.Email = user.Email;
        //        }
        //        else
        //        {
        //            db.Users.Add(user);

        //        }
        //        db.SaveChanges();
        //    }
        //    return RedirectToActionPermanent("Index", "User");
        //}

        //public ActionResult Delete(int id)
        //{
        //    using (Model1 db = new Model1())
        //    {
        //        var user = db.Users.Where(x => x.Id == id).FirstOrDefault();
        //        db.Users.Remove(user);
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Index", "User");
        //}
        protected IMapper mapper;

        public UserController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var userBO = DependencyResolver.Current.GetService<UserBO>();
            var userList = userBO.GetUsersList();
            ViewBag.Users = userList.Select(m => mapper.Map<UserView>(m)).ToList();

            return View();
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            var userBO = DependencyResolver.Current.GetService<UserBO>();
            var model = mapper.Map<UserView>(userBO);
            if (id != null)
            {
                var userList = userBO.GetUsersListById(id);
                model = mapper.Map<UserView>(userList);
                ViewBag.Message = "Edit";
            }
            else ViewBag.Message = "Create";

            return View(model);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(UserView model)
        {
            var userBO = mapper.Map<UserBO>(model);
            userBO.Save();

            return RedirectToActionPermanent("Index", "User");
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = DependencyResolver.Current.GetService<UserBO>().GetUsersListById(id);
            user.Delete(id);

            return RedirectToActionPermanent("Index", "User");
        }

        public ActionResult _UsersOrders(int id)
        {
            var orders = DependencyResolver.Current.GetService<OrderBO>();
            var userOrders = orders.GetOrdersList().Where(o => o.UserId == id).ToList();
            var books = DependencyResolver.Current.GetService<BookBO>().GetBooksList();
            var authors = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsList();

            ViewBag.TopOrders = userOrders.Select(m => mapper.Map<OrderView>(m)).ToList().Distinct().Take(5);
            ViewBag.Books = books.Select(m => mapper.Map<BookView>(m)).ToList();
            ViewBag.Authors = authors.Select(m => mapper.Map<AuthorView>(m)).ToList();

            return PartialView("Partial/_UsersOrders");
        }
    }
}