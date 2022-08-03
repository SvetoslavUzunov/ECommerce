using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlatRockTechnology.eCommerce.DataLayer.Migrations
{
    public partial class AddProductsCategoriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategoriesEntity_Categories_CategoryId",
                table: "ProductCategoriesEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategoriesEntity_Products_ProductId",
                table: "ProductCategoriesEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategoriesEntity",
                table: "ProductCategoriesEntity");

            migrationBuilder.RenameTable(
                name: "ProductCategoriesEntity",
                newName: "ProductCategories");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategoriesEntity_CategoryId",
                table: "ProductCategories",
                newName: "IX_ProductCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories",
                columns: new[] { "ProductId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Categories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Products_ProductId",
                table: "ProductCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Categories_CategoryId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Products_ProductId",
                table: "ProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories");

            migrationBuilder.RenameTable(
                name: "ProductCategories",
                newName: "ProductCategoriesEntity");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategoriesEntity",
                newName: "IX_ProductCategoriesEntity_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategoriesEntity",
                table: "ProductCategoriesEntity",
                columns: new[] { "ProductId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategoriesEntity_Categories_CategoryId",
                table: "ProductCategoriesEntity",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategoriesEntity_Products_ProductId",
                table: "ProductCategoriesEntity",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
