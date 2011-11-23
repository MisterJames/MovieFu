using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieFu.Models;
using MovieFu.ActionFilters;

namespace MovieFu.Controllers
{   
    public class MoviesController : Controller
    {
        private MovieFuContext context = new MovieFuContext();

        //
        // GET: /Movies/

        public ViewResult Index()
        {
            return View(context.Movies.Include(movie => movie.Ratings).ToList());
        }

        //
        // GET: /Movies/Details/5
        public ViewResult Details(string userName, int id)
        {
            Movie movie = context.Movies.Single(x => x.MovieID == id);
            return View(movie);
        }

        [Authorize]
        [UserNameFilter]
        public PartialViewResult RatingAdd(string UserName, int movieId)
        {
            string result = UserName.Length == 0 ? "PleaseLoginToRate" : "RatingAdd";
            ViewBag.MovieId = movieId;
            Rating rating = new Rating { MovieID = movieId };

            return PartialView(result, rating);
        }


        //
        // GET: /Movies/Create

        [Authorize(Roles="admin")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Movies/Create

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                context.Movies.Add(movie);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(movie);
        }
        
        //
        // GET: /Movies/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            Movie movie = context.Movies.Single(x => x.MovieID == id);
            return View(movie);
        }

        //
        // POST: /Movies/Edit/5

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                context.Entry(movie).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        //
        // GET: /Movies/Delete/5

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            Movie movie = context.Movies.Single(x => x.MovieID == id);
            return View(movie);
        }

        //
        // POST: /Movies/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = context.Movies.Single(x => x.MovieID == id);
            context.Movies.Remove(movie);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}