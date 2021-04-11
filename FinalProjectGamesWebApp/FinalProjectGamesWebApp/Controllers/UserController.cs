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
        private GameContext context { get; set; }

        public UserController(GameContext ctx)
        {
            context = ctx;
        }

        [HttpPost]
        public IActionResult Add(User user)
        {
            Console.WriteLine("Unauthenticated User Add Response");
            foreach (User u in context.Users.ToList())
            {
                if (user.UserName == u.UserName)
                {
                    ViewBag.CompletionMessage = "";
                    ViewBag.errorMessage = "";
                    ViewBag.FailCompletionMessage = "That username already exists. Try logging in instead.";
                    return View("~/Views/Home/Authentication.cshtml");
                }
            }
            if (ModelState.IsValid)
            {
                if (user.UserId == 0) context.Users.Add(user);
                else context.Users.Update(user);
                context.SaveChanges();
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