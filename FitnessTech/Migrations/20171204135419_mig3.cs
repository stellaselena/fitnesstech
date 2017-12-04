using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FitnessTech.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutAssigment_Workout_WorkoutId",
                table: "WorkoutAssigment");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutAssigment_WorkoutProgram_WorkoutProgramId",
                table: "WorkoutAssigment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutAssigment",
                table: "WorkoutAssigment");

            migrationBuilder.RenameTable(
                name: "WorkoutAssigment",
                newName: "WorkoutAssigments");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutAssigment_WorkoutProgramId",
                table: "WorkoutAssigments",
                newName: "IX_WorkoutAssigments_WorkoutProgramId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutAssigments",
                table: "WorkoutAssigments",
                columns: new[] { "WorkoutId", "WorkoutProgramId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutAssigments_Workout_WorkoutId",
                table: "WorkoutAssigments",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "WorkoutId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutAssigments_WorkoutProgram_WorkoutProgramId",
                table: "WorkoutAssigments",
                column: "WorkoutProgramId",
                principalTable: "WorkoutProgram",
                principalColumn: "WorkoutProgramId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutAssigments_Workout_WorkoutId",
                table: "WorkoutAssigments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutAssigments_WorkoutProgram_WorkoutProgramId",
                table: "WorkoutAssigments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutAssigments",
                table: "WorkoutAssigments");

            migrationBuilder.RenameTable(
                name: "WorkoutAssigments",
                newName: "WorkoutAssigment");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutAssigments_WorkoutProgramId",
                table: "WorkoutAssigment",
                newName: "IX_WorkoutAssigment_WorkoutProgramId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutAssigment",
                table: "WorkoutAssigment",
                columns: new[] { "WorkoutId", "WorkoutProgramId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutAssigment_Workout_WorkoutId",
                table: "WorkoutAssigment",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "WorkoutId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutAssigment_WorkoutProgram_WorkoutProgramId",
                table: "WorkoutAssigment",
                column: "WorkoutProgramId",
                principalTable: "WorkoutProgram",
                principalColumn: "WorkoutProgramId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
