using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InMyAppinion.Models
{
    public class ProfessorReview
    {
        public int ID { get; set; }
        [Display(Name = "Recenzija")]
        [Required(ErrorMessage = "Unos recenzije je obavezan", AllowEmptyStrings = false)]
        [StringLength(4000, ErrorMessage = "Maksimalna duljina recenzije je 4000 znakova")]
        public string Text { get; set; }
        [Display(Name = "Kvaliteta predavača")]
        [Required(ErrorMessage = "Obavezna ocjena")]
        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5")]
        public int QualityGrade { get; set; }
        [Display(Name = "Ocjena interaktivnosti")]
        [Required(ErrorMessage = "Obavezna ocjena")]
        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5")]
        public int InteractionGrade { get; set; }
        [Display(Name = "Pristupačnost profesora")]
        [Required(ErrorMessage = "Obavezna ocjena")]
        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5")]
        public int HelpfulnessGrade { get; set; }
        [Display(Name = "Ocjena mentorstva")]
        [Range(0, 5, ErrorMessage = "Ocjena mora biti između 1 i 5")]
        public int? MentorGrade { get; set; }
        [Display(Name = "Ukupna ocjena")]
        [Range(0.0, 5.0, ErrorMessage = "Ocjena mora biti između 1 i 5")]
        public decimal TotalGrade { get; set; }
        [Display(Name = "Bodovi")]
        public int Points { get; set; }
        [Display(Name = "Datum")]
        public DateTime Timestamp { get; set; }

        [Display(Name = "Autor recenzije")]
        [Required(ErrorMessage = "Recenzija mora imati autora")]
        public string AuthorID { get; set; }
        [Display(Name = "Autor recenzije")]
        public ApplicationUser Author { get; set; }
        [Display(Name = "Recenzija o profesoru")]
        public int ProfessorID { get; set; }
        public Professor Professor { get; set; }

        public ICollection<ProfessorReviewTagSet> ProfessorReviewTagSet { get; set; }
        [Display(Name = "Komentari")]
        public ICollection<Comment> Comments { get; set; }
        public ICollection<VoteProfessorReview> Votes { get; set; }
    }
}
