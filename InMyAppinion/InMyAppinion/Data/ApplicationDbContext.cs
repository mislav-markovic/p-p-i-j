using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InMyAppinion.Models;

namespace InMyAppinion.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> User{ get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<ProfessorReview> ProfessorReview { get; set; }
        public DbSet<ProfessorTag> ProfessorTag { get; set; }
        public DbSet<ProfessorTagSet> ProfessorTagSet { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<SubjectReview> SubjectReview { get; set; }
        public DbSet<SubjectTag> SubjectTag { get; set; }
        public DbSet<SubjectTagSet> SubjectTagSet { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("User");
            builder.Entity<IdentityRole>().ToTable("Role");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");

            builder.Entity<Faculty>().ToTable("Faculty");
            builder.Entity<Professor>().ToTable("Professor");
            builder.Entity<ProfessorReview>().ToTable("ProfessorReview");
            builder.Entity<ProfessorSubjectSet>().ToTable("ProfessorSubjectSet");
            builder.Entity<ProfessorTag>().ToTable("ProfessorTag");
            builder.Entity<ProfessorTagSet>().ToTable("ProfessorTagSet");
            builder.Entity<Subject>().ToTable("Subject");
            builder.Entity<SubjectReview>().ToTable("SubjectReview");
            builder.Entity<SubjectTag>().ToTable("SubjectTag");
            builder.Entity<SubjectTagSet>().ToTable("SubjectTagSet");
        }
    }
}
