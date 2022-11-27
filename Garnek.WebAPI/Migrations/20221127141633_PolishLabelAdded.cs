using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Garnek.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class PolishLabelAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("410e1010-7ed7-49b9-bf6c-02a07a9bbede"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d48567be-8bf7-4b34-a46f-17bb196cfed2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f8b53255-f93e-41b1-80dc-650dd7f83a2e"));

            migrationBuilder.AddColumn<string>(
                name: "PolishLabel",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "PolishLabel" },
                values: new object[,]
                {
                    { new Guid("0416457b-a013-491b-b38c-57b6699d4990"), new DateTime(2022, 11, 27, 14, 16, 33, 586, DateTimeKind.Utc).AddTicks(5730), "Things", "Rzeczy 🎸" },
                    { new Guid("e8e14638-59c8-46d8-8aa7-e3f0aa7b86f6"), new DateTime(2022, 11, 27, 14, 16, 33, 586, DateTimeKind.Utc).AddTicks(5630), "People", "Osoby 💆🏼‍" },
                    { new Guid("f64206c8-4dcb-4415-89cd-367571420fb5"), new DateTime(2022, 11, 27, 14, 16, 33, 586, DateTimeKind.Utc).AddTicks(5720), "Places", "Miejsca 🌁" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0416457b-a013-491b-b38c-57b6699d4990"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e8e14638-59c8-46d8-8aa7-e3f0aa7b86f6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f64206c8-4dcb-4415-89cd-367571420fb5"));

            migrationBuilder.DropColumn(
                name: "PolishLabel",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("410e1010-7ed7-49b9-bf6c-02a07a9bbede"), new DateTime(2022, 11, 21, 22, 23, 12, 635, DateTimeKind.Utc).AddTicks(3520), "People" },
                    { new Guid("d48567be-8bf7-4b34-a46f-17bb196cfed2"), new DateTime(2022, 11, 21, 22, 23, 12, 635, DateTimeKind.Utc).AddTicks(3590), "Places" },
                    { new Guid("f8b53255-f93e-41b1-80dc-650dd7f83a2e"), new DateTime(2022, 11, 21, 22, 23, 12, 635, DateTimeKind.Utc).AddTicks(3590), "Things" }
                });
        }
    }
}
