using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InMyAppinion.Models
{
    public class Professor
    {
        public int ID { get; set; }
        [Display(Name = "Ime")]
        [Required(ErrorMessage = "{0} je obavezno", AllowEmptyStrings = false)]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} mora biti između 3 i 60 znakova")]
        public string FirstName { get; set; }
        [Display(Name = "Prezime")]
        [Required(ErrorMessage = "{0} je obavezno", AllowEmptyStrings = false)]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} mora biti između 3 i 60 znakova")]
        public string LastName { get; set; }
        [Display(Name = "Biografija")]
        public string Biography { get; set; }
        [Display(Name = "Potvrđen?")]
        public bool Validated { get; set; }

        [Display(Name = "Predmeti")]
        public ICollection<ProfessorSubjectSet> Subjects { get; set; }
        [Display(Name = "Kritike")]
        public ICollection<ProfessorReview> Reviews { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
