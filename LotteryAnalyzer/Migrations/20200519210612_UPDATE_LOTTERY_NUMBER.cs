using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LotteryAnalyzer.Migrations
{
    public partial class UPDATE_LOTTERY_NUMBER : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lottery",
                keyColumn: "LotteryId",
                keyValue: new Guid("19172bd5-92d7-44f2-aee4-367e4a9f42f3"));

            migrationBuilder.DeleteData(
                table: "Lottery",
                keyColumn: "LotteryId",
                keyValue: new Guid("4f370b5d-6e59-463e-b3a8-aa1a6fe03725"));

            migrationBuilder.DeleteData(
                table: "Lottery",
                keyColumn: "LotteryId",
                keyValue: new Guid("fb61262a-4691-4e58-b419-a60f04639f65"));

            migrationBuilder.InsertData(
                table: "Lottery",
                columns: new[] { "LotteryId", "LastDrawDate", "LotteryDomain", "LotteryName", "LotteryUrl" },
                values: new object[,]
                {
                    { new Guid("53338e29-399e-4ffd-a336-0986baff0d34"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lotto Max", "https://www.wclc.com/winning-numbers/lotto-max-extra.htm" },
                    { new Guid("4329ea67-0832-4248-9ba6-7881fb4e566c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lotto 649", "https://www.wclc.com/winning-numbers/lotto-649-extra.htm" },
                    { new Guid("c26a608f-b486-4a14-9b1f-684bdcb06d84"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Western 649", "https://www.wclc.com/winning-numbers/Western-649-extra.htm" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lottery",
                keyColumn: "LotteryId",
                keyValue: new Guid("4329ea67-0832-4248-9ba6-7881fb4e566c"));

            migrationBuilder.DeleteData(
                table: "Lottery",
                keyColumn: "LotteryId",
                keyValue: new Guid("53338e29-399e-4ffd-a336-0986baff0d34"));

            migrationBuilder.DeleteData(
                table: "Lottery",
                keyColumn: "LotteryId",
                keyValue: new Guid("c26a608f-b486-4a14-9b1f-684bdcb06d84"));

            migrationBuilder.InsertData(
                table: "Lottery",
                columns: new[] { "LotteryId", "LastDrawDate", "LotteryDomain", "LotteryName", "LotteryUrl" },
                values: new object[,]
                {
                    { new Guid("fb61262a-4691-4e58-b419-a60f04639f65"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Lotto Max", "https://www.wclc.com/winning-numbers/lotto-max-extra.htm" },
                    { new Guid("4f370b5d-6e59-463e-b3a8-aa1a6fe03725"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Lotto 649", "https://www.wclc.com/winning-numbers/lotto-649-extra.htm" },
                    { new Guid("19172bd5-92d7-44f2-aee4-367e4a9f42f3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Western 649", "https://www.wclc.com/winning-numbers/Western-649-extra.htm" }
                });
        }
    }
}
