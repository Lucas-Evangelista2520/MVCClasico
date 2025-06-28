using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCClasico.Migrations
{
    /// <inheritdoc />
    public partial class AddCartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Carts",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Carts",
                newName: "id");
        }
    }
}
