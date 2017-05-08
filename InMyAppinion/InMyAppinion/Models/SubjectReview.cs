using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InMyAppinion.Models
{
    public class SubjectReview
    {
        public int ID { get; set; }
        [Display(Name = "Recenzija")]
        [Required(ErrorMessage = "Unos recenzije je obavezan", AllowEmptyStrings = false)]
        [StringLength(4000, ErrorMessage = "Maksimalna duljina recenzije je 4000 znakova")]
        public string Text { get; set; }
        [Display(Name = "Korisnost predmeta")]
        [Required(ErrorMessage = "Obavezna ocjena")]
        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5")]
        public int UsefulnessGrade { get; set; }
        [Display(Name = "Kvaliteta predavača")]
        [Required(ErrorMessage = "Zanimljivost gradiva")]
        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5")]
        public int InterestGrade { get; set; }
        [Display(Name = "Težina predmeta")]
        [Required(ErrorMessage = "Obavezna ocjena")]
        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5")]
        public int DifficultyGrade { get; set; }
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
        [Display(Name = "Predmet")]
        [Required(ErrorMessage = "Recenzija mora biti o određenom predmetu")]
        public int SubjectID { get; set; }

        [Display(Name = "Autor recenzije")]
        public ApplicationUser Author { get; set; }
        public Subject Subject { get; set; }
        
        public ICollection<SubjectReviewTagSet> SubjectReviewTagSet { get; set; }
        [Display(Name = "Komentari")]
        public ICollection<Comment> Comments { get; set; }
        public ICollection<VoteSubjectReview> Votes { get; set; }
    }
}
