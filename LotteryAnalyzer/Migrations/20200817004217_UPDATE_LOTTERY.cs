using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LotteryAnalyzer.Migrations
{
    public partial class UPDATE_LOTTERY : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lottery",
                keyColumn: "LotteryId",
                keyValue: new Guid("246a70d1-2103-4fc4-b259-d451ee3ce86a"));

            migrationBuilder.DeleteData(
                table: "Lottery",
                keyColumn: "LotteryId",
                keyValue: new Guid("8c22d69b-4b39-438a-81cb-fc1c1bc3b2c6"));

            migrationBuilder.DeleteData(
                table: "Lottery",
                keyColumn: "LotteryId",
                keyValue: new Guid("b31b44b3-523b-436b-a006-ef8f748cfe68"));

            migrationBuilder.AddColumn<bool>(
                name: "HasBonus",
                table: "Lottery",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxlotteryNumber",
                table: "Lottery",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalNumberDraws",
                table: "Lottery",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Lottery",
                columns: new[] { "LotteryId", "HasBonus", "LastDrawDate", "LotteryDateTagBrokerId", "LotteryDateTagId", "LotteryHtmlTagBrokerId", "LotteryName", "LotteryUrl", "MaxlotteryNumber", "TotalNumberDraws" },
                values: new object[,]
                {
                    { new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e86"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e84"), null, new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83"), "Lotto Max", "https://www.wclc.com/winning-numbers/lotto-max-extra.htm", 49, 7 },
                    { new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e87"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e84"), null, new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83"), "Lotto 649", "https://www.wclc.com/winning-numbers/lotto-649-extra.htm", 49, 6 },
                    { new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e88"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e84"), null, new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83"), "Western 649", "https://www.wclc.com/winning-numbers/Western-649-extra.htm", 49, 6 }
                });

            migrationBuilder.InsertData(
                table: "LotteryNumbers",
                columns: new[] { "LotteryNumberId", "LotteryId", "Number", "TimesPicked" },
                values: new object[,]
                {
                    { new Guid("9d48a39d-663b-4c7a-b6fb-28f8c9304361"), new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e86"), "1", 0 },
                    { new Guid("cb85ef25-f4d9-4c8c-9fb7-63bcb8eb5cb4"), new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e87"), "1", 0 },
                    { new Guid("175a91a4-6a75-48ea-a20d-901294d41c21"), new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e88"), "1", 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LotteryNumbers",
                keyColumn: "LotteryNumberId",
                keyValue: new Guid("175a91a4-6a75-48ea-a20d-901294d41c21"));

            migrationBuilder.DeleteData(
                table: "LotteryNumbers",
                keyColumn: "LotteryNumberId",
                keyValue: new Guid("9d48a39d-663b-4c7a-b6fb-28f8c9304361"));

            migrationBuilder.DeleteData(
                table: "LotteryNumbers",
                keyColumn: "LotteryNumberId",
                keyValue: new Guid("cb85ef25-f4d9-4c8c-9fb7-63bcb8eb5cb4"));

            migrationBuilder.DeleteData(
                table: "Lottery",
                keyColumn: "LotteryId",
                keyValue: new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e86"));

            migrationBuilder.DeleteData(
                table: "Lottery",
                keyColumn: "LotteryId",
                keyValue: new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e87"));

            migrationBuilder.DeleteData(
                table: "Lottery",
                keyColumn: "LotteryId",
                keyValue: new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e88"));

            migrationBuilder.DropColumn(
                name: "HasBonus",
                table: "Lottery");

            migrationBuilder.DropColumn(
                name: "MaxlotteryNumber",
                table: "Lottery");

            migrationBuilder.DropColumn(
                name: "TotalNumberDraws",
                table: "Lottery");

            migrationBuilder.InsertData(
                table: "Lottery",
                columns: new[] { "LotteryId", "LastDrawDate", "LotteryDateTagBrokerId", "LotteryDateTagId", "LotteryHtmlTagBrokerId", "LotteryName", "LotteryUrl" },
                values: new object[,]
                {
                    { new Guid("246a70d1-2103-4fc4-b259-d451ee3ce86a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83"), null, null, "Lotto Max", "https://www.wclc.com/winning-numbers/lotto-max-extra.htm" },
                    { new Guid("b31b44b3-523b-436b-a006-ef8f748cfe68"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e84"), null, new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83"), "Lotto 649", "https://www.wclc.com/winning-numbers/lotto-649-extra.htm" },
                    { new Guid("8c22d69b-4b39-438a-81cb-fc1c1bc3b2c6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e84"), null, new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83"), "Western 649", "https://www.wclc.com/winning-numbers/Western-649-extra.htm" }
                });
        }
    }
}
