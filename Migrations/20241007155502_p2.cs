using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1.Migrations
{
    /// <inheritdoc />
    public partial class p2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "cartproductt",
                columns: table => new
                {
                    cartsId = table.Column<int>(type: "int", nullable: false),
                    producttsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartproductt", x => new { x.cartsId, x.producttsid });
                    table.ForeignKey(
                        name: "FK_cartproductt_carts_cartsId",
                        column: x => x.cartsId,
                        principalTable: "carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cartproductt_products_producttsid",
                        column: x => x.producttsid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carts_userid",
                table: "carts",
                column: "userid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cartproductt_producttsid",
                table: "cartproductt",
                column: "producttsid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cartproductt");

            migrationBuilder.DropIndex(
                name: "IX_carts_userid",
                table: "carts");

            migrationBuilder.AddColumn<int>(
                name: "cartId",
                table: "products",
                type: "int",
                nullable: true);

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
                name: "FK_products_carts_cartId",
                table: "products",
                column: "cartId",
                principalTable: "carts",
                principalColumn: "Id");
        }
    }
}
