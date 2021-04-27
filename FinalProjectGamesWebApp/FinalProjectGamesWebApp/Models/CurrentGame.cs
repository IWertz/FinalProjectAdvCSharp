using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectGamesWebApp.Models
{
    //holds the current game that the user is looking at, in order to perform actions like adding or editing reviews
    public static class CurrentGame
    {
        public static Game Current { get; set; }
    }
}
