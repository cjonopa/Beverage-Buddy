using Microsoft.EntityFrameworkCore.Migrations;

namespace Beverage_Buddy.Data.Migrations
{
    public partial class UpdatesRecipeAndDrinkTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecipeThumb",
                table: "Recipes",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipeThumb",
                table: "Recipes");
        }
    }
}
