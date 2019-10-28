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
            //var genres = DependencyResolver.Current.GetService<GenreBO>();

            if (id != null)
            {
                var bookBOList = bookBO.GetBooksListById(id);
                model = mapper.Map<BookView>(bookBOList);
            }

            //ViewBag.Authors = new SelectList(authors.GetAuthorsList().Select(x => mapper.Map<AuthorView>(x)).ToList(), "Id", "LastName");
            //ViewBag.Genres = new SelectList(genres.GetGenreList().Select(x => mapper.Map<GenreView>(x)).ToList(), "Id", "Name");

            if (Request.IsAjaxRequest())
            {
                return PartialView("Partial/EditPartialView", model);
            }
            return View(model);
        }

    
        [HttpPost]
        public ActionResult Edit(BookView model, HttpPostedFileBase imageBook, int genre, int author)
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
            bookBO.GenreId = genre;
            bookBO.AuthorId = author;
            bookBO.Save();
            var books = DependencyResolver.Current.GetService<BookBO>().GetBooksList();
            var authorList = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsList();
            var genreList = DependencyResolver.Current.GetService<GenreBO>().GetGenreList();
            ViewBag.Authors = authorList.Select(m => mapper.Map<AuthorView>(m)).ToList();
            ViewBag.Genres = genreList.Select(m => mapper.Map<GenreView>(m)).ToList();

            //return RedirectToActionPermanent("Index", "Book");
            return PartialView("Partial/BookPartialView", books.Select(m => mapper.Map<BookView>(m)).ToList());
        }


        public ActionResult Delete(int id)
        {
            var book = DependencyResolver.Current.GetService<BookBO>().GetBooksListById(id);
            book.Delete(id);

            return RedirectToActionPermanent("Index", "Book");
        }

        [HttpGet]
        public ActionResult GenreDropDown()
        {
            var genreBO = DependencyResolver.Current.GetService<GenreBO>().GetGenreList();
            var genreList = genreBO.Select(m => mapper.Map<GenreView>(m)).ToList();
            return Json(genreList.Select(g => new { g.Id, g.Name }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AuthorDropDown()
        {
            var authorBO = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsList();
            var authorList = authorBO.Select(m => mapper.Map<AuthorView>(m)).ToList();
            return Json(authorList.Select(g => new { g.Id, g.LastName }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}