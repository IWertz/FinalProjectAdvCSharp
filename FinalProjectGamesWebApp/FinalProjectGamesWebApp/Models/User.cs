using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectGamesWebApp.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter a user name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter an authorization level")]
        [Range(0, 2, ErrorMessage = "Authorization level must be between 0 and 2")]
        public int? AuthLevel { get; set; }
    }
}
