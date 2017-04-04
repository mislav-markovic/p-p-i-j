using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class SubjectTagSet
    {
        public int ID { get; set; }
        public int SubjectTagID { get; set; }
        public int SubjectReviewID { get; set; }

        public SubjectTag SubjectTag { get; set; }
        public SubjectReview SubjectReview { get; set; }
    }
}
