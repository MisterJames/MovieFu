using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieFu.Models;

namespace MovieFu.Controllers
{
    public class SearchController : Controller
    {
        private MovieFuContext context = new MovieFuContext();
        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult FindMovies(string searchText)
        {
            var movies = context.Movies.Where(m=>m.Title.Contains(searchText));

            return PartialView(movies);
        }

    }
}
