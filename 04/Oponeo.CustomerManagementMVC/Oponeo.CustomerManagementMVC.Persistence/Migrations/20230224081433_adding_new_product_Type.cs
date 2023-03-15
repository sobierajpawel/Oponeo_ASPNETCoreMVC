using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oponeo.CustomerManagementMVC.WebApp.Migrations
{
    public partial class adding_new_product_Type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Product");
        }
    }
}
