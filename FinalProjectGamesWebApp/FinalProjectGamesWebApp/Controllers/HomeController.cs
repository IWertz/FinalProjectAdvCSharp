using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinalProjectGamesWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectGamesWebApp.Controllers
{
    public class HomeController : Controller
    {
        private void Update()
        {
            var games = context.Games.ToList();
            foreach(Game g in games)
            {
                double totalRating = 0;
                int totalReviews = 0;
                var reviews = context.Reviews.Include(m => m.User).Include(m => m.Game).Where(m => m.GameId == g.GameId).ToList();
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
                context.Games.Update(g);
            }
            context.SaveChanges();
        }
        private GameContext context { get; set; }

        public HomeController(GameContext ctx)
        {
            context = ctx;
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
            List<Game> games;
            switch (sortOrder)
            {
                case "title_desc":
                    games = context.Games.OrderByDescending(m => m.Title).ToList();
                    break;
                case "rating":
                    games = context.Games.OrderBy(m => m.Rating).ToList();
                    break;
                case "rating_desc":
                    games = context.Games.OrderByDescending(m => m.Rating).ToList();
                    break;
                case "review":
                    games = context.Games.OrderBy(m => m.TotalReviews).ToList();
                    break;
                case "review_desc":
                    games = context.Games.OrderByDescending(m => m.TotalReviews).ToList();
                    break;
                default:
                    games = context.Games.OrderBy(m => m.Title).ToList();
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
        public IActionResult Authentication(User user)
        {
            Console.WriteLine("Unauthenticated Home Authentication Response");
            List<User> users = context.Users.ToList();
            foreach (User u in users)
            {
                if (user.UserName == u.UserName && user.Password == u.Password)
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
