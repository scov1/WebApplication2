using AutoMapper;
using BL.BO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication2;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class AuthorController : Controller
    {
        //GET: Author
        //public ActionResult Index()
        //{
        //    List<Authors> authors;

        //    using (Model1 db = new Model1())
        //    {
        //        authors = db.Authors.ToList();
        //    }

        //    return View(authors);
        //}
        //protected IMapper mapper;

        //public AuthorController(IMapper mapper)
        //{
        //    this.mapper = mapper;
        //}

        //public ActionResult Index()
        //{
        //    var authorBO = DependencyResolver.Current.GetService<AuthorBO>();
        //    var authorList = authorBO.GetAuthorsList();
        //    ViewBag.Authors = authorList.Select(m => mapper.Map<AuthorViewModel>(m)).ToList();

        //    List<AuthorViewModel> authorsTop = new List<AuthorViewModel>();
        //    BookBO books = DependencyResolver.Current.GetService<BookBO>();
        //    var expensiveBooks = books.GetBooksList().Select(item => mapper.Map<BookViewModel>(item))
        //                        .OrderByDescending(b => b.Price).ToList();
        //    //expensiveBooks.ForEach(x => authorsTop.Add(db.Authors.Where(a => a.Id == x).FirstOrDefault()));
        //    foreach (var item in expensiveBooks)
        //    {
        //        authorsTop.Add(authorList.Select(a => mapper.Map<AuthorViewModel>(a))
        //            .Where(a => a.Id == item.AuthorId).FirstOrDefault());
        //    }
        //    ViewBag.Authors = authorList.Select(item => mapper.Map<AuthorViewModel>(item)).ToList();
        //    ViewBag.AuthorsTop = authorsTop.Distinct().Take(5);

        //    return View();
        //}

        //public ActionResult Edit(int? id)
        //{
        //    var authorBO = DependencyResolver.Current.GetService<AuthorBO>();
        //    var model = mapper.Map<AuthorViewModel>(authorBO);

        //    if (id != null)
        //    {
        //        var authorBOList = authorBO.GetAuthorsListById(id);
        //        model = mapper.Map<AuthorViewModel>(authorBOList);
        //        ViewBag.Message = "Edit";
        //    }
        //    else ViewBag.Message = "Create";

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Edit(AuthorViewModel model)
        //{
        //    var authorBO = mapper.Map<AuthorBO>(model);
        //    //if (ModelState.IsValid)
        //    //{
        //    authorBO.Save();
        //    return RedirectToActionPermanent("Index", "Author");
        //    //}
        //    //else return View(model);
        //}

        //// POST: Author/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    var author = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsListById(id);
        //    author.Delete(id);

        //    return RedirectToActionPermanent("Index", "Author");
        //}

        //public ActionResult _MyPartialView()
        //{
        //    var books = DependencyResolver.Current.GetService<BookBO>();
        //    var authors = DependencyResolver.Current.GetService<AuthorBO>();
        //    var expensiveBooks = books.GetBooksList().Select(item => mapper.Map<BookViewModel>(item))
        //                        .OrderByDescending(b => b.Price).ToList();
        //    ViewBag.ExpBooks = expensiveBooks;
        //    ViewBag.Authors = authors.GetAuthorsList();

        //    return PartialView();
        //}


        //[HttpGet]
        //public ActionResult CreateOrEdit(int? id)
        //{
        //    Authors author = new Authors();

        //    if (id != null)
        //        using (Model1 db = new Model1())
        //        {
        //            author = db.Authors.Where(x => x.Id == id).FirstOrDefault();
        //        }
        //    return View(author);
        //}


        //[HttpPost]
        //public ActionResult CreateOrEdit(Authors author)
        //{

        //    using (Model1 db = new Model1())
        //    {
        //        if (author.Id != 0)
        //        {
        //            var oldAuthor = db.Authors.Where(x => x.Id == author.Id).FirstOrDefault();
        //            oldAuthor.FirstName = author.FirstName;
        //            oldAuthor.LastName = author.LastName;
        //            db.SaveChanges();
        //        }
        //        else
        //        {
        //            db.Authors.Add(author);
        //            db.SaveChanges();
        //        }

        //    }

        //    return RedirectToActionPermanent("Index", "Author");
        //}

        //public ActionResult Delete(int id)
        //{
            //using (Model1 db = new Model1())
        //    {
        //        var author = db.Authors.Where(a => a.Id == id).FirstOrDefault();
        //        db.Authors.Remove(author);
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Index", "Author");
        //}

    protected IMapper mapper;
    public AuthorController(IMapper mapper)
        {
                this.mapper = mapper;
        }

    public ActionResult Index()
        {
            var authorBO = DependencyResolver.Current.GetService<AuthorBO>();
            var authorList = authorBO.GetAuthorsList();
            ViewBag.Authors = authorList.Select(m => mapper.Map<AuthorView>(m)).ToList();

            List<AuthorView> topAuthor = new List<AuthorView>();
            BookBO books = DependencyResolver.Current.GetService<BookBO>();
            var expensive = books.GetBooksList().Select(item => mapper.Map<BookView>(item)).OrderByDescending(b => b.Price).ToList();

            foreach (var item in expensive)
            {
                topAuthor.Add(authorList.Select(a => mapper.Map<AuthorView>(a))
                    .Where(a => a.Id == item.AuthorId).FirstOrDefault());
            }
         

            ViewBag.Authors = authorList.Select(item => mapper.Map<AuthorView>(item)).ToList();
            ViewBag.AuthorsTop = topAuthor.Distinct().Take(5);

            if (Request.IsAjaxRequest())
            {
                return PartialView("Partial/MyPartialView", ViewBag.Authors);
            }

            return View();
        }

 
        public ActionResult Edit(int? id)
        {
            var authorBO = DependencyResolver.Current.GetService<AuthorBO>();
            var model = mapper.Map<AuthorView>(authorBO);

            if (id != null)
            {
                var authorBOList = authorBO.GetAuthorsListById(id);
                model = mapper.Map<AuthorView>(authorBOList);
                ViewBag.Message = "Edit";
            }
            else ViewBag.Message = "Create";

            return View(model);
        }

      
        [HttpPost]
        public ActionResult Edit(AuthorView model)
        {
            var authorBO = mapper.Map<AuthorBO>(model);
            if (ModelState.IsValid)
            {
                authorBO.Save();
                return RedirectToActionPermanent("Index", "Author");
            }
            else return View(model);
        }


        public ActionResult Delete(int id)
        {
            var author = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsListById(id);
            author.Delete(id);

            return RedirectToActionPermanent("Index", "Author");
        }

        public ActionResult _MyPartialView()
        {
            var books = DependencyResolver.Current.GetService<BookBO>();
            var authors = DependencyResolver.Current.GetService<AuthorBO>();
            var expensiveBooks = books.GetBooksList().Select(item => mapper.Map<BookView>(item)).OrderByDescending(b => b.Price).ToList();
            ViewBag.ExpBooks = expensiveBooks;
            ViewBag.Authors = authors.GetAuthorsList();

            return PartialView();
        }
    }
}