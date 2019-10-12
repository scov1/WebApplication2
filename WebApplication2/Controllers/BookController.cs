using AutoMapper;
using BL.BO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class BookController : Controller
    {
        //// GET: Author
        //public ActionResult Index()
        //{
        //    List<Books> books;

        //    using (Model1 db = new Model1())
        //    {
        //        books = db.Books.ToList();
        //    }

        //    return View(books);
        //}



        //[HttpGet]
        //public ActionResult CreateOrEdit(int? id)
        //{
        //    Books book = new Books();

        //    if (id != null)
        //        using (Model1 db = new Model1())
        //        {
        //            book = db.Books.Where(x => x.Id == id).FirstOrDefault();
        //        }
        //    return View(book);
        //}


        //[HttpPost]
        //public ActionResult CreateOrEdit(Books book)
        //{

        //    using (Model1 db = new Model1())
        //    {
        //        if (book.Id != 0)
        //        {
        //            var oldBook = db.Books.Where(x => x.Id == book.Id).FirstOrDefault();
        //            oldBook.Title = book.Title;
        //            oldBook.Price = book.Price;
        //            oldBook.Pages = book.Pages;
        //            db.SaveChanges();
        //        }
        //        else
        //        {
        //            db.Books.Add(book);
        //            db.SaveChanges();
        //        }

        //    }

        //    return RedirectToActionPermanent("Index", "Book");
        //}

        //public ActionResult Delete(int id)
        //{
        //    using (Model1 db = new Model1())
        //    {
        //        var book = db.Books.Where(a => a.Id == id).FirstOrDefault();
        //        db.Books.Remove(book);
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Index", "Book");
        //}

        protected IMapper mapper;

        public BookController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var bookBO = DependencyResolver.Current.GetService<BookBO>();

            var bookList = bookBO.GetBooksList();
            var authorList = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsList();
            var genreList = DependencyResolver.Current.GetService<GenreBO>().GetGenreList();

            ViewBag.Books = bookList.Select(x => mapper.Map<BookView>(x)).ToList();
            ViewBag.Authors = authorList.Select(x => mapper.Map<AuthorView>(x)).ToList();
            ViewBag.Genres = genreList.Select(x => mapper.Map<GenreView>(x)).ToList();

            return View();
        }


        public ActionResult Edit(int? id)
        {
            var bookBO = DependencyResolver.Current.GetService<BookBO>();
            var authors = DependencyResolver.Current.GetService<AuthorBO>();
            var model = mapper.Map<BookView>(bookBO);
            var genres = DependencyResolver.Current.GetService<GenreBO>();

            if (id != null)
            {
                var bookBOList = bookBO.GetBooksListById(id);
                model = mapper.Map<BookView>(bookBOList);
            }

            ViewBag.Authors = new SelectList(authors.GetAuthorsList().Select(x => mapper.Map<AuthorView>(x)).ToList(), "Id", "LastName");
            ViewBag.Genres = new SelectList(genres.GetGenreList().Select(x => mapper.Map<GenreView>(x)).ToList(), "Id", "Name");

            return View(model);
        }

    
        [HttpPost]
        public ActionResult Edit(BookView model, HttpPostedFileBase imageBook)
        {
            string str = "check";
            var bookBO = mapper.Map<BookBO>(model);
            byte[] imageData = null;
            if (imageBook != null)
            {
                using (var binaryReader = new BinaryReader(imageBook.InputStream))
                {
                    imageData = binaryReader.ReadBytes(imageBook.ContentLength);
                }
                bookBO.ImageData = imageData;
            }
            else
            {
                bookBO.ImageData = new byte[str.Length];
            }

            bookBO.Save();
            var books = DependencyResolver.Current.GetService<BookBO>().GetBooksList();

            return RedirectToActionPermanent("Index", "Book");
        }


        public ActionResult Delete(int id)
        {
            var book = DependencyResolver.Current.GetService<BookBO>().GetBooksListById(id);
            book.Delete(id);

            return RedirectToActionPermanent("Index", "Book");
        }
    }
}