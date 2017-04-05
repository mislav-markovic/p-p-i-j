using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class SubjectReviewTagSet
    {
        public int ID { get; set; }
        public int SubjectTagID { get; set; }
        public int SubjectReviewID { get; set; }

        public SubjectReviewTag SubjectReviewTag { get; set; }
        public SubjectReview SubjectReview { get; set; }
    }
}
