using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FitnessTech.Migrations
{
    public partial class exerciseEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Equipment",
                table: "Exercise",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Exercise",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mechanics",
                table: "Exercise",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Equipment",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "Mechanics",
                table: "Exercise");
        }
    }
}
