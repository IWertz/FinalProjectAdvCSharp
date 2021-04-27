using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectGamesWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectGamesWebApp.Controllers
{
    public class UserController : Controller
    {
        //repo setup
        private IRepository<Game> game { get; set; }
        private IRepository<Review> review { get; set; }
        private IRepository<User> user { get; set; }
        public UserController(IRepository<Game> ctxGame, IRepository<Review> ctxReview, IRepository<User> ctxUser)
        {
            game = ctxGame;
            review = ctxReview;
            user = ctxUser;
        }

        //takes in user as a new user, and if there are no conflicts with the user model or database, it allows a 
        //new user to be created
        [HttpPost]
        public IActionResult Add(User us)
        {
            Console.WriteLine("Unauthenticated User Add Response");
            foreach (User u in user.List(new QueryOptions<User> { }))
            {
                if (us.UserName == u.UserName)
                {
                    ViewBag.CompletionMessage = "";
                    ViewBag.errorMessage = "";
                    ViewBag.FailCompletionMessage = "That username already exists. Try logging in instead.";
                    return View("~/Views/Home/Authentication.cshtml");
                }
            }
            if (ModelState.IsValid)
            {
                if (us.UserId == 0) user.Insert(us);
                else user.Update(us);
                user.Save();
                ViewBag.CompletionMessage = "Account successfully created!";
                ViewBag.errorMessage = "";
                ViewBag.FailCompletionMessage = "";
                return View("~/Views/Home/Authentication.cshtml");
            }
            else
            {
                Console.WriteLine("Unauthenticated User Add Response Failed");
                ViewBag.CompletionMessage = "";
                ViewBag.errorMessage = "";
                ViewBag.FailCompletionMessage = "Could not create account. Please try again.";
                return View("~/Views/Home/Authentication.cshtml");
            }
        }
    }
}