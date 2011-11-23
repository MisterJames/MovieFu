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
    public class RatingsController : Controller
    {
        private MovieFuContext context = new MovieFuContext();

        //
        // GET: /Ratings/

        public ViewResult Index()
        {
            return View(context.Ratings.Include(rating => rating.Movie).ToList());
        }

        //
        // GET: /Ratings/Details/5

        public ViewResult Details(int id)
        {
            Rating rating = context.Ratings.Single(x => x.RatingID == id);
            return View(rating);
        }

        //
        // GET: /Ratings/Create
        [Authorize]
        [UserNameFilter]
        public ActionResult Create(string UserName)
        {
            ViewBag.PossibleMovies = context.Movies;
            Rating rating = new Rating { UserName = UserName };
            return View(rating);
        } 

        //
        // POST: /Ratings/Create

        [HttpPost]
        [UserNameFilter]
        public ActionResult Create(string UserName, Rating rating)
        {
            
            ModelState.Clear();
            rating.UserName = UserName
                ;
            TryUpdateModel(rating);

            if (ModelState.IsValid)
            {
                context.Ratings.Add(rating);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleMovies = context.Movies;
            return View(rating);
        }
        
        //
        // GET: /Ratings/Edit/5

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            Rating rating = context.Ratings.Single(x => x.RatingID == id);
            ViewBag.PossibleMovies = context.Movies;
            return View(rating);
        }

        //
        // POST: /Ratings/Edit/5

        [HttpPost]
        public ActionResult Edit(Rating rating)
        {
            if (ModelState.IsValid)
            {
                context.Entry(rating).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleMovies = context.Movies;
            return View(rating);
        }

        //
        // GET: /Ratings/Delete/5

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            Rating rating = context.Ratings.Single(x => x.RatingID == id);
            return View(rating);
        }

        //
        // POST: /Ratings/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = context.Ratings.Single(x => x.RatingID == id);
            context.Ratings.Remove(rating);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}