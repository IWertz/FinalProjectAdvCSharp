using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectGamesWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectGamesWebApp.Areas.AdminUser.Controllers
{
    [Area("AdminUser")]
    public class ReviewController : Controller
    {
        private GameContext context { get; set; }

        public ReviewController(GameContext ctx)
        {
            context = ctx;
        }

        [Route("AdminUser/Review/List")]
        [HttpGet]
        public IActionResult List(int id, string sortOrder)
        {
            Console.WriteLine("Admin Review List Request");
            ViewBag.UserSortParm = String.IsNullOrEmpty(sortOrder) ? "user_desc" : "";
            ViewBag.RatingSortParm = sortOrder == "rating_desc" ? "rating" : "rating_desc";
            ViewBag.SortOrder = sortOrder;

            CurrentGame.Current = context.Games.Find(id);
            ViewBag.GameTitle = CurrentGame.Current.Title;
            ViewBag.GameId = CurrentGame.Current.GameId;

            List<Review> reviews;
            switch(sortOrder)
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

        [Route("AdminUser/Review/UserList")]
        [HttpGet]
        public IActionResult UserList()
        {
            Console.WriteLine("Admin Review UserList Request");
            ViewBag.UserName = CurrentUser.Current.UserName;
            var reviews = context.Reviews.Include(m => m.User).Include(m => m.Game).Where(m => m.UserId == CurrentUser.Current.UserId).OrderBy(m => m.ReviewId).ToList();
            return View(reviews);
        }

        [Route("AdminUser/Review/View")]
        [HttpGet]
        public IActionResult View(int id)
        {
            Console.WriteLine("Admin Review View Request");
            Review review = null;
            var reviews = context.Reviews.Include(m => m.User).Include(m => m.Game).Where(m => m.ReviewId == id).ToList(); 
            foreach (Review r in reviews)
            {
                review = r;
            }

            return View(review);
        }

        [Route("AdminUser/Review/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            Console.WriteLine("Admin Review Add Request");
            return View("Edit", new Review());
        }

        [Route("AdminUser/Review/Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Console.WriteLine("Admin Review Edit Request");
            Review review = null;
            var reviews = context.Reviews.Include(m => m.User).Include(m => m.Game).Where(m => m.ReviewId == id).ToList();
            foreach (Review r in reviews)
            {
                review = r;
            }

            return View(review);
        }

        [Route("AdminUser/Review/Edit")]
        [HttpPost]
        public IActionResult Edit(Review review)
        {
            Console.WriteLine("Admin Review Edit Response");
            if (ModelState.IsValid)
            {
                if (review.ReviewId == 0)
                {
                    review.User = context.Users.Find(CurrentUser.Current.UserId);
                    review.Game = context.Games.Find(CurrentGame.Current.GameId);
                    context.Reviews.Add(review);
                } else
                {
                    review.User = context.Users.Find(review.UserId);
                    review.Game = context.Games.Find(review.GameId);
                    context.Reviews.Update(review);
                }
                context.SaveChanges();
                ViewBag.FailEdit = "";
                return View("View", review);
            }
            else
            {
                Console.WriteLine("Admin Review Edit Response Failed");
                ViewBag.FailEdit = "Could not edit review. Please try again.";
                return View(review);
            }
        }

        [Route("AdminUser/Review/Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("Admin Review Delete Request");
            var review = context.Reviews.Find(id);
            return View(review);
        }

        [Route("AdminUser/Review/Delete")]
        [HttpPost]
        public IActionResult Delete(Review review)
        {
            Console.WriteLine("Admin Review Delete Response");
            ViewBag.GameId = CurrentGame.Current.GameId;
            context.Reviews.Remove(review);
            context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}