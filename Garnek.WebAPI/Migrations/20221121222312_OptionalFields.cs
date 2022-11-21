using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Garnek.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class OptionalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0207d2d9-6161-4340-a1e1-742b16a4b0f9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("12f45516-091e-48ed-92aa-641b216215a8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7def094c-5c74-4be9-ae47-b6fefca5d02c"));

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "Teams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0207d2d9-6161-4340-a1e1-742b16a4b0f9"), new DateTime(2022, 11, 20, 11, 53, 44, 287, DateTimeKind.Utc).AddTicks(8000), "Places" },
                    { new Guid("12f45516-091e-48ed-92aa-641b216215a8"), new DateTime(2022, 11, 20, 11, 53, 44, 287, DateTimeKind.Utc).AddTicks(7940), "People" },
                    { new Guid("7def094c-5c74-4be9-ae47-b6fefca5d02c"), new DateTime(2022, 11, 20, 11, 53, 44, 287, DateTimeKind.Utc).AddTicks(8010), "Things" }
                });
        }
    }
}
