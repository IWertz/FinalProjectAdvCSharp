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
        //repository setup
        private IRepository<Game> game { get; set; }
        private IRepository<Review> review { get; set; }
        private IRepository<User> user { get; set; }
        public GameController(IRepository<Game> ctxGame, IRepository<Review> ctxReview, IRepository<User> ctxUser)
        {
            game = ctxGame;
            review = ctxReview;
            user = ctxUser;
        }

        //returns a view of a game by its id
        [Route("AdminUser/Game/View")]
        [HttpGet]
        public IActionResult View(int id)
        {
            Game g = game.Get(id);

            return View(g);
        }

        //returns the edit game action with a new game
        [Route("AdminUser/Game/Add")]
        public IActionResult Add()
        {
            return View("Edit", new Game());
        }

        //allows users to add or edit games, returns the game edit page 
        [Route("AdminUser/Game/Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Console.WriteLine("Admin Game Edit Request");
            var g = game.Get(id);
            return View(g);
        }

        //saves the changes to the database if the model state is valid
        [Route("AdminUser/Game/Edit")]
        [HttpPost]
        public IActionResult Edit(Game g)
        {
            Console.WriteLine("Admin Game Edit Response");
            if (ModelState.IsValid)
            {
                if (g.GameId == 0)
                {
                    g.Rating = 0;
                    g.TotalReviews = 0;
                    game.Insert(g);
                }
                else
                {
                    game.Update(g);
                }
                game.Save();
                ViewBag.FailEdit = "";
                return View("View", g);
            }
            else
            {
                Console.WriteLine("Admin Game Edit Response Failed");
                ViewBag.FailEdit = "Could not edit game. Please try again.";
                return View(g);
            }
        }

        //allows verified users to delete games from the database, returns confirmation page
        [Route("AdminUser/Game/Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Console.WriteLine("Admin Game Delete Request");
            var g = game.Get(id);
            return View(g);
        }

        //once confirmed, the database removes the game and all its reviews from the database
        [Route("AdminUser/Game/Delete")]
        [HttpPost]
        public IActionResult Delete(Game g)
        {
            Console.WriteLine("Admin Game Delete Response");
            var reviews = review.List(new QueryOptions<Review> { Where = r => r.GameId == g.GameId });
            foreach (Review r in reviews)
            {
                review.Delete(r);
            }
            game.Delete(g);
            game.Save();
            return RedirectToAction("Index", "Home");
        }
    }
}