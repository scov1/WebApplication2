//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using WebApplication2;


//namespace WebApplication1.Controllers
//{
//    public class BookController : Controller
//    {
//        // GET: Author
//        public ActionResult Index()
//        {
//            List<Books> books;

//            using (Model1 db = new Model1())
//            {
//                books = db.Books.ToList();
//            }

//            return View(books);
//        }



//        [HttpGet]
//        public ActionResult CreateOrEdit(int? id)
//        {
//            Books book = new Books();

//            if (id != null)
//                using (Model1 db = new Model1())
//                {
//                    book = db.Books.Where(x => x.Id == id).FirstOrDefault();
//                }
//            return View(book);
//        }


//        [HttpPost]
//        public ActionResult CreateOrEdit(Books book)
//        {

//            using (Model1 db = new Model1())
//            {
//                if (book.Id != 0)
//                {
//                    var oldBook = db.Books.Where(x => x.Id == book.Id).FirstOrDefault();
//                    oldBook.Title = book.Title;
//                    oldBook.Price = book.Price;
//                    oldBook.Pages = book.Pages;
//                    db.SaveChanges();
//                }
//                else
//                {
//                    db.Books.Add(book);
//                    db.SaveChanges();
//                }

//            }

//            return RedirectToActionPermanent("Index", "Book");
//        }

//        public ActionResult Delete(int id)
//        {
//            using (Model1 db = new Model1())
//            {
//                var book = db.Books.Where(a => a.Id == id).FirstOrDefault();
//                db.Books.Remove(book);
//                db.SaveChanges();
//            }
//            return RedirectToAction("Index", "Book");
//        }
//    }
//}