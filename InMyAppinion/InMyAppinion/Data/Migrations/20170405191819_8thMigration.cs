using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InMyAppinion.Data.Migrations
{
    public partial class _8thMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorReviewTagSet_ProfessorReviewTag_ProfessorReviewTagID",
                table: "ProfessorReviewTagSet");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectReviewTagSet_SubjectReviewTag_SubjectReviewTagID",
                table: "SubjectReviewTagSet");

            migrationBuilder.DropColumn(
                name: "SubjectTagID",
                table: "SubjectReviewTagSet");

            migrationBuilder.DropColumn(
                name: "ProfessorTagID",
                table: "ProfessorReviewTagSet");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectReviewTagID",
                table: "SubjectReviewTagSet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProfessorReviewTagID",
                table: "ProfessorReviewTagSet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorReviewTagSet_ProfessorReviewTag_ProfessorReviewTagID",
                table: "ProfessorReviewTagSet",
                column: "ProfessorReviewTagID",
                principalTable: "ProfessorReviewTag",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectReviewTagSet_SubjectReviewTag_SubjectReviewTagID",
                table: "SubjectReviewTagSet",
                column: "SubjectReviewTagID",
                principalTable: "SubjectReviewTag",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorReviewTagSet_ProfessorReviewTag_ProfessorReviewTagID",
                table: "ProfessorReviewTagSet");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectReviewTagSet_SubjectReviewTag_SubjectReviewTagID",
                table: "SubjectReviewTagSet");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectReviewTagID",
                table: "SubjectReviewTagSet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "SubjectTagID",
                table: "SubjectReviewTagSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProfessorReviewTagID",
                table: "ProfessorReviewTagSet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProfessorTagID",
                table: "ProfessorReviewTagSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorReviewTagSet_ProfessorReviewTag_ProfessorReviewTagID",
                table: "ProfessorReviewTagSet",
                column: "ProfessorReviewTagID",
                principalTable: "ProfessorReviewTag",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectReviewTagSet_SubjectReviewTag_SubjectReviewTagID",
                table: "SubjectReviewTagSet",
                column: "SubjectReviewTagID",
                principalTable: "SubjectReviewTag",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
