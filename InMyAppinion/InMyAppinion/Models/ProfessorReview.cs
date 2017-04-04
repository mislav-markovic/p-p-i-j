using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class ProfessorReview
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int QualityGrade { get; set; }
        public int InteractionGrade { get; set; }
        public int MentorGrade { get; set; }
        public decimal TotalGrade { get; set; }
        public int Points { get; set; }
        public DateTime Timestamp { get; set; }

        public string AuthorID { get; set; }
        public ApplicationUser Author { get; set; }
        public int ProfessorID { get; set; }
        public Professor Professor { get; set; }

        public ICollection<ProfessorTagSet> ProfessorTagSet { get; set; }
    }
}
