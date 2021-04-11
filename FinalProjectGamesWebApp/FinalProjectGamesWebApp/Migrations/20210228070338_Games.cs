using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectGamesWebApp.Migrations
{
    public partial class Games : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Reviews_ReviewId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ReviewId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "ReviewId",
                table: "Games",
                newName: "TotalReviews");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 1,
                column: "TotalReviews",
                value: 2);

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "Rating", "Title", "TotalReviews" },
                values: new object[] { 2, 4.0, "Fortnite", 2 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "GameId",
                value: 1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthLevel", "Password", "UserName" },
                values: new object[] { 2, 1, "password", "RegularUser" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "GameId", "Rating", "ReviewContent", "UserId" },
                values: new object[] { 3, 2, 4, "It's alright", 1 });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "GameId", "Rating", "ReviewContent", "UserId" },
                values: new object[] { 2, 1, 4, "Good Game!", 2 });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "GameId", "Rating", "ReviewContent", "UserId" },
                values: new object[] { 4, 2, 4, "I really enjoyed this game", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_GameId",
                table: "Reviews",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Games_GameId",
                table: "Reviews",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Games_GameId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_GameId",
                table: "Reviews");

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "TotalReviews",
                table: "Games",
                newName: "ReviewId");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 1,
                column: "ReviewId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Games_ReviewId",
                table: "Games",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Reviews_ReviewId",
                table: "Games",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
