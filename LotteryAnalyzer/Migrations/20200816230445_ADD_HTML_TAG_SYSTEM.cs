using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LotteryAnalyzer.Migrations
{
    public partial class ADD_HTML_TAG_SYSTEM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lottery",
                keyColumn: "LotteryId",
                keyValue: new Guid("0bae9861-be75-4204-90cb-1266dc358a10"));

            migrationBuilder.DeleteData(
                table: "Lottery",
                keyColumn: "LotteryId",
                keyValue: new Guid("15ca4248-c0b4-4de8-a5e2-ab8d219d2043"));

            migrationBuilder.DeleteData(
                table: "Lottery",
                keyColumn: "LotteryId",
                keyValue: new Guid("cbd0aca0-1f32-44da-a635-ab2e61325c83"));

            migrationBuilder.DropColumn(
                name: "LotteryDomain",
                table: "Lottery");

            migrationBuilder.AddColumn<Guid>(
                name: "LotteryDateTagBrokerId",
                table: "Lottery",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LotteryDateTagId",
                table: "Lottery",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LotteryHtmlTagBrokerId",
                table: "Lottery",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LotteryHtmlTagBrokers",
                columns: table => new
                {
                    LotteryHtmlTagBrokerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryHtmlTagBrokers", x => x.LotteryHtmlTagBrokerId);
                });

            migrationBuilder.CreateTable(
                name: "LotteryHtmlTags",
                columns: table => new
                {
                    LotteryHtmlTagId = table.Column<Guid>(nullable: false),
                    LotteryHtmlTagBrokerId = table.Column<Guid>(nullable: true),
                    HtmlTag = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryHtmlTags", x => x.LotteryHtmlTagId);
                    table.ForeignKey(
                        name: "FK_LotteryHtmlTags_LotteryHtmlTagBrokers_LotteryHtmlTagBrokerId",
                        column: x => x.LotteryHtmlTagBrokerId,
                        principalTable: "LotteryHtmlTagBrokers",
                        principalColumn: "LotteryHtmlTagBrokerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Lottery",
                columns: new[] { "LotteryId", "LastDrawDate", "LotteryDateTagBrokerId", "LotteryDateTagId", "LotteryHtmlTagBrokerId", "LotteryName", "LotteryUrl" },
                values: new object[] { new Guid("246a70d1-2103-4fc4-b259-d451ee3ce86a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83"), null, null, "Lotto Max", "https://www.wclc.com/winning-numbers/lotto-max-extra.htm" });

            migrationBuilder.InsertData(
                table: "LotteryHtmlTagBrokers",
                column: "LotteryHtmlTagBrokerId",
                values: new object[]
                {
                    new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83"),
                    new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e84")
                });

            migrationBuilder.InsertData(
                table: "Lottery",
                columns: new[] { "LotteryId", "LastDrawDate", "LotteryDateTagBrokerId", "LotteryDateTagId", "LotteryHtmlTagBrokerId", "LotteryName", "LotteryUrl" },
                values: new object[,]
                {
                    { new Guid("b31b44b3-523b-436b-a006-ef8f748cfe68"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e84"), null, new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83"), "Lotto 649", "https://www.wclc.com/winning-numbers/lotto-649-extra.htm" },
                    { new Guid("8c22d69b-4b39-438a-81cb-fc1c1bc3b2c6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e84"), null, new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83"), "Western 649", "https://www.wclc.com/winning-numbers/Western-649-extra.htm" }
                });

            migrationBuilder.InsertData(
                table: "LotteryHtmlTags",
                columns: new[] { "LotteryHtmlTagId", "HtmlTag", "LotteryHtmlTagBrokerId" },
                values: new object[,]
                {
                    { new Guid("6fa14576-2c64-4fd6-892f-eb000bc6cae9"), "pastWinNumber", new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83") },
                    { new Guid("6fa14576-2c64-4fd6-892f-eb000bc6cae0"), "pastWinNumDate", new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e84") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lottery_LotteryDateTagId",
                table: "Lottery",
                column: "LotteryDateTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Lottery_LotteryHtmlTagBrokerId",
                table: "Lottery",
                column: "LotteryHtmlTagBrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryHtmlTags_LotteryHtmlTagBrokerId",
                table: "LotteryHtmlTags",
                column: "LotteryHtmlTagBrokerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lottery_LotteryHtmlTagBrokers_LotteryDateTagId",
                table: "Lottery",
                column: "LotteryDateTagId",
                principalTable: "LotteryHtmlTagBrokers",
                principalColumn: "LotteryHtmlTagBrokerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lottery_LotteryHtmlTagBrokers_LotteryHtmlTagBrokerId",
                table: "Lottery",
                column: "LotteryHtmlTagBrokerId",
                principalTable: "LotteryHtmlTagBrokers",
                principalColumn: "LotteryHtmlTagBrokerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lottery_LotteryHtmlTagBrokers_LotteryDateTagId",
                table: "Lottery");

            migrationBuilder.DropForeignKey(
                name: "FK_Lottery_LotteryHtmlTagBrokers_LotteryHtmlTagBrokerId",
                table: "Lottery");

            migrationBuilder.DropTable(
                name: "LotteryHtmlTags");

            migrationBuilder.DropTable(
                name: "LotteryHtmlTagBrokers");

            migrationBuilder.DropIndex(
                name: "IX_Lottery_LotteryDateTagId",
                table: "Lottery");

            migrationBuilder.DropIndex(
                name: "IX_Lottery_LotteryHtmlTagBrokerId",
                table: "Lottery");

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

            migrationBuilder.DropColumn(
                name: "LotteryDateTagBrokerId",
                table: "Lottery");

            migrationBuilder.DropColumn(
                name: "LotteryDateTagId",
                table: "Lottery");

            migrationBuilder.DropColumn(
                name: "LotteryHtmlTagBrokerId",
                table: "Lottery");

            migrationBuilder.AddColumn<int>(
                name: "LotteryDomain",
                table: "Lottery",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Lottery",
                columns: new[] { "LotteryId", "LastDrawDate", "LotteryDomain", "LotteryName", "LotteryUrl" },
                values: new object[,]
                {
                    { new Guid("0bae9861-be75-4204-90cb-1266dc358a10"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lotto Max", "https://www.wclc.com/winning-numbers/lotto-max-extra.htm" },
                    { new Guid("15ca4248-c0b4-4de8-a5e2-ab8d219d2043"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lotto 649", "https://www.wclc.com/winning-numbers/lotto-649-extra.htm" },
                    { new Guid("cbd0aca0-1f32-44da-a635-ab2e61325c83"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Western 649", "https://www.wclc.com/winning-numbers/Western-649-extra.htm" }
                });
        }
    }
}
