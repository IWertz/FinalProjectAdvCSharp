using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinalProjectGamesWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectGamesWebApp.Areas.AdminUser.Controllers
{
    [Area("AdminUser")]
    public class HomeController : Controller
    {
        //Update() refreshes the database every time a user returns to the main menu in order to see if any new 
        //reviews or changes in rating occur
        private void Update()
        {
            var games = game.List(new QueryOptions<Game> { });
            foreach (Game g in games)
            {
                double totalRating = 0;
                int totalReviews = 0;
                var reviews = review.List(new QueryOptions<Review> { Includes = "User, Game", Where = r => r.GameId == g.GameId });
                foreach (Review r in reviews)
                {
                    totalRating += (double)r.Rating;
                    totalReviews += 1;
                }
                if (totalReviews == 0)
                {
                    g.Rating = 0;
                    g.TotalReviews = 0;
                }
                else
                {
                    totalRating = totalRating / totalReviews;
                    g.Rating = totalRating;
                    g.TotalReviews = totalReviews;
                }
                game.Update(g);
            }
            game.Save();
        }

        //repository setup
        private IRepository<Game> game { get; set; }
        private IRepository<Review> review { get; set; }
        private IRepository<User> user { get; set; }
        public HomeController(IRepository<Game> ctxGame, IRepository<Review> ctxReview, IRepository<User> ctxUser)
        {
            game = ctxGame;
            review = ctxReview;
            user = ctxUser;
        }

        //takes in sorting parameters and returns a sorted list of games
        [Route("AdminUser")]
        public IActionResult Index(string sortOrder)
        {
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.RatingSortParm = sortOrder == "rating_desc" ? "rating" : "rating_desc";
            ViewBag.ReviewSortParm = sortOrder == "review_desc" ? "review" : "review_desc";
            ViewBag.SortOrder = sortOrder;
            Update();
            if (CurrentUser.Current != null)
            {
                ViewBag.UserName = CurrentUser.Current.UserName;
            }
            IEnumerable<Game> games;
            switch (sortOrder)
            {
                case "title_desc":
                    games = game.List(new QueryOptions<Game> { OrderBy = g => g.Title, OrderByDirection = "dsc" });
                    break;
                case "rating":
                    games = game.List(new QueryOptions<Game> { OrderBy = g => g.Rating });
                    break;
                case "rating_desc":
                    games = game.List(new QueryOptions<Game> { OrderBy = g => g.Rating, OrderByDirection = "dsc" });
                    break;
                case "review":
                    games = game.List(new QueryOptions<Game> { OrderBy = g => g.TotalReviews });
                    break;
                case "review_desc":
                    games = game.List(new QueryOptions<Game> { OrderBy = g => g.TotalReviews, OrderByDirection = "dsc" });
                    break;
                default:
                    games = game.List(new QueryOptions<Game> { OrderBy = g => g.Title });
                    break;
            }
            return View(games);
        }
    }
}
