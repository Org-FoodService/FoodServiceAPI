using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodServiceAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Order",
                newName: "Id");

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 9, 11, 10, 16, 729, DateTimeKind.Local).AddTicks(9653));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 7, 11, 10, 16, 729, DateTimeKind.Local).AddTicks(9679));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 5, 11, 10, 16, 729, DateTimeKind.Local).AddTicks(9681));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 4,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 12, 11, 10, 16, 729, DateTimeKind.Local).AddTicks(9683));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 5,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 9, 11, 10, 16, 729, DateTimeKind.Local).AddTicks(9686));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 6,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 12, 11, 10, 16, 729, DateTimeKind.Local).AddTicks(9688));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdate",
                value: new DateTime(2024, 9, 2, 11, 10, 16, 730, DateTimeKind.Local).AddTicks(359));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Order",
                newName: "OrderId");

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 4, 17, 49, 37, 384, DateTimeKind.Local).AddTicks(2395));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 2, 17, 49, 37, 384, DateTimeKind.Local).AddTicks(2420));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpirationDate",
                value: new DateTime(2024, 8, 31, 17, 49, 37, 384, DateTimeKind.Local).AddTicks(2423));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 4,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 7, 17, 49, 37, 384, DateTimeKind.Local).AddTicks(2426));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 5,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 4, 17, 49, 37, 384, DateTimeKind.Local).AddTicks(2431));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 6,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 7, 17, 49, 37, 384, DateTimeKind.Local).AddTicks(2434));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdate",
                value: new DateTime(2024, 8, 28, 17, 49, 37, 384, DateTimeKind.Local).AddTicks(3991));
        }
    }
}
