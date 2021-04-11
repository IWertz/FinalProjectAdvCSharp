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
        private GameContext context { get; set; }

        public UserController(GameContext ctx)
        {
            context = ctx;
        }

        [Route("NormalUser/User/Account")]
        public IActionResult Account()
        {
            return View(CurrentUser.Current);
        }

        [Route("NormalUser/User/Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Console.WriteLine("User User Edit Request");
            var user = context.Users.Find(id);
            return View(user);
        }

        [Route("NormalUser/User/Edit")]
        [HttpPost]
        public IActionResult Edit(User user)
        {
            Console.WriteLine("User User Edit Response");
            if (ModelState.IsValid)
            {
                context.Users.Update(user);
                context.SaveChanges();
                CurrentUser.Current = user;
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

        [Route("NormalUser/User/Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("User User Delete Request");
            var user = context.Users.Find(id);
            return View(user);
        }

        [Route("NormalUser/User/Delete")]
        [HttpPost]
        public IActionResult Delete(User user)
        {
            Console.WriteLine("User User Edit Response");
            var reviews = context.Reviews.Where(u => u.UserId == user.UserId).ToList();
            foreach(Review r in reviews)
            {
                context.Reviews.Remove(r);
            }
            context.Users.Remove(user);
            context.SaveChanges();
            CurrentUser.Current = null;
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}