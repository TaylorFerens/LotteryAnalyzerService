using Microsoft.EntityFrameworkCore.Migrations;

namespace LotteryAnalyzer.Migrations
{
    public partial class UPDATE_STATISTICS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LotteryDomain",
                table: "SearchStatistics");

            migrationBuilder.AddColumn<string>(
                name: "LotteryId",
                table: "SearchStatistics",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LotteryId",
                table: "SearchStatistics");

            migrationBuilder.AddColumn<int>(
                name: "LotteryDomain",
                table: "SearchStatistics",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
