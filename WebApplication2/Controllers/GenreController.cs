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
    public class GenreController : Controller
    {

        protected IMapper mapper;

        public GenreController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        // GET: Genre
        public ActionResult Index()
        {
            var genreBO = DependencyResolver.Current.GetService<GenreBO>();
            var genreList = genreBO.GetGenreList();
            ViewBag.Genres = genreList.Select(m => mapper.Map<GenreView>(m)).ToList();

         
            return View();
        }

        public ActionResult Edit(int? id)
        {
            var genreBO = DependencyResolver.Current.GetService<GenreBO>();
            var model = mapper.Map<GenreView>(genreBO);
            if (id != null)
            {
                var genreList = genreBO.GetGenresListById(id);
                model = mapper.Map<GenreView>(genreList);
                ViewBag.Message = "Edit";
            }
            else ViewBag.Message = "Create";

            return View(model);
        }

[HttpPost]
        public ActionResult Edit(GenreView model)
        {
            var genreBO = mapper.Map<GenreBO>(model);
            genreBO.Save();

            return RedirectToActionPermanent("Index", "Genre");
        }


        //    // GET: Genre/Delete/5
            public ActionResult Delete(int id)
        {
            var genre = DependencyResolver.Current.GetService<GenreBO>().GetGenresListById(id);
            genre.Delete(id);

            return RedirectToActionPermanent("Index", "Genre");
        }
    }
}