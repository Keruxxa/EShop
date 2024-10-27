using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Category_Closure_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryClosureNodes",
                columns: table => new
                {
                    AncestorCategoryId = table.Column<int>(type: "integer", nullable: false),
                    DescendantCategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryClosureNodes", x => new { x.AncestorCategoryId, x.DescendantCategoryId });
                    table.ForeignKey(
                        name: "FK_CategoryClosureNodes_Categories_DescendantCategoryId",
                        column: x => x.DescendantCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryClosureNodes_DescendantCategoryId",
                table: "CategoryClosureNodes",
                column: "DescendantCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryClosureNodes");
        }
    }
}
