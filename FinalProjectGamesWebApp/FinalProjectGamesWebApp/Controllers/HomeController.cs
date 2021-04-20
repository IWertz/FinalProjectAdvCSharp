using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinalProjectGamesWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FinalProjectGamesWebApp.Controllers
{
    public class HomeController : Controller
    {
        private void Update()
        {
            var games = game.List(new QueryOptions<Game> { });
            foreach(Game g in games)
            {
                double totalRating = 0;
                int totalReviews = 0;
                var reviews = review.List(new QueryOptions<Review> { Includes = "User, Game", Where = r => r.GameId == g.GameId });
                foreach(Review r in reviews)
                {
                    totalRating += (double) r.Rating;
                    totalReviews += 1;
                }
                if (totalReviews == 0)
                {
                    g.Rating = 0;
                    g.TotalReviews = 0;
                } else
                {
                    totalRating = totalRating / totalReviews;
                    g.Rating = totalRating;
                    g.TotalReviews = totalReviews;
                }
                game.Update(g);
            }
            game.Save();
        }
        private IRepository<Game> game { get; set; }
        private IRepository<Review> review { get; set; }
        private IRepository<User> user { get; set; }
        public HomeController(IRepository<Game> ctxGame, IRepository<Review> ctxReview, IRepository<User> ctxUser)
        {
            game = ctxGame;
            review = ctxReview;
            user = ctxUser;
        }

        [Route("/")]
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
                    games = game.List(new QueryOptions<Game> { OrderBy = g => g.Rating});
                    break;
                case "rating_desc":
                    games = game.List(new QueryOptions<Game> { OrderBy = g => g.Rating, OrderByDirection = "dsc" });
                    break;
                case "review":
                    games = game.List(new QueryOptions<Game> { OrderBy = g => g.TotalReviews});
                    break;
                case "review_desc":
                    games = game.List(new QueryOptions<Game> { OrderBy = g => g.TotalReviews, OrderByDirection = "dsc" });
                    break;
                default:
                    games = game.List(new QueryOptions<Game> { OrderBy = g => g.Title});
                    break;
            }
            return View(games);
        }

        [Route("Authentication")]
        [HttpGet]
        public IActionResult Authentication()
        {
            Console.WriteLine("Unauthenticated Home Authentication Request");
            if (TempData["errorMessage"] != null)
            {
                ViewBag.errorMessage = TempData["errorMessage"].ToString();
            }
            return View(new User());
        }

        [Route("Authentication")]
        [HttpPost]
        public IActionResult Authentication(User us)
        {
            Console.WriteLine("Unauthenticated Home Authentication Response");
            IEnumerable<User> users = user.List(new QueryOptions<User> {});
            foreach (User u in users)
            {
                if (us.UserName == u.UserName && us.Password == u.Password)
                {
                    CurrentUser.Current = u;
                    ViewBag.errorMessage = "";
                }
            }
            if (CurrentUser.Current != null)
            {
                switch (CurrentUser.Current.AuthLevel)
                {
                    case 1:
                        return RedirectToAction("Index", "Home", new { area = "NormalUser" });
                    case 2:
                        return RedirectToAction("Index", "Home", new { area = "AdminUser" });
                    default:
                        return RedirectToAction("Index");
                }
            }
            TempData["errorMessage"] = "User not found.";
            return RedirectToAction("Authentication");
        }

        [Route("SignOut")]
        [HttpGet]
        public IActionResult SignOut()
        {
            Console.WriteLine("Unauthenticated Home SignOut Request");
            CurrentUser.Current = null;
            CurrentGame.Current = null;
            return RedirectToAction("Index");
        }
    }
}
