using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Beverage_Buddy.Data.Migrations
{
    public partial class CreateDrinkTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DrinkName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DrinkAlternate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alcoholic = table.Column<bool>(type: "bit", nullable: false),
                    Glass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrinkThumb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageAttribution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreativeCommonsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrinkIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Measure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrinkId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrinkIngredients_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkIngredients_DrinkId",
                table: "DrinkIngredients",
                column: "DrinkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkIngredients");

            migrationBuilder.DropTable(
                name: "Drinks");
        }
    }
}
