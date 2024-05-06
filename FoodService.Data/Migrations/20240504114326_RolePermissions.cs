using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodService.Data.Migrations
{
    /// <inheritdoc />
    public partial class RolePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanAccessFinancialResources",
                table: "AspNetRoles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanAddEmployee",
                table: "AspNetRoles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanAddProduct",
                table: "AspNetRoles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanChangePrice",
                table: "AspNetRoles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanControlStock",
                table: "AspNetRoles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 11, 8, 43, 25, 900, DateTimeKind.Local).AddTicks(7960));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 9, 8, 43, 25, 900, DateTimeKind.Local).AddTicks(7982));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 7, 8, 43, 25, 900, DateTimeKind.Local).AddTicks(7984));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 4,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 14, 8, 43, 25, 900, DateTimeKind.Local).AddTicks(7986));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 5,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 11, 8, 43, 25, 900, DateTimeKind.Local).AddTicks(7987));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 6,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 14, 8, 43, 25, 900, DateTimeKind.Local).AddTicks(7989));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanAccessFinancialResources",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CanAddEmployee",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CanAddProduct",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CanChangePrice",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CanControlStock",
                table: "AspNetRoles");

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 10, 15, 31, 15, 477, DateTimeKind.Local).AddTicks(2081));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 8, 15, 31, 15, 477, DateTimeKind.Local).AddTicks(2101));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 6, 15, 31, 15, 477, DateTimeKind.Local).AddTicks(2103));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 4,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 13, 15, 31, 15, 477, DateTimeKind.Local).AddTicks(2104));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 5,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 10, 15, 31, 15, 477, DateTimeKind.Local).AddTicks(2106));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 6,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 13, 15, 31, 15, 477, DateTimeKind.Local).AddTicks(2108));
        }
    }
}
