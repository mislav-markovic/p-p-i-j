using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int Points { get; set; }

        [DataType(DataType.Date)]
        public DateTime Timestamp { get; set; }
        public int? ParentCommentID { get; set; }
        public int? ProfessorReviewID { get; set; }
        public int? SubjectReviewID { get; set; }
        public string AuthorID { get; set; }

        public ApplicationUser Author { get; set; }
        public Comment ParentComment { get; set; }
        public ProfessorReview ProfessorReview { get; set; }
        public SubjectReview SubjectReview { get; set; }
        public ICollection<Comment> ChildComments { get; set; }
        public ICollection<VoteComment> Votes { get; set; }
    }
}
