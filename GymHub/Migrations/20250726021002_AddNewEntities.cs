using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymHub.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassBookings_AspNetUsers_UserId",
                table: "ClassBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassBookings_GymClasses_GymClassId",
                table: "ClassBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressMetrics_AspNetUsers_UserId",
                table: "ProgressMetrics");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSessions_AspNetUsers_UserId",
                table: "WorkoutSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSets_Exercises_ExerciseId",
                table: "WorkoutSets");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSets_WorkoutSessions_WorkoutSessionId",
                table: "WorkoutSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutSets",
                table: "WorkoutSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutSessions",
                table: "WorkoutSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgressMetrics",
                table: "ProgressMetrics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassBookings",
                table: "ClassBookings");

            migrationBuilder.RenameTable(
                name: "WorkoutSets",
                newName: "WorkoutSet");

            migrationBuilder.RenameTable(
                name: "WorkoutSessions",
                newName: "WorkoutSession");

            migrationBuilder.RenameTable(
                name: "ProgressMetrics",
                newName: "ProgressMetric");

            migrationBuilder.RenameTable(
                name: "ClassBookings",
                newName: "ClassBooking");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutSets_WorkoutSessionId",
                table: "WorkoutSet",
                newName: "IX_WorkoutSet_WorkoutSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutSets_ExerciseId",
                table: "WorkoutSet",
                newName: "IX_WorkoutSet_ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutSessions_UserId",
                table: "WorkoutSession",
                newName: "IX_WorkoutSession_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgressMetrics_UserId",
                table: "ProgressMetric",
                newName: "IX_ProgressMetric_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassBookings_UserId",
                table: "ClassBooking",
                newName: "IX_ClassBooking_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassBookings_GymClassId",
                table: "ClassBooking",
                newName: "IX_ClassBooking_GymClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutSet",
                table: "WorkoutSet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutSession",
                table: "WorkoutSession",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgressMetric",
                table: "ProgressMetric",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassBooking",
                table: "ClassBooking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassBooking_AspNetUsers_UserId",
                table: "ClassBooking",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassBooking_GymClasses_GymClassId",
                table: "ClassBooking",
                column: "GymClassId",
                principalTable: "GymClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressMetric_AspNetUsers_UserId",
                table: "ProgressMetric",
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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSet_WorkoutSession_WorkoutSessionId",
                table: "WorkoutSet",
                column: "WorkoutSessionId",
                principalTable: "WorkoutSession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassBooking_AspNetUsers_UserId",
                table: "ClassBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassBooking_GymClasses_GymClassId",
                table: "ClassBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressMetric_AspNetUsers_UserId",
                table: "ProgressMetric");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSession_AspNetUsers_UserId",
                table: "WorkoutSession");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSet_Exercises_ExerciseId",
                table: "WorkoutSet");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSet_WorkoutSession_WorkoutSessionId",
                table: "WorkoutSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutSet",
                table: "WorkoutSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutSession",
                table: "WorkoutSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgressMetric",
                table: "ProgressMetric");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassBooking",
                table: "ClassBooking");

            migrationBuilder.RenameTable(
                name: "WorkoutSet",
                newName: "WorkoutSets");

            migrationBuilder.RenameTable(
                name: "WorkoutSession",
                newName: "WorkoutSessions");

            migrationBuilder.RenameTable(
                name: "ProgressMetric",
                newName: "ProgressMetrics");

            migrationBuilder.RenameTable(
                name: "ClassBooking",
                newName: "ClassBookings");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutSet_WorkoutSessionId",
                table: "WorkoutSets",
                newName: "IX_WorkoutSets_WorkoutSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutSet_ExerciseId",
                table: "WorkoutSets",
                newName: "IX_WorkoutSets_ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutSession_UserId",
                table: "WorkoutSessions",
                newName: "IX_WorkoutSessions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgressMetric_UserId",
                table: "ProgressMetrics",
                newName: "IX_ProgressMetrics_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassBooking_UserId",
                table: "ClassBookings",
                newName: "IX_ClassBookings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassBooking_GymClassId",
                table: "ClassBookings",
                newName: "IX_ClassBookings_GymClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutSets",
                table: "WorkoutSets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutSessions",
                table: "WorkoutSessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgressMetrics",
                table: "ProgressMetrics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassBookings",
                table: "ClassBookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassBookings_AspNetUsers_UserId",
                table: "ClassBookings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassBookings_GymClasses_GymClassId",
                table: "ClassBookings",
                column: "GymClassId",
                principalTable: "GymClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressMetrics_AspNetUsers_UserId",
                table: "ProgressMetrics",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSessions_AspNetUsers_UserId",
                table: "WorkoutSessions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSets_Exercises_ExerciseId",
                table: "WorkoutSets",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSets_WorkoutSessions_WorkoutSessionId",
                table: "WorkoutSets",
                column: "WorkoutSessionId",
                principalTable: "WorkoutSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
