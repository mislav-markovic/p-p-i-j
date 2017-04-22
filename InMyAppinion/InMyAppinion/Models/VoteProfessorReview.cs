using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class VoteProfessorReview
    {
        public int ID { get; set; }
        public bool Vote { get; set; }
        public string VoterID { get; set; }
        public int ProfessorReviewID { get; set; }

        public ApplicationUser Voter { get; set; }
        public ProfessorReview ProfessorReview { get; set; }
    }
}
