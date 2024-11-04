using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1.Migrations
{
    /// <inheritdoc />
    public partial class rf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartitem_carts_cartId",
                table: "cartitem");

            migrationBuilder.RenameColumn(
                name: "cartId",
                table: "cartitem",
                newName: "cartid");

            migrationBuilder.RenameIndex(
                name: "IX_cartitem_cartId",
                table: "cartitem",
                newName: "IX_cartitem_cartid");

            migrationBuilder.AlterColumn<int>(
                name: "cartid",
                table: "cartitem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_cartitem_carts_cartid",
                table: "cartitem",
                column: "cartid",
                principalTable: "carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartitem_carts_cartid",
                table: "cartitem");

            migrationBuilder.RenameColumn(
                name: "cartid",
                table: "cartitem",
                newName: "cartId");

            migrationBuilder.RenameIndex(
                name: "IX_cartitem_cartid",
                table: "cartitem",
                newName: "IX_cartitem_cartId");

            migrationBuilder.AlterColumn<int>(
                name: "cartId",
                table: "cartitem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_cartitem_carts_cartId",
                table: "cartitem",
                column: "cartId",
                principalTable: "carts",
                principalColumn: "Id");
        }
    }
}
