using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InMyAppinion.Data.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorSubjectSet_Subeject_SubjectID",
                table: "ProfessorSubjectSet");

            migrationBuilder.DropForeignKey(
                name: "FK_Subeject_Faculty_FacultyID",
                table: "Subeject");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectReview_Subeject_SubjectID",
                table: "SubjectReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subeject",
                table: "Subeject");

            migrationBuilder.RenameTable(
                name: "Subeject",
                newName: "Subject");

            migrationBuilder.RenameIndex(
                name: "IX_Subeject_FacultyID",
                table: "Subject",
                newName: "IX_Subject_FacultyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject",
                table: "Subject",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorSubjectSet_Subject_SubjectID",
                table: "ProfessorSubjectSet",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Faculty_FacultyID",
                table: "Subject",
                column: "FacultyID",
                principalTable: "Faculty",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectReview_Subject_SubjectID",
                table: "SubjectReview",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorSubjectSet_Subject_SubjectID",
                table: "ProfessorSubjectSet");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Faculty_FacultyID",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectReview_Subject_SubjectID",
                table: "SubjectReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                table: "Subject");

            migrationBuilder.RenameTable(
                name: "Subject",
                newName: "Subeject");

            migrationBuilder.RenameIndex(
                name: "IX_Subject_FacultyID",
                table: "Subeject",
                newName: "IX_Subeject_FacultyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subeject",
                table: "Subeject",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorSubjectSet_Subeject_SubjectID",
                table: "ProfessorSubjectSet",
                column: "SubjectID",
                principalTable: "Subeject",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subeject_Faculty_FacultyID",
                table: "Subeject",
                column: "FacultyID",
                principalTable: "Faculty",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectReview_Subeject_SubjectID",
                table: "SubjectReview",
                column: "SubjectID",
                principalTable: "Subeject",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
