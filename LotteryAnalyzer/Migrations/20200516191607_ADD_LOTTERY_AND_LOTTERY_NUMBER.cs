using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LotteryAnalyzer.Migrations
{
    public partial class ADD_LOTTERY_AND_LOTTERY_NUMBER : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SearchStatisticID",
                table: "SearchStatistics",
                newName: "SearchStatisticId");

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
                name: "Lottery");

            migrationBuilder.RenameColumn(
                name: "SearchStatisticId",
                table: "SearchStatistics",
                newName: "SearchStatisticID");
        }
    }
}
