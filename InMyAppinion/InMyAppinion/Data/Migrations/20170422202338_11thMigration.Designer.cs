using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using InMyAppinion.Data;

namespace InMyAppinion.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170422202338_11thMigration")]
    partial class _11thMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InMyAppinion.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsBanned");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("Points");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("User");
                });

            modelBuilder.Entity("InMyAppinion.Models.City", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("ZipCode");

                    b.HasKey("ID");

                    b.ToTable("City");
                });

            modelBuilder.Entity("InMyAppinion.Models.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorID");

                    b.Property<int?>("ParentCommentID");

                    b.Property<int?>("ParentID");

                    b.Property<int>("Points");

                    b.Property<int?>("ProfessorReviewID");

                    b.Property<int?>("SubjectReviewID");

                    b.Property<string>("Text");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.HasIndex("ParentCommentID");

                    b.HasIndex("ProfessorReviewID");

                    b.HasIndex("SubjectReviewID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("InMyAppinion.Models.Faculty", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("ShortName");

                    b.Property<int?>("UniversityID");

                    b.HasKey("ID");

                    b.HasIndex("UniversityID");

                    b.ToTable("Faculty");
                });

            modelBuilder.Entity("InMyAppinion.Models.Professor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Biography");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("Validated");

                    b.HasKey("ID");

                    b.ToTable("Professor");
                });

            modelBuilder.Entity("InMyAppinion.Models.ProfessorReview", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorID");

                    b.Property<int>("HelpfulnessGrade");

                    b.Property<int>("InteractionGrade");

                    b.Property<int?>("MentorGrade");

                    b.Property<int>("Points");

                    b.Property<int>("ProfessorID");

                    b.Property<int>("QualityGrade");

                    b.Property<string>("Text");

                    b.Property<DateTime>("Timestamp");

                    b.Property<decimal>("TotalGrade");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.HasIndex("ProfessorID");

                    b.ToTable("ProfessorReview");
                });

            modelBuilder.Entity("InMyAppinion.Models.ProfessorReviewTag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("ProfessorReviewTag");
                });

            modelBuilder.Entity("InMyAppinion.Models.ProfessorReviewTagSet", b =>
                {
                    b.Property<int>("ProfessorReviewID");

                    b.Property<int>("ProfessorReviewTagID");

                    b.HasKey("ProfessorReviewID", "ProfessorReviewTagID");

                    b.HasIndex("ProfessorReviewTagID");

                    b.ToTable("ProfessorReviewTagSet");
                });

            modelBuilder.Entity("InMyAppinion.Models.ProfessorSubjectSet", b =>
                {
                    b.Property<int>("ProfessorID");

                    b.Property<int>("SubjectID");

                    b.HasKey("ProfessorID", "SubjectID");

                    b.HasIndex("SubjectID");

                    b.ToTable("ProfessorSubjectSet");
                });

            modelBuilder.Entity("InMyAppinion.Models.Subject", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("FacultyID");

                    b.Property<string>("Name");

                    b.Property<string>("ShortName");

                    b.Property<bool>("Validated");

                    b.HasKey("ID");

                    b.HasIndex("FacultyID");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("InMyAppinion.Models.SubjectReview", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorID");

                    b.Property<int>("DifficultyGrade");

                    b.Property<int>("InterestGrade");

                    b.Property<int>("Points");

                    b.Property<int>("SubjectID");

                    b.Property<string>("Text");

                    b.Property<DateTime>("Timestamp");

                    b.Property<decimal>("TotalGrade");

                    b.Property<int>("UsefulnessGrade");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.HasIndex("SubjectID");

                    b.ToTable("SubjectReview");
                });

            modelBuilder.Entity("InMyAppinion.Models.SubjectReviewTag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("SubjectReviewTag");
                });

            modelBuilder.Entity("InMyAppinion.Models.SubjectReviewTagSet", b =>
                {
                    b.Property<int>("SubjectReviewID");

                    b.Property<int>("SubjectReviewTagID");

                    b.HasKey("SubjectReviewID", "SubjectReviewTagID");

                    b.HasIndex("SubjectReviewTagID");

                    b.ToTable("SubjectReviewTagSet");
                });

            modelBuilder.Entity("InMyAppinion.Models.SubjectTag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("SubjectTag");
                });

            modelBuilder.Entity("InMyAppinion.Models.SubjectTagSet", b =>
                {
                    b.Property<int>("SubjectID");

                    b.Property<int>("SubjectTagID");

                    b.Property<int>("ID");

                    b.HasKey("SubjectID", "SubjectTagID");

                    b.HasIndex("SubjectTagID");

                    b.ToTable("SubjectTagSet");
                });

            modelBuilder.Entity("InMyAppinion.Models.University", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityID");

                    b.Property<string>("Name");

                    b.Property<string>("ShortName");

                    b.HasKey("ID");

                    b.HasIndex("CityID");

                    b.ToTable("University");
                });

            modelBuilder.Entity("InMyAppinion.Models.VoteComment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommentID");

                    b.Property<bool>("Vote");

                    b.Property<string>("VoterID");

                    b.HasKey("ID");

                    b.HasIndex("CommentID");

                    b.HasIndex("VoterID");

                    b.ToTable("VoteComment");
                });

            modelBuilder.Entity("InMyAppinion.Models.VoteProfessorReview", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProfessorReviewID");

                    b.Property<bool>("Vote");

                    b.Property<string>("VoterID");

                    b.HasKey("ID");

                    b.HasIndex("ProfessorReviewID");

                    b.HasIndex("VoterID");

                    b.ToTable("VoteProfessorReview");
                });

            modelBuilder.Entity("InMyAppinion.Models.VoteSubjectReview", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SubjectReviewID");

                    b.Property<bool>("Vote");

                    b.Property<string>("VoterID");

                    b.HasKey("ID");

                    b.HasIndex("SubjectReviewID");

                    b.HasIndex("VoterID");

                    b.ToTable("VoteSubjectReview");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("Role");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("InMyAppinion.Models.ApplicationRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole");

                    b.Property<string>("Description");

                    b.ToTable("ApplicationRole");

                    b.HasDiscriminator().HasValue("ApplicationRole");
                });

            modelBuilder.Entity("InMyAppinion.Models.Comment", b =>
                {
                    b.HasOne("InMyAppinion.Models.ApplicationUser", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorID");

                    b.HasOne("InMyAppinion.Models.Comment", "ParentComment")
                        .WithMany("ChildComments")
                        .HasForeignKey("ParentCommentID");

                    b.HasOne("InMyAppinion.Models.ProfessorReview", "ProfessorReview")
                        .WithMany("Comments")
                        .HasForeignKey("ProfessorReviewID");

                    b.HasOne("InMyAppinion.Models.SubjectReview", "SubjectReview")
                        .WithMany("Comments")
                        .HasForeignKey("SubjectReviewID");
                });

            modelBuilder.Entity("InMyAppinion.Models.Faculty", b =>
                {
                    b.HasOne("InMyAppinion.Models.University", "University")
                        .WithMany("Faculties")
                        .HasForeignKey("UniversityID");
                });

            modelBuilder.Entity("InMyAppinion.Models.ProfessorReview", b =>
                {
                    b.HasOne("InMyAppinion.Models.ApplicationUser", "Author")
                        .WithMany("ProfessorReviews")
                        .HasForeignKey("AuthorID");

                    b.HasOne("InMyAppinion.Models.Professor", "Professor")
                        .WithMany("Reviews")
                        .HasForeignKey("ProfessorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InMyAppinion.Models.ProfessorReviewTagSet", b =>
                {
                    b.HasOne("InMyAppinion.Models.ProfessorReview", "ProfessorReview")
                        .WithMany("ProfessorReviewTagSet")
                        .HasForeignKey("ProfessorReviewID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InMyAppinion.Models.ProfessorReviewTag", "ProfessorReviewTag")
                        .WithMany("ProfessorReviewTagSet")
                        .HasForeignKey("ProfessorReviewTagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InMyAppinion.Models.ProfessorSubjectSet", b =>
                {
                    b.HasOne("InMyAppinion.Models.Professor", "Professor")
                        .WithMany("Subjects")
                        .HasForeignKey("ProfessorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InMyAppinion.Models.Subject", "Subject")
                        .WithMany("Professors")
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InMyAppinion.Models.Subject", b =>
                {
                    b.HasOne("InMyAppinion.Models.Faculty", "Faculty")
                        .WithMany("Subjects")
                        .HasForeignKey("FacultyID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InMyAppinion.Models.SubjectReview", b =>
                {
                    b.HasOne("InMyAppinion.Models.ApplicationUser", "Author")
                        .WithMany("SubjectReviews")
                        .HasForeignKey("AuthorID");

                    b.HasOne("InMyAppinion.Models.Subject", "Subject")
                        .WithMany("Reviews")
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InMyAppinion.Models.SubjectReviewTagSet", b =>
                {
                    b.HasOne("InMyAppinion.Models.SubjectReview", "SubjectReview")
                        .WithMany("SubjectReviewTagSet")
                        .HasForeignKey("SubjectReviewID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InMyAppinion.Models.SubjectReviewTag", "SubjectReviewTag")
                        .WithMany("SubjectReviewTagSet")
                        .HasForeignKey("SubjectReviewTagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InMyAppinion.Models.SubjectTagSet", b =>
                {
                    b.HasOne("InMyAppinion.Models.Subject", "Subject")
                        .WithMany("SubjectTagSet")
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InMyAppinion.Models.SubjectTag", "SubjectTag")
                        .WithMany("SubjectTagSet")
                        .HasForeignKey("SubjectTagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InMyAppinion.Models.University", b =>
                {
                    b.HasOne("InMyAppinion.Models.City", "City")
                        .WithMany("Universities")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InMyAppinion.Models.VoteComment", b =>
                {
                    b.HasOne("InMyAppinion.Models.Comment", "Comment")
                        .WithMany("Votes")
                        .HasForeignKey("CommentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InMyAppinion.Models.ApplicationUser", "Voter")
                        .WithMany("VotedComments")
                        .HasForeignKey("VoterID");
                });

            modelBuilder.Entity("InMyAppinion.Models.VoteProfessorReview", b =>
                {
                    b.HasOne("InMyAppinion.Models.ProfessorReview", "ProfessorReview")
                        .WithMany("Votes")
                        .HasForeignKey("ProfessorReviewID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InMyAppinion.Models.ApplicationUser", "Voter")
                        .WithMany("VotedProfessorReview")
                        .HasForeignKey("VoterID");
                });

            modelBuilder.Entity("InMyAppinion.Models.VoteSubjectReview", b =>
                {
                    b.HasOne("InMyAppinion.Models.SubjectReview", "SubjectReview")
                        .WithMany("Votes")
                        .HasForeignKey("SubjectReviewID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InMyAppinion.Models.ApplicationUser", "Voter")
                        .WithMany("VotedSubjectReview")
                        .HasForeignKey("VoterID");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("InMyAppinion.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("InMyAppinion.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InMyAppinion.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
