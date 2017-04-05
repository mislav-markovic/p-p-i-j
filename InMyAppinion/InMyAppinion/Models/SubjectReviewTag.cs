using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class SubjectReviewTag
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<SubjectReviewTagSet> SubjectReviewTagSet { get; set; }
    }
}
