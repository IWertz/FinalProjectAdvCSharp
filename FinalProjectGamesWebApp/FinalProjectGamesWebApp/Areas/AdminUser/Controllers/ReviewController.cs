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
        //repository setup
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
        [Route("AdminUser/Review/List")]
        [HttpGet]
        public IActionResult List(int id, string sortOrder)
        {
            Console.WriteLine("Admin Review List Request");
            ViewBag.UserSortParm = String.IsNullOrEmpty(sortOrder) ? "user_desc" : "";
            ViewBag.RatingSortParm = sortOrder == "rating_desc" ? "rating" : "rating_desc";
            ViewBag.SortOrder = sortOrder;

            CurrentGame.Current = game.Get(id);
            ViewBag.GameTitle = CurrentGame.Current.Title;
            ViewBag.GameId = CurrentGame.Current.GameId;

            IEnumerable<Review> reviews;
            switch(sortOrder)
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

        //allows users to check all the reviews that they have created, based on creation date
        [Route("AdminUser/Review/UserList")]
        [HttpGet]
        public IActionResult UserList()
        {
            Console.WriteLine("Admin Review UserList Request");
            ViewBag.UserName = CurrentUser.Current.UserName;
            var reviews = review.List(new QueryOptions<Review> { Includes = "User, Game", Where = r => r.UserId == CurrentUser.Current.UserId, OrderBy = r => r.ReviewId });
            return View(reviews);
        }

        //views a single review
        [Route("AdminUser/Review/View")]
        [HttpGet]
        public IActionResult View(int id)
        {
            Console.WriteLine("Admin Review View Request");
            Review re = null;
            var reviews = review.List(new QueryOptions<Review> { Includes = "User, Game", Where = r => r.ReviewId == id });
            foreach (Review r in reviews)
            {
                re = r;
            }

            return View(re);
        }

        //returns a new review to the edit method, allows users to create a new review
        [Route("AdminUser/Review/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            Console.WriteLine("Admin Review Add Request");
            return View("Edit", new Review());
        }

        //returns new or current review to edit to the edit page
        [Route("AdminUser/Review/Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Console.WriteLine("Admin Review Edit Request");
            Review re = null;
            var reviews = review.List(new QueryOptions<Review> { Includes = "User, Game", Where = r => r.ReviewId == id });
            foreach (Review r in reviews)
            {
                re = r;
            }

            return View(re);
        }

        //allows users to add or edit reviews, saves to the database if not a failed edit, and returns finished review
        [Route("AdminUser/Review/Edit")]
        [HttpPost]
        public IActionResult Edit(Review r)
        {
            Console.WriteLine("Admin Review Edit Response");
            if (ModelState.IsValid)
            {
                if (r.ReviewId == 0)
                {
                    r.User = user.Get(CurrentUser.Current.UserId);
                    r.Game = game.Get(CurrentGame.Current.GameId);
                    review.Insert(r);
                }
                else
                {
                    r.User = user.Get(r.UserId);
                    r.Game = game.Get(r.GameId);
                    review.Update(r);
                }
                review.Save();
                ViewBag.FailEdit = "";
                return View("View", r);
            }
            else
            {
                Console.WriteLine("Admin Review Edit Response Failed");
                ViewBag.FailEdit = "Could not edit review. Please try again.";
                return View(r);
            }
        }

        //allows users to delete their reviews, returns a review to the delete confirmation page
        [Route("AdminUser/Review/Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("Admin Review Delete Request");
            var r = review.Get(id);
            return View(r);
        }

        //carries out the delete after confirmation is given
        [Route("AdminUser/Review/Delete")]
        [HttpPost]
        public IActionResult Delete(Review r)
        {
            Console.WriteLine("Admin Review Delete Response");
            ViewBag.GameId = CurrentGame.Current.GameId;
            review.Delete(r);
            review.Save();
            return RedirectToAction("List");
        }
    }
}