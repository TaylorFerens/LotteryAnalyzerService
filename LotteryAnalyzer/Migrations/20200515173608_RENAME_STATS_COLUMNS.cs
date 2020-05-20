using Microsoft.EntityFrameworkCore.Migrations;

namespace LotteryAnalyzer.Migrations
{
    public partial class RENAME_STATS_COLUMNS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "totalTimesSearched",
                table: "SearchStatistics",
                newName: "TotalTimesSearched");

            migrationBuilder.RenameColumn(
                name: "lotteryDomain",
                table: "SearchStatistics",
                newName: "LotteryDomain");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalTimesSearched",
                table: "SearchStatistics",
                newName: "totalTimesSearched");

            migrationBuilder.RenameColumn(
                name: "LotteryDomain",
                table: "SearchStatistics",
                newName: "lotteryDomain");
        }
    }
}
