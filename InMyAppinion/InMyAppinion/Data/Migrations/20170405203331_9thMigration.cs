using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InMyAppinion.Data.Migrations
{
    public partial class _9thMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectReviewTagSet",
                table: "SubjectReviewTagSet");

            migrationBuilder.DropIndex(
                name: "IX_SubjectReviewTagSet_SubjectReviewID",
                table: "SubjectReviewTagSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorReviewTagSet",
                table: "ProfessorReviewTagSet");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorReviewTagSet_ProfessorReviewID",
                table: "ProfessorReviewTagSet");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "SubjectReviewTagSet");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ProfessorReviewTagSet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectReviewTagSet",
                table: "SubjectReviewTagSet",
                columns: new[] { "SubjectReviewID", "SubjectReviewTagID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorReviewTagSet",
                table: "ProfessorReviewTagSet",
                columns: new[] { "ProfessorReviewID", "ProfessorReviewTagID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectReviewTagSet",
                table: "SubjectReviewTagSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorReviewTagSet",
                table: "ProfessorReviewTagSet");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "SubjectReviewTagSet",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "ProfessorReviewTagSet",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectReviewTagSet",
                table: "SubjectReviewTagSet",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorReviewTagSet",
                table: "ProfessorReviewTagSet",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectReviewTagSet_SubjectReviewID",
                table: "SubjectReviewTagSet",
                column: "SubjectReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviewTagSet_ProfessorReviewID",
                table: "ProfessorReviewTagSet",
                column: "ProfessorReviewID");
        }
    }
}
