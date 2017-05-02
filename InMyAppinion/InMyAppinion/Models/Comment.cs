using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InMyAppinion.Models
{
    public class Comment
    {
        public int ID { get; set; }
        [Display(Name = "Komentar")]
        [Required(ErrorMessage = "{0} je obavezan", AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "{0} mora biti između 3 i 200 znakova")]
        public string Text { get; set; }
        [Display(Name = "Bodovi")]
        [Required]
        public int Points { get; set; }
        [Display(Name = "Datum")]
        [Required]
        public DateTime Timestamp { get; set; }
        [Display(Name = "Roditelj")]
        public int? ParentID { get; set; }
        public int? ProfessorReviewID { get; set; }
        public int? SubjectReviewID { get; set; }
        public string AuthorID { get; set; }

        [Display(Name = "Autor")]
        [Required]
        public ApplicationUser Author { get; set; }
        [Display(Name = "Roditelj")]
        public Comment ParentComment { get; set; }
        [Display(Name = "Kritika")]
        public ProfessorReview ProfessorReview { get; set; }
        [Display(Name = "Kritika")]
        public SubjectReview SubjectReview { get; set; }
        [Display(Name = "Odgovori")]
        public ICollection<Comment> ChildComments { get; set; }
        public ICollection<VoteComment> Votes { get; set; }
    }
}
