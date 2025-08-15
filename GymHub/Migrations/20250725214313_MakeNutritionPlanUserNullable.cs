using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymHub.Migrations
{
    /// <inheritdoc />
    public partial class MakeNutritionPlanUserNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionPlans_AspNetUsers_UserId",
                table: "NutritionPlans");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "NutritionPlans",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionPlans_AspNetUsers_UserId",
                table: "NutritionPlans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionPlans_AspNetUsers_UserId",
                table: "NutritionPlans");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "NutritionPlans",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionPlans_AspNetUsers_UserId",
                table: "NutritionPlans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
