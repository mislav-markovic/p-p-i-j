using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InMyAppinion.Data.Migrations
{
    public partial class _11thMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "SubjectTagSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Validated",
                table: "Subject",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Professor",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Validated",
                table: "Professor",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorID = table.Column<string>(nullable: true),
                    ParentCommentID = table.Column<int>(nullable: true),
                    ParentID = table.Column<int>(nullable: true),
                    Points = table.Column<int>(nullable: false),
                    ProfessorReviewID = table.Column<int>(nullable: true),
                    SubjectReviewID = table.Column<int>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comment_User_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Comment_ParentCommentID",
                        column: x => x.ParentCommentID,
                        principalTable: "Comment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_ProfessorReview_ProfessorReviewID",
                        column: x => x.ProfessorReviewID,
                        principalTable: "ProfessorReview",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_SubjectReview_SubjectReviewID",
                        column: x => x.SubjectReviewID,
                        principalTable: "SubjectReview",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VoteProfessorReview",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProfessorReviewID = table.Column<int>(nullable: false),
                    Vote = table.Column<bool>(nullable: false),
                    VoterID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteProfessorReview", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VoteProfessorReview_ProfessorReview_ProfessorReviewID",
                        column: x => x.ProfessorReviewID,
                        principalTable: "ProfessorReview",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoteProfessorReview_User_VoterID",
                        column: x => x.VoterID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VoteSubjectReview",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SubjectReviewID = table.Column<int>(nullable: false),
                    Vote = table.Column<bool>(nullable: false),
                    VoterID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteSubjectReview", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VoteSubjectReview_SubjectReview_SubjectReviewID",
                        column: x => x.SubjectReviewID,
                        principalTable: "SubjectReview",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoteSubjectReview_User_VoterID",
                        column: x => x.VoterID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VoteComment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentID = table.Column<int>(nullable: false),
                    Vote = table.Column<bool>(nullable: false),
                    VoterID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteComment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VoteComment_Comment_CommentID",
                        column: x => x.CommentID,
                        principalTable: "Comment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoteComment_User_VoterID",
                        column: x => x.VoterID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AuthorID",
                table: "Comment",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentCommentID",
                table: "Comment",
                column: "ParentCommentID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ProfessorReviewID",
                table: "Comment",
                column: "ProfessorReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_SubjectReviewID",
                table: "Comment",
                column: "SubjectReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_VoteComment_CommentID",
                table: "VoteComment",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_VoteComment_VoterID",
                table: "VoteComment",
                column: "VoterID");

            migrationBuilder.CreateIndex(
                name: "IX_VoteProfessorReview_ProfessorReviewID",
                table: "VoteProfessorReview",
                column: "ProfessorReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_VoteProfessorReview_VoterID",
                table: "VoteProfessorReview",
                column: "VoterID");

            migrationBuilder.CreateIndex(
                name: "IX_VoteSubjectReview_SubjectReviewID",
                table: "VoteSubjectReview",
                column: "SubjectReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_VoteSubjectReview_VoterID",
                table: "VoteSubjectReview",
                column: "VoterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoteComment");

            migrationBuilder.DropTable(
                name: "VoteProfessorReview");

            migrationBuilder.DropTable(
                name: "VoteSubjectReview");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "SubjectTagSet");

            migrationBuilder.DropColumn(
                name: "Validated",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Professor");

            migrationBuilder.DropColumn(
                name: "Validated",
                table: "Professor");
        }
    }
}
