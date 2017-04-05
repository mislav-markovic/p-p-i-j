using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class ProfessorReviewTagSet
    {
        public int ID { get; set; }
        public int ProfessorTagID { get; set; }
        public int ProfessorReviewID { get; set; }

        public ProfessorReviewTag ProfessorReviewTag { get; set; }
        public ProfessorReview ProfessorReview { get; set; }
    }
}
