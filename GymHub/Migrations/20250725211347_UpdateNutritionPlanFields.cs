using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNutritionPlanFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarbsGrams",
                table: "NutritionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FatsGrams",
                table: "NutritionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProteinGrams",
                table: "NutritionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalCaloriesPerDay",
                table: "NutritionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarbsGrams",
                table: "NutritionPlans");

            migrationBuilder.DropColumn(
                name: "FatsGrams",
                table: "NutritionPlans");

            migrationBuilder.DropColumn(
                name: "ProteinGrams",
                table: "NutritionPlans");

            migrationBuilder.DropColumn(
                name: "TotalCaloriesPerDay",
                table: "NutritionPlans");
        }
    }
}
