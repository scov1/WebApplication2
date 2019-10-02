﻿using AutoMapper;
using BL.BO;
using System;
using System.Collections.Generic;
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
            ViewBag.Books = bookList.Select(m => mapper.Map<BookView>(m)).ToList();
            ViewBag.Authors = authorList.Select(m => mapper.Map<AuthorView>(m)).ToList();
            return View();
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            var bookBO = DependencyResolver.Current.GetService<BookBO>();
            var authors = DependencyResolver.Current.GetService<AuthorBO>();
            var model = mapper.Map<BookView>(bookBO);

            if (id != null)
            {
                var bookBOList = bookBO.GetBooksListById(id);
                model = mapper.Map<BookView>(bookBOList);
                ViewBag.Message = "Edit";
            }
            else ViewBag.Message = "Create";

            ViewBag.Authors = new SelectList(authors.GetAuthorsList().Select(m => mapper.Map<AuthorView>(m)).ToList(), "Id", "LastName");

            return View(model);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(BookView model)
        {
            var bookBO = mapper.Map<BookBO>(model);
            //if (ModelState.IsValid)
            //{
            bookBO.Save();
            return RedirectToActionPermanent("Index", "Book");

            //// массив для хранения бинарных данных файла
            //byte[] imageData;
            //using (System.IO.FileStream fs = new System.IO.FileStream(filename, FileMode.Open))
            //{
            //    imageData = new byte[fs.Length];
            //    fs.Read(imageData, 0, imageData.Length);
            //}

            //}
            //else return View(model);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            var book = DependencyResolver.Current.GetService<BookBO>().GetBooksListById(id);
            book.Delete(id);

            return RedirectToActionPermanent("Index", "Book");
        }
    }
}