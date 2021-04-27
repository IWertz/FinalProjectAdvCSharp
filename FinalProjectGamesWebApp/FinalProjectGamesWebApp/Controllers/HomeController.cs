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
        //Update() refreshes the database every time a user returns to the main menu in order to see if any new 
        //reviews or changes in rating occur
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

        //Repository setup
        private IRepository<Game> game { get; set; }
        private IRepository<Review> review { get; set; }
        private IRepository<User> user { get; set; }
        public HomeController(IRepository<Game> ctxGame, IRepository<Review> ctxReview, IRepository<User> ctxUser)
        {
            game = ctxGame;
            review = ctxReview;
            user = ctxUser;
        }

        //returns a list of games with information about reviews and ratings attached to the home page
        [Route("/")]
        public IActionResult Index(string sortOrder)
        {
            //takes in sorting parameters
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.RatingSortParm = sortOrder == "rating_desc" ? "rating" : "rating_desc";
            ViewBag.ReviewSortParm = sortOrder == "review_desc" ? "review" : "review_desc";
            ViewBag.SortOrder = sortOrder;

            //calls update
            Update();

            //checks if anyone is logged in, if not, allow them to use the sign in link
            if (CurrentUser.Current != null)
            {
                ViewBag.UserName = CurrentUser.Current.UserName;
            }

            //sets up a list of games, then takes sort parameters to query games from the game repo. 
            //returns sorted list to home page
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

        //returns authentication page with temp data in case authentication fails
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

        //takes user authentication and determines if user is valid or not. returns areas based on user authentication
        //level. if user fails authentication they are redirected to the auth page with an error message. current
        //user is stored in a class to be called upon in other methods with ease, until a signout event where current
        //user gets wiped.
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

        //when signout is called, it removes the information about the current user from the client. then it returns
        //to the home page of the main area
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
