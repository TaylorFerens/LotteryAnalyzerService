using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LotteryAnalyzer.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lottery",
                columns: table => new
                {
                    LotteryId = table.Column<Guid>(nullable: false),
                    LotteryName = table.Column<string>(nullable: true),
                    LotteryUrl = table.Column<string>(nullable: true),
                    LotteryDomain = table.Column<int>(nullable: false),
                    LastDrawDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lottery", x => x.LotteryId);
                });

            migrationBuilder.CreateTable(
                name: "SearchStatistics",
                columns: table => new
                {
                    SearchStatisticId = table.Column<Guid>(nullable: false),
                    LotteryId = table.Column<string>(nullable: true),
                    TotalTimesSearched = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchStatistics", x => x.SearchStatisticId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AccessToken = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "LotteryNumbers",
                columns: table => new
                {
                    LotteryNumberId = table.Column<Guid>(nullable: false),
                    LotteryId = table.Column<Guid>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    TimesPicked = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryNumbers", x => x.LotteryNumberId);
                    table.ForeignKey(
                        name: "FK_LotteryNumbers_Lottery_LotteryId",
                        column: x => x.LotteryId,
                        principalTable: "Lottery",
                        principalColumn: "LotteryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Lottery",
                columns: new[] { "LotteryId", "LastDrawDate", "LotteryDomain", "LotteryName", "LotteryUrl" },
                values: new object[,]
                {
                    { new Guid("0bae9861-be75-4204-90cb-1266dc358a10"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lotto Max", "https://www.wclc.com/winning-numbers/lotto-max-extra.htm" },
                    { new Guid("15ca4248-c0b4-4de8-a5e2-ab8d219d2043"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lotto 649", "https://www.wclc.com/winning-numbers/lotto-649-extra.htm" },
                    { new Guid("cbd0aca0-1f32-44da-a635-ab2e61325c83"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Western 649", "https://www.wclc.com/winning-numbers/Western-649-extra.htm" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LotteryNumbers_LotteryId",
                table: "LotteryNumbers",
                column: "LotteryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotteryNumbers");

            migrationBuilder.DropTable(
                name: "SearchStatistics");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Lottery");
        }
    }
}
