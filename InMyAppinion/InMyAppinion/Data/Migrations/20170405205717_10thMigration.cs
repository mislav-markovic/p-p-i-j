using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InMyAppinion.Data.Migrations
{
    public partial class _10thMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTagSet",
                table: "SubjectTagSet");

            migrationBuilder.DropIndex(
                name: "IX_SubjectTagSet_SubjectID",
                table: "SubjectTagSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorSubjectSet",
                table: "ProfessorSubjectSet");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorSubjectSet_ProfessorID",
                table: "ProfessorSubjectSet");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "SubjectTagSet");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ProfessorSubjectSet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTagSet",
                table: "SubjectTagSet",
                columns: new[] { "SubjectID", "SubjectTagID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorSubjectSet",
                table: "ProfessorSubjectSet",
                columns: new[] { "ProfessorID", "SubjectID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTagSet",
                table: "SubjectTagSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorSubjectSet",
                table: "ProfessorSubjectSet");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "SubjectTagSet",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "ProfessorSubjectSet",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTagSet",
                table: "SubjectTagSet",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorSubjectSet",
                table: "ProfessorSubjectSet",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTagSet_SubjectID",
                table: "SubjectTagSet",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubjectSet_ProfessorID",
                table: "ProfessorSubjectSet",
                column: "ProfessorID");
        }
    }
}
