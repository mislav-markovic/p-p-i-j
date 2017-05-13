using InMyAppinion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.ViewModels
{
    public class UserViewModel
    {
        public string Role { get; set; }
        public ApplicationUser User { get; set; }
        public int PositiveVotesProfessorReviews { get; set; }
        public int NegativeVotesProfessorReviews { get; set; }
        public int PositiveVotesSubjectReviews { get; set; }
        public int NegativeVotesSubjectReviews { get; set; }
        public int PositiveVotesComments { get; set; }
        public int NegativeVotesComments { get; set; }
        public List<ProfessorReview> ProfReviews { get; set; }
        public List<SubjectReview> SubjectReviews { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
