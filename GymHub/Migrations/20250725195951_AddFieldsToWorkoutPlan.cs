using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymHub.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToWorkoutPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DifficultyLevel",
                table: "WorkoutPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DurationWeeks",
                table: "WorkoutPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Goal",
                table: "WorkoutPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DifficultyLevel",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "DurationWeeks",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "Goal",
                table: "WorkoutPlans");
        }
    }
}
