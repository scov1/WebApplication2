using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Entities;

namespace WebApplication2.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            List<Users> users;

            using (Model1 db = new Model1())
            {
                users = db.Users.ToList();
            }

            return View(users);
        }

        public ActionResult History()
        {
   
            return PartialView();
        }

        [HttpGet]
        public ActionResult CreateOrEdit(int? id)
        {
            Users user = new Users();

            if (id == 0)
            {
                using (Model1 db = new Model1())
                {
                    user = db.Users.Where(x => x.Id == id).FirstOrDefault();
                    List<Orders> historyBooks = db.Orders.Where(i => i.UserId == id).ToList();
                    ViewBag.BooksList = historyBooks;
                }
            }
            else
            {
                using (Model1 db = new Model1())
                {

                    ViewBag.Comment = "";
                    List<Orders> historyBooks = db.Orders.Where(i => i.UserId == id).ToList();
                    ViewBag.BooksList = historyBooks;
                    ViewData["BookList"] = historyBooks;
                }
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult CreateOrEdit(Users user)
        {
            using (Model1 db = new Model1())
            {
                if(user.Id!=0)
                {
                    var oldUser = db.Users.Where(x => x.Id == user.Id).FirstOrDefault();
                    oldUser.FIO = user.FIO;
                    oldUser.Email = user.Email;
                }
                else
                {
                    db.Users.Add(user);
                  
                }
                db.SaveChanges();
            }
            return RedirectToActionPermanent("Index", "User");
        }

        public ActionResult Delete(int id)
        {
            using (Model1 db = new Model1())
            {
               var user = db.Users.Where(x => x.Id == id).FirstOrDefault();
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "User");
        }
    }
}