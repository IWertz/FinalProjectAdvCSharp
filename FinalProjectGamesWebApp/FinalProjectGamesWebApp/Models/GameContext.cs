using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectGamesWebApp.Models
{
    //game context class, holds database connections that created the migration based on the coded tables below
    public class GameContext :DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    GameId = 1,
                    Title = "Portal 2",
                    Rating = 4,
                    TotalReviews = 2
                },
                new Game
                {
                    GameId = 2,
                    Title = "Fortnite",
                    Rating = 4,
                    TotalReviews = 2
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "IWertz",
                    Password = "Iw062401!",
                    AuthLevel = 2
                },
                new User
                {
                    UserId = 2,
                    UserName = "RegularUser",
                    Password = "password",
                    AuthLevel = 1
                }
            );

            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    ReviewId = 1,
                    UserId = 1,
                    GameId = 1,
                    Rating = 4,
                    ReviewContent = "It's pretty good"
                },
                new Review
                {
                    ReviewId = 2,
                    UserId = 2,
                    GameId = 1,
                    Rating = 4,
                    ReviewContent = "Good Game!"
                },
                new Review
                {
                    ReviewId = 3,
                    UserId = 1,
                    GameId = 2,
                    Rating = 4,
                    ReviewContent = "It's alright"
                },
                new Review
                {
                    ReviewId = 4,
                    UserId = 2,
                    GameId = 2,
                    Rating = 4,
                    ReviewContent = "I really enjoyed this game"
                }
            );
        }
    }
}
