using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectGamesWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectGamesWebApp.Areas.NormalUser.Controllers
{
    [Area("NormalUser")]
    public class UserController : Controller
    {
        //repository setup
        private IRepository<Game> game { get; set; }
        private IRepository<Review> review { get; set; }
        private IRepository<User> user { get; set; }
        public UserController(IRepository<Game> ctxGame, IRepository<Review> ctxReview, IRepository<User> ctxUser)
        {
            game = ctxGame;
            review = ctxReview;
            user = ctxUser;
        }

        //allows users to view the information about their account
        [Route("NormalUser/User/Account")]
        public IActionResult Account()
        {
            return View(CurrentUser.Current);
        }

        //allows users to edit the information about their account
        [Route("NormalUser/User/Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Console.WriteLine("User User Edit Request");
            var us = user.Get(id);
            return View(us);
        }

        //updates the database, if authentication level changes, they move to their proper place
        [Route("NormalUser/User/Edit")]
        [HttpPost]
        public IActionResult Edit(User us)
        {
            Console.WriteLine("User User Edit Response");
            if (ModelState.IsValid)
            {
                user.Update(us);
                user.Save();
                CurrentUser.Current = us;
                ViewBag.FailEdit = "";
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
            else
            {
                Console.WriteLine("User User Edit Response Failed");
                ViewBag.FailEdit = "Could not edit account. Please try again.";
                return View();
            }
        }

        //allows users to delete their account
        [Route("NormalUser/User/Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("User User Delete Request");
            var us = user.Get(id);
            return View(us);
        }

        //saves changes to database, deletes cascading reviews made by the user, and wipes user computer of user info
        [Route("NormalUser/User/Delete")]
        [HttpPost]
        public IActionResult Delete(User us)
        {
            Console.WriteLine("User User Edit Response");
            var reviews = review.List(new QueryOptions<Review> { Where = r => r.UserId == us.UserId });
            foreach(Review r in reviews)
            {
                review.Delete(r);
            }
            user.Delete(us);
            user.Save();
            CurrentUser.Current = null;
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}