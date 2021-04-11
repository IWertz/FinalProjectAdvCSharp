using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectGamesWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectGamesWebApp.Areas.AdminUser.Controllers
{
    [Area("AdminUser")]
    public class GameController : Controller
    {
        private GameContext context { get; set; }

        public GameController(GameContext ctx)
        {
            context = ctx;
        }

        [Route("AdminUser/Game/View")]
        [HttpGet]
        public IActionResult View(int id)
        {
            Game game = context.Games.Find(id);

            return View(game);
        }

        [Route("AdminUser/Game/Add")]
        public IActionResult Add()
        {
            return View("Edit", new Game());
        }

        [Route("AdminUser/Game/Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Console.WriteLine("Admin Game Edit Request");
            var game = context.Games.Find(id);
            return View(game);
        }

        [Route("AdminUser/Game/Edit")]
        [HttpPost]
        public IActionResult Edit(Game game)
        {
            Console.WriteLine("Admin Game Edit Response");
            if (ModelState.IsValid)
            {
                if (game.GameId == 0)
                {
                    game.Rating = 0;
                    game.TotalReviews = 0;
                    context.Games.Add(game);
                }
                else
                {
                    context.Games.Update(game);
                }
                context.SaveChanges();
                ViewBag.FailEdit = "";
                return View("View", game);
            }
            else
            {
                Console.WriteLine("Admin Game Edit Response Failed");
                ViewBag.FailEdit = "Could not edit game. Please try again.";
                return View(game);
            }
        }

        [Route("AdminUser/Game/Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("Admin Game Delete Request");
            var game = context.Games.Find(id);
            return View(game);
        }

        [Route("AdminUser/Game/Delete")]
        [HttpPost]
        public IActionResult Delete(Game game)
        {
            Console.WriteLine("Admin Game Delete Response");
            var reviews = context.Reviews.Where(g => g.GameId == game.GameId).ToList();
            foreach (Review r in reviews)
            {
                context.Reviews.Remove(r);
            }
            context.Games.Remove(game);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}