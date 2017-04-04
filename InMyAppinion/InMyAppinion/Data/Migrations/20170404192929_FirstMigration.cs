using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InMyAppinion.Data.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculty", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.ID);
                });

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
                name: "SubjectTag",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTag", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Subeject",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FacultyID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subeject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Subeject_Faculty_FacultyID",
                        column: x => x.FacultyID,
                        principalTable: "Faculty",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorReview",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorID = table.Column<string>(nullable: true),
                    InteractionGrade = table.Column<int>(nullable: false),
                    MentorGrade = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    ProfessorID = table.Column<int>(nullable: false),
                    QualityGrade = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    TotalGrade = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorReview", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProfessorReview_User_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfessorReview_Professor_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorSubjectSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProfessorID = table.Column<int>(nullable: false),
                    SubjectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorSubjectSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProfessorSubjectSet_Professor_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorSubjectSet_Subeject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subeject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectReview",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorID = table.Column<string>(nullable: true),
                    DifficultyGrade = table.Column<int>(nullable: false),
                    InterestGrade = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    SubjectID = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    TotalGrade = table.Column<decimal>(nullable: false),
                    UsefulnessGrade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectReview", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubjectReview_User_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectReview_Subeject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subeject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "SubjectTagSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SubjectReviewID = table.Column<int>(nullable: false),
                    SubjectTagID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTagSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubjectTagSet_SubjectReview_SubjectReviewID",
                        column: x => x.SubjectReviewID,
                        principalTable: "SubjectReview",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectTagSet_SubjectTag_SubjectTagID",
                        column: x => x.SubjectTagID,
                        principalTable: "SubjectTag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReview_AuthorID",
                table: "ProfessorReview",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReview_ProfessorID",
                table: "ProfessorReview",
                column: "ProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubjectSet_ProfessorID",
                table: "ProfessorSubjectSet",
                column: "ProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubjectSet_SubjectID",
                table: "ProfessorSubjectSet",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorTagSet_ProfessorReviewID",
                table: "ProfessorTagSet",
                column: "ProfessorReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorTagSet_ProfessorTagID",
                table: "ProfessorTagSet",
                column: "ProfessorTagID");

            migrationBuilder.CreateIndex(
                name: "IX_Subeject_FacultyID",
                table: "Subeject",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectReview_AuthorID",
                table: "SubjectReview",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectReview_SubjectID",
                table: "SubjectReview",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTagSet_SubjectReviewID",
                table: "SubjectTagSet",
                column: "SubjectReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTagSet_SubjectTagID",
                table: "SubjectTagSet",
                column: "SubjectTagID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfessorSubjectSet");

            migrationBuilder.DropTable(
                name: "ProfessorTagSet");

            migrationBuilder.DropTable(
                name: "SubjectTagSet");

            migrationBuilder.DropTable(
                name: "ProfessorReview");

            migrationBuilder.DropTable(
                name: "ProfessorTag");

            migrationBuilder.DropTable(
                name: "SubjectReview");

            migrationBuilder.DropTable(
                name: "SubjectTag");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "Subeject");

            migrationBuilder.DropTable(
                name: "Faculty");

            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "User");
        }
    }
}
