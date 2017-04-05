using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class ProfessorReviewTag
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<ProfessorReviewTagSet> ProfessorReviewTagSet { get; set; }
    }
}
