using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymHub.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionPlans_AspNetUsers_UserId",
                table: "NutritionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_AspNetUsers_UserId",
                table: "WorkoutPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSession_AspNetUsers_UserId",
                table: "WorkoutSession");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSet_Exercises_ExerciseId",
                table: "WorkoutSet");

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionPlans_AspNetUsers_UserId",
                table: "NutritionPlans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_AspNetUsers_UserId",
                table: "WorkoutPlans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSession_AspNetUsers_UserId",
                table: "WorkoutSession",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSet_Exercises_ExerciseId",
                table: "WorkoutSet",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionPlans_AspNetUsers_UserId",
                table: "NutritionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_AspNetUsers_UserId",
                table: "WorkoutPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSession_AspNetUsers_UserId",
                table: "WorkoutSession");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSet_Exercises_ExerciseId",
                table: "WorkoutSet");

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionPlans_AspNetUsers_UserId",
                table: "NutritionPlans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_AspNetUsers_UserId",
                table: "WorkoutPlans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSession_AspNetUsers_UserId",
                table: "WorkoutSession",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSet_Exercises_ExerciseId",
                table: "WorkoutSet",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
