using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectGamesWebApp.Models
{
    //game class, holds gameid, title, rating, and reviews
    public class Game
    {
        public int GameId { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }

        public double Rating { get; set; }

        public int TotalReviews { get; set; }
    }
}
