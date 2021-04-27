using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectGamesWebApp.Models
{
    //holds information on the current signed in user in order to perform actions that unauthenticated users cannot
    public static class CurrentUser
    {
        public static User Current { get; set; }
    }
}
