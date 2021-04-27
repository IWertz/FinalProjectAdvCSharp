using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectGamesWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectGamesWebApp.Controllers
{
    public class ReviewController : Controller
    {
        //repo setup
        private IRepository<Game> game { get; set; }
        private IRepository<Review> review { get; set; }
        private IRepository<User> user { get; set; }
        public ReviewController(IRepository<Game> ctxGame, IRepository<Review> ctxReview, IRepository<User> ctxUser)
        {
            game = ctxGame;
            review = ctxReview;
            user = ctxUser;
        }

        //returns a list of reviews based on sort parameters
        [HttpGet]
        public IActionResult List(int id, string sortOrder)
        {
            Console.WriteLine("Unauthenticated Review List Request");
            ViewBag.UserSortParm = String.IsNullOrEmpty(sortOrder) ? "user_desc" : "";
            ViewBag.RatingSortParm = sortOrder == "rating_desc" ? "rating" : "rating_desc";
            ViewBag.SortOrder = sortOrder;

            CurrentGame.Current = game.Get(id);
            ViewBag.GameTitle = CurrentGame.Current.Title;
            ViewBag.GameId = CurrentGame.Current.GameId;

            IEnumerable<Review> reviews;
            switch (sortOrder)
            {
                case "user_desc":
                    reviews = review.List(new QueryOptions<Review> { Includes = "Game, User", Where = r => r.GameId == id, OrderBy = r => r.User.UserName, OrderByDirection = "dsc" });
                    break;
                case "rating":
                    reviews = review.List(new QueryOptions<Review> { Includes = "Game, User", Where = r => r.GameId == id, OrderBy = r => r.Rating });
                    break;
                case "rating_desc":
                    reviews = review.List(new QueryOptions<Review> { Includes = "Game, User", Where = r => r.GameId == id, OrderBy = r => r.Rating, OrderByDirection = "dsc" });
                    break;
                default:
                    reviews = review.List(new QueryOptions<Review> { Includes = "Game, User", Where = r => r.GameId == id, OrderBy = r => r.User.UserName });
                    break;
            }
            return View(reviews);
        }

        //views a single review
        [HttpGet]
        public IActionResult View(int id)
        {
            Console.WriteLine("Unauthenticated Review View Request");
            Review re = null;
            var reviews = review.List(new QueryOptions<Review> { Includes = "User, Game", Where = r => r.ReviewId == id });
            foreach (Review r in reviews)
            {
                re = r;
            }

            return View(re);
        }
    }
}