using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InMyAppinion.Data.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_University_UniversityID",
                table: "Faculty");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityID",
                table: "Faculty",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_University_UniversityID",
                table: "Faculty",
                column: "UniversityID",
                principalTable: "University",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_University_UniversityID",
                table: "Faculty");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityID",
                table: "Faculty",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_University_UniversityID",
                table: "Faculty",
                column: "UniversityID",
                principalTable: "University",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
