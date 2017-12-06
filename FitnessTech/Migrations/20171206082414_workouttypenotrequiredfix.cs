using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FitnessTech.Migrations
{
    public partial class workouttypenotrequiredfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workout_WorkoutType_WorkoutTypeId",
                table: "Workout");

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutTypeId",
                table: "Workout",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_WorkoutType_WorkoutTypeId",
                table: "Workout",
                column: "WorkoutTypeId",
                principalTable: "WorkoutType",
                principalColumn: "WorkoutTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workout_WorkoutType_WorkoutTypeId",
                table: "Workout");

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutTypeId",
                table: "Workout",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_WorkoutType_WorkoutTypeId",
                table: "Workout",
                column: "WorkoutTypeId",
                principalTable: "WorkoutType",
                principalColumn: "WorkoutTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
