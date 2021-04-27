using System;
using Xunit;
using FinalProjectGamesWebApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using FinalProjectGamesWebApp.Areas.AdminUser.Controllers;
using Moq;

namespace FinalProjectGamesWebAppTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestGame()
        {
            //Arrange
            Game game = new Game();
            game.GameId = 21;
            game.Title = "Minecraft";
            game.Rating = 4.75;
            game.TotalReviews = 12;
            string expected = "21, Minecraft, 4.75, 12";
            string actual;

            //Act
            actual = game.GameId + ", " + game.Title + ", " + game.Rating + ", " + game.TotalReviews;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestReview()
        {
            //Arrange
            Review review = new Review();
            review.ReviewId = 21;
            review.UserId = 22;
            review.GameId = 23;
            review.Rating = 5;
            review.ReviewContent = "It was great";
            string expected = "21, 22, 23, 5, It was great";
            string actual;

            //Act
            actual = review.ReviewId + ", " + review.UserId + ", " + review.GameId + ", " + review.Rating + ", " + review.ReviewContent;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestUser()
        {
            //Arrange
            User user = new User();
            user.UserId = 21;
            user.UserName = "Apple";
            user.Password = "Orange";
            user.AuthLevel = 2;
            string expected = "21, Apple, Orange, 2";
            string actual;

            //Act
            actual = user.UserId + ", " + user.UserName + ", " + user.Password + ", " + user.AuthLevel;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestRepo()
        {
            //Arrange
            var repGame = new Mock<IRepository<Game>>();
            var repReview = new Mock<IRepository<Review>>();
            var repUser = new Mock<IRepository<User>>();
            var controller = new HomeController(repGame.Object, repReview.Object, repUser.Object);

            //Act
            var result = controller.Index("");

            //Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
