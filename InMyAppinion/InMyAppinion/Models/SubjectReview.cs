using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class SubjectReview
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int UsefulnessGrade { get; set; }
        public int InterestGrade { get; set; }
        public int DifficultyGrade { get; set; }
        public decimal TotalGrade { get; set; }
        public int Points { get; set; }
        public DateTime Timestamp { get; set; }
        public string AuthorID { get; set; }
        public int SubjectID { get; set; }

        public ApplicationUser Author { get; set; }
        public Subject Subject { get; set; }
        
        public ICollection<SubjectTagSet> SubjectTagSet { get; set; }
    }
}
