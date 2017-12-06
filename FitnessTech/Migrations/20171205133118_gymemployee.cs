using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FitnessTech.Migrations
{
    public partial class gymemployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Gym_GymId",
                table: "Employee");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Gym_GymId",
                table: "Employee");

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
        }
    }
}
