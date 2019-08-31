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
            List<Users> Users;

            using (Model1 db = new Model1())
            {
                Users = db.Users.ToList();
            }

                return View(Users);
        }

        [HttpGet]
        public ActionResult CreateOrEdit(int? id)
        {
            Users user = new Users();

            if (id != null)
            {
                using (Model1 db = new Model1())
                {
                        user = db.Users.Where(x => x.Id == id).FirstOrDefault();
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