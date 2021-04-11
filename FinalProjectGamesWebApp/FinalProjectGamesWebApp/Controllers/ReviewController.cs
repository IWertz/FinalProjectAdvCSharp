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
        private GameContext context { get; set; }

        public ReviewController(GameContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult List(int id, string sortOrder)
        {
            Console.WriteLine("Unauthenticated Review List Request");
            ViewBag.UserSortParm = String.IsNullOrEmpty(sortOrder) ? "user_desc" : "";
            ViewBag.RatingSortParm = sortOrder == "rating_desc" ? "rating" : "rating_desc";
            ViewBag.SortOrder = sortOrder;

            CurrentGame.Current = context.Games.Find(id);
            ViewBag.GameTitle = CurrentGame.Current.Title;
            ViewBag.GameId = CurrentGame.Current.GameId;

            List<Review> reviews;
            switch (sortOrder)
            {
                case "user_desc":
                    reviews = context.Reviews.Include(m => m.User).Include(m => m.Game).Where(m => m.GameId == id).OrderByDescending(m => m.User.UserName).ToList();
                    break;
                case "rating":
                    reviews = context.Reviews.Include(m => m.User).Include(m => m.Game).Where(m => m.GameId == id).OrderBy(m => m.Rating).ToList();
                    break;
                case "rating_desc":
                    reviews = context.Reviews.Include(m => m.User).Include(m => m.Game).Where(m => m.GameId == id).OrderByDescending(m => m.Rating).ToList();
                    break;
                default:
                    reviews = context.Reviews.Include(m => m.User).Include(m => m.Game).Where(m => m.GameId == id).OrderBy(m => m.User.UserName).ToList();
                    break;
            }
            return View(reviews);
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            Console.WriteLine("Unauthenticated Review View Request");
            Review review = null;
            var reviews = context.Reviews.Include(m => m.User).Include(mbox => mbox.Game).Where(m => m.ReviewId == id).ToList(); 
            foreach (Review r in reviews)
            {
                review = r;
            }

            return View(review);
        }
    }
}