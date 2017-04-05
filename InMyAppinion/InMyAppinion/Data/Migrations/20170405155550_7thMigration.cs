using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InMyAppinion.Data.Migrations
{
    public partial class _7thMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MentorGrade",
                table: "ProfessorReview",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "HelpfulnessGrade",
                table: "ProfessorReview",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HelpfulnessGrade",
                table: "ProfessorReview");

            migrationBuilder.AlterColumn<int>(
                name: "MentorGrade",
                table: "ProfessorReview",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
