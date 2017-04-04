using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using InMyAppinion.Data;

namespace InMyAppinion.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170404193351_SecondMigration")]
    partial class SecondMigration
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

            modelBuilder.Entity("InMyAppinion.Models.Faculty", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Faculty");
                });

            modelBuilder.Entity("InMyAppinion.Models.Professor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("ID");

                    b.ToTable("Professor");
                });

            modelBuilder.Entity("InMyAppinion.Models.ProfessorReview", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorID");

                    b.Property<int>("InteractionGrade");

                    b.Property<int>("MentorGrade");

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

            modelBuilder.Entity("InMyAppinion.Models.ProfessorSubjectSet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProfessorID");

                    b.Property<int>("SubjectID");

                    b.HasKey("ID");

                    b.HasIndex("ProfessorID");

                    b.HasIndex("SubjectID");

                    b.ToTable("ProfessorSubjectSet");
                });

            modelBuilder.Entity("InMyAppinion.Models.ProfessorTag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("ProfessorTag");
                });

            modelBuilder.Entity("InMyAppinion.Models.ProfessorTagSet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProfessorReviewID");

                    b.Property<int>("ProfessorTagID");

                    b.HasKey("ID");

                    b.HasIndex("ProfessorReviewID");

                    b.HasIndex("ProfessorTagID");

                    b.ToTable("ProfessorTagSet");
                });

            modelBuilder.Entity("InMyAppinion.Models.Subject", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FacultyID");

                    b.Property<string>("Name");

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
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SubjectReviewID");

                    b.Property<int>("SubjectTagID");

                    b.HasKey("ID");

                    b.HasIndex("SubjectReviewID");

                    b.HasIndex("SubjectTagID");

                    b.ToTable("SubjectTagSet");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("Role");
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

            modelBuilder.Entity("InMyAppinion.Models.ProfessorReview", b =>
                {
                    b.HasOne("InMyAppinion.Models.ApplicationUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorID");

                    b.HasOne("InMyAppinion.Models.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorID")
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

            modelBuilder.Entity("InMyAppinion.Models.ProfessorTagSet", b =>
                {
                    b.HasOne("InMyAppinion.Models.ProfessorReview", "ProfessorReview")
                        .WithMany("ProfessorTagSet")
                        .HasForeignKey("ProfessorReviewID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InMyAppinion.Models.ProfessorTag", "ProfessorTag")
                        .WithMany("ProfessorTagSet")
                        .HasForeignKey("ProfessorTagID")
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
                        .WithMany()
                        .HasForeignKey("AuthorID");

                    b.HasOne("InMyAppinion.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InMyAppinion.Models.SubjectTagSet", b =>
                {
                    b.HasOne("InMyAppinion.Models.SubjectReview", "SubjectReview")
                        .WithMany("SubjectTagSet")
                        .HasForeignKey("SubjectReviewID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InMyAppinion.Models.SubjectTag", "SubjectTag")
                        .WithMany("SubjectTagSet")
                        .HasForeignKey("SubjectTagID")
                        .OnDelete(DeleteBehavior.Cascade);
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
