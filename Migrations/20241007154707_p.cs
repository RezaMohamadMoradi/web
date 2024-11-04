using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1.Migrations
{
    /// <inheritdoc />
    public partial class p : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cartId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createfactor",
                table: "carts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isfinaly",
                table: "carts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "userid",
                table: "carts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "id",
                keyValue: 1,
                column: "cartId",
                value: null);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "id",
                keyValue: 2,
                column: "cartId",
                value: null);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "id",
                keyValue: 3,
                column: "cartId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_products_cartId",
                table: "products",
                column: "cartId");

            migrationBuilder.CreateIndex(
                name: "IX_carts_userid",
                table: "carts",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_AspNetUsers_userid",
                table: "carts",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_carts_cartId",
                table: "products",
                column: "cartId",
                principalTable: "carts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_AspNetUsers_userid",
                table: "carts");

            migrationBuilder.DropForeignKey(
                name: "FK_products_carts_cartId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_cartId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_carts_userid",
                table: "carts");

            migrationBuilder.DropColumn(
                name: "cartId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "createfactor",
                table: "carts");

            migrationBuilder.DropColumn(
                name: "isfinaly",
                table: "carts");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "carts");
        }
    }
}
