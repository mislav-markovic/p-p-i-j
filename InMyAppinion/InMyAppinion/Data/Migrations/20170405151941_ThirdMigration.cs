using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InMyAppinion.Data.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTagSet_SubjectReview_SubjectReviewID",
                table: "SubjectTagSet");

            migrationBuilder.DropTable(
                name: "ProfessorTagSet");

            migrationBuilder.DropTable(
                name: "ProfessorTag");

            migrationBuilder.RenameColumn(
                name: "SubjectReviewID",
                table: "SubjectTagSet",
                newName: "SubjectID");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectTagSet_SubjectReviewID",
                table: "SubjectTagSet",
                newName: "IX_SubjectTagSet_SubjectID");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Subject",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Subject",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Faculty",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityID",
                table: "Faculty",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ZipCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorReviewTag",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorReviewTag", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SubjectReviewTag",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectReviewTag", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "University",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_University", x => x.ID);
                    table.ForeignKey(
                        name: "FK_University_City_CityID",
                        column: x => x.CityID,
                        principalTable: "City",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorReviewTagSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProfessorReviewID = table.Column<int>(nullable: false),
                    ProfessorTagID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorReviewTagSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProfessorReviewTagSet_ProfessorReview_ProfessorReviewID",
                        column: x => x.ProfessorReviewID,
                        principalTable: "ProfessorReview",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorReviewTagSet_ProfessorReviewTag_ProfessorTagID",
                        column: x => x.ProfessorTagID,
                        principalTable: "ProfessorReviewTag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectReviewTagSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SubjectReviewID = table.Column<int>(nullable: false),
                    SubjectTagID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectReviewTagSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubjectReviewTagSet_SubjectReview_SubjectReviewID",
                        column: x => x.SubjectReviewID,
                        principalTable: "SubjectReview",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectReviewTagSet_SubjectReviewTag_SubjectTagID",
                        column: x => x.SubjectTagID,
                        principalTable: "SubjectReviewTag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faculty_UniversityID",
                table: "Faculty",
                column: "UniversityID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviewTagSet_ProfessorReviewID",
                table: "ProfessorReviewTagSet",
                column: "ProfessorReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviewTagSet_ProfessorTagID",
                table: "ProfessorReviewTagSet",
                column: "ProfessorTagID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectReviewTagSet_SubjectReviewID",
                table: "SubjectReviewTagSet",
                column: "SubjectReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectReviewTagSet_SubjectTagID",
                table: "SubjectReviewTagSet",
                column: "SubjectTagID");

            migrationBuilder.CreateIndex(
                name: "IX_University_CityID",
                table: "University",
                column: "CityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_University_UniversityID",
                table: "Faculty",
                column: "UniversityID",
                principalTable: "University",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTagSet_Subject_SubjectID",
                table: "SubjectTagSet",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_University_UniversityID",
                table: "Faculty");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTagSet_Subject_SubjectID",
                table: "SubjectTagSet");

            migrationBuilder.DropTable(
                name: "ProfessorReviewTagSet");

            migrationBuilder.DropTable(
                name: "SubjectReviewTagSet");

            migrationBuilder.DropTable(
                name: "University");

            migrationBuilder.DropTable(
                name: "ProfessorReviewTag");

            migrationBuilder.DropTable(
                name: "SubjectReviewTag");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropIndex(
                name: "IX_Faculty_UniversityID",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "UniversityID",
                table: "Faculty");

            migrationBuilder.RenameColumn(
                name: "SubjectID",
                table: "SubjectTagSet",
                newName: "SubjectReviewID");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectTagSet_SubjectID",
                table: "SubjectTagSet",
                newName: "IX_SubjectTagSet_SubjectReviewID");

            migrationBuilder.CreateTable(
                name: "ProfessorTag",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorTag", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorTagSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProfessorReviewID = table.Column<int>(nullable: false),
                    ProfessorTagID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorTagSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProfessorTagSet_ProfessorReview_ProfessorReviewID",
                        column: x => x.ProfessorReviewID,
                        principalTable: "ProfessorReview",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorTagSet_ProfessorTag_ProfessorTagID",
                        column: x => x.ProfessorTagID,
                        principalTable: "ProfessorTag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorTagSet_ProfessorReviewID",
                table: "ProfessorTagSet",
                column: "ProfessorReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorTagSet_ProfessorTagID",
                table: "ProfessorTagSet",
                column: "ProfessorTagID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTagSet_SubjectReview_SubjectReviewID",
                table: "SubjectTagSet",
                column: "SubjectReviewID",
                principalTable: "SubjectReview",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
