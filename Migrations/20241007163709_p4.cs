using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1.Migrations
{
    /// <inheritdoc />
    public partial class p4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cartitem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemid = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    cartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartitem", x => x.id);
                    table.ForeignKey(
                        name: "FK_cartitem_carts_cartId",
                        column: x => x.cartId,
                        principalTable: "carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_cartitem_items_itemid",
                        column: x => x.itemid,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cartitem_cartId",
                table: "cartitem",
                column: "cartId");

            migrationBuilder.CreateIndex(
                name: "IX_cartitem_itemid",
                table: "cartitem",
                column: "itemid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cartitem");
        }
    }
}
