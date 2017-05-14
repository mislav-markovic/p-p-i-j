using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InMyAppinion.Models
{
    public class Subject
    {
        public int ID { get; set; }
        [Display(Name = "Ime")]
        [Required(ErrorMessage = "{0} je obavezno", AllowEmptyStrings = false)]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} mora biti između 3 i 60 znakova")]
        public string Name { get; set; }
        [Display(Name = "Skraćenica")]
        [Required(ErrorMessage = "{0} je obavezno", AllowEmptyStrings = false)]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "{0} mora biti između 1 i 10 znakova")]
        public string ShortName { get; set; }
        [Display(Name = "Opis")]
        [Required(ErrorMessage = "{0} je obavezan", AllowEmptyStrings = false)]
        public string Description { get; set; }
        [Display(Name = "Potvrđen?")]
        public bool Validated { get; set; }
        [Required(ErrorMessage = "Obaveno je pridružiti fakultet predmetu")]
        public int FacultyID { get; set; }

        public Faculty Faculty { get; set; }
        [Display(Name = "Područja")]
        public ICollection<SubjectTagSet> SubjectTagSet { get; set; }
        [Display(Name = "Predavači")]
        public ICollection<ProfessorSubjectSet> Professors { get; set; }
        [Display(Name = "Kritike")]
        public ICollection<SubjectReview> Reviews { get; set; }
    }
}
