using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InMyAppinion.Data.Migrations
{
    public partial class _6thMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorReviewTagSet_ProfessorReviewTag_ProfessorTagID",
                table: "ProfessorReviewTagSet");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectReviewTagSet_SubjectReviewTag_SubjectTagID",
                table: "SubjectReviewTagSet");

            migrationBuilder.DropIndex(
                name: "IX_SubjectReviewTagSet_SubjectTagID",
                table: "SubjectReviewTagSet");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorReviewTagSet_ProfessorTagID",
                table: "ProfessorReviewTagSet");

            migrationBuilder.AddColumn<int>(
                name: "SubjectReviewTagID",
                table: "SubjectReviewTagSet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfessorReviewTagID",
                table: "ProfessorReviewTagSet",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectReviewTagSet_SubjectReviewTagID",
                table: "SubjectReviewTagSet",
                column: "SubjectReviewTagID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviewTagSet_ProfessorReviewTagID",
                table: "ProfessorReviewTagSet",
                column: "ProfessorReviewTagID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorReviewTagSet_ProfessorReviewTag_ProfessorReviewTagID",
                table: "ProfessorReviewTagSet");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectReviewTagSet_SubjectReviewTag_SubjectReviewTagID",
                table: "SubjectReviewTagSet");

            migrationBuilder.DropIndex(
                name: "IX_SubjectReviewTagSet_SubjectReviewTagID",
                table: "SubjectReviewTagSet");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorReviewTagSet_ProfessorReviewTagID",
                table: "ProfessorReviewTagSet");

            migrationBuilder.DropColumn(
                name: "SubjectReviewTagID",
                table: "SubjectReviewTagSet");

            migrationBuilder.DropColumn(
                name: "ProfessorReviewTagID",
                table: "ProfessorReviewTagSet");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectReviewTagSet_SubjectTagID",
                table: "SubjectReviewTagSet",
                column: "SubjectTagID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviewTagSet_ProfessorTagID",
                table: "ProfessorReviewTagSet",
                column: "ProfessorTagID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorReviewTagSet_ProfessorReviewTag_ProfessorTagID",
                table: "ProfessorReviewTagSet",
                column: "ProfessorTagID",
                principalTable: "ProfessorReviewTag",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectReviewTagSet_SubjectReviewTag_SubjectTagID",
                table: "SubjectReviewTagSet",
                column: "SubjectTagID",
                principalTable: "SubjectReviewTag",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
