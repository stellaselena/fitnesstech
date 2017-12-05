using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FitnessTech.Migrations
{
    public partial class employeegym : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Gym_GymId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Workout_WorkoutType_WorkoutTypeId",
                table: "Workout");

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutTypeId",
                table: "Workout",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GymId",
                table: "Employee",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Gym_GymId",
                table: "Employee",
                column: "GymId",
                principalTable: "Gym",
                principalColumn: "GymId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_WorkoutType_WorkoutTypeId",
                table: "Workout",
                column: "WorkoutTypeId",
                principalTable: "WorkoutType",
                principalColumn: "WorkoutTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Gym_GymId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Workout_WorkoutType_WorkoutTypeId",
                table: "Workout");

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutTypeId",
                table: "Workout",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "GymId",
                table: "Employee",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Gym_GymId",
                table: "Employee",
                column: "GymId",
                principalTable: "Gym",
                principalColumn: "GymId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_WorkoutType_WorkoutTypeId",
                table: "Workout",
                column: "WorkoutTypeId",
                principalTable: "WorkoutType",
                principalColumn: "WorkoutTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
