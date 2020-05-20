using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LotteryAnalyzer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchStatistics",
                columns: table => new
                {
                    SearchStatisticID = table.Column<Guid>(nullable: false),
                    lotteryDomain = table.Column<int>(nullable: false),
                    totalTimesSearched = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchStatistics", x => x.SearchStatisticID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchStatistics");
        }
    }
}
