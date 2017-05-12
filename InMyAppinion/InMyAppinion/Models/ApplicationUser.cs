using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace InMyAppinion.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public bool IsBanned { get; set; }
        public int Points { get; set; }
        public DateTime DateRegistered { get; set; }
        public ICollection<VoteComment> VotedComments { get; set; }
        public ICollection<VoteProfessorReview> VotedProfessorReview { get; set; }
        public ICollection<VoteSubjectReview> VotedSubjectReview { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<ProfessorReview> ProfessorReviews { get; set; }
        public ICollection<SubjectReview> SubjectReviews { get; set; }
    }
}
