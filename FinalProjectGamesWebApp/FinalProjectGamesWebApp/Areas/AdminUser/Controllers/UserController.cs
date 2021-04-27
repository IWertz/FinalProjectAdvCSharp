using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectGamesWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FinalProjectGamesWebApp.Areas.AdminUser.Controllers
{
    [Area("AdminUser")]
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
        [Route("AdminUser/User/Account")]
        public IActionResult Account()
        {
            return View(CurrentUser.Current);
        }

        //allows users to edit the information about their account
        [Route("AdminUser/User/Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Console.WriteLine("Admin User Edit Request");
            var u = user.Get(id);
            return View(u);
        }

        //updates the database, if authentication level changes, they move to their proper place
        [Route("AdminUser/User/Edit")]
        [HttpPost]
        public IActionResult Edit(User u)
        {
            Console.WriteLine("Admin User Edit Response");
            if (ModelState.IsValid)
            {
                user.Update(u);
                user.Save();
                CurrentUser.Current = u;
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
                Console.WriteLine("Admin User Edit Response Failed");
                ViewBag.FailEdit = "Could not edit account. Please try again.";
                return View();
            }
        }

        //allows users to delete their account
        [Route("AdminUser/User/Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("Admin User Delete Request");
            var u = user.Get(id);
            return View(u);
        }

        //saves changes to database, deletes cascading reviews made by the user, and wipes user computer of user info
        [Route("AdminUser/User/Delete")]
        [HttpPost]
        public IActionResult Delete(User u)
        {
            Console.WriteLine("Admin User Delete Response");
            var reviews = review.List(new QueryOptions<Review> { Where = r => r.UserId == u.UserId });
            foreach (Review r in reviews)
            {
                review.Delete(r);
            }
            user.Delete(u);
            user.Save();
            CurrentUser.Current = null;
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}