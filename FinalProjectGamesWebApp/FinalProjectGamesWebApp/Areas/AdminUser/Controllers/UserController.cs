using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectGamesWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectGamesWebApp.Areas.AdminUser.Controllers
{
    [Area("AdminUser")]
    public class UserController : Controller
    {
        private GameContext context { get; set; }

        public UserController(GameContext ctx)
        {
            context = ctx;
        }

        [Route("AdminUser/User/Account")]
        public IActionResult Account()
        {
            return View(CurrentUser.Current);
        }

        [Route("AdminUser/User/Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Console.WriteLine("Admin User Edit Request");
            var user = context.Users.Find(id);
            return View(user);
        }

        [Route("AdminUser/User/Edit")]
        [HttpPost]
        public IActionResult Edit(User user)
        {
            Console.WriteLine("Admin User Edit Response");
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
                Console.WriteLine("Admin User Edit Response Failed");
                ViewBag.FailEdit = "Could not edit account. Please try again.";
                return View();
            }
        }

        [Route("AdminUser/User/Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("Admin User Delete Request");
            var user = context.Users.Find(id);
            return View(user);
        }

        [Route("AdminUser/User/Delete")]
        [HttpPost]
        public IActionResult Delete(User user)
        {
            Console.WriteLine("Admin User Delete Response");
            var reviews = context.Reviews.Where(u => u.UserId == user.UserId).ToList();
            foreach (Review r in reviews)
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