using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models.SubjectViewModels
{
    public class SubjectDetailViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Ime")]
        [Required(ErrorMessage = "{0} je obavezno", AllowEmptyStrings = false)]
        public string Name { get; set; }


        [Display(Name = "Skraćenica")]
        [Required(ErrorMessage = "{0} je obavezno", AllowEmptyStrings = false)]
        public string ShortName { get; set; }


        [Display(Name = "Opis")]
        [Required(ErrorMessage = "{0} je obavezan", AllowEmptyStrings = false)]
        public string Description { get; set; }


        [Display(Name = "Potvrđen?")]
        public bool Validated { get; set; }

        [Display(Name = "Korisnost predmeta")]
        public double AvgUsefulnessGrade { get; set; }
        [Display(Name = "Kvaliteta predavača")]
        public double AvgInterestGrade { get; set; }
        [Display(Name = "Težina predmeta")]
        public double AvgDifficultyGrade { get; set; }
        [Display(Name = "Ukupna ocjena")]
        public double AvgTotal { get; set; }

        [Display(Name = "Fakultet")]
        public Faculty Faculty { get; set; }
        [Display(Name = "Područja")]
        public IEnumerable<SubjectTag> SubjectTagSet { get; set; }
        [Display(Name = "Predavači")]
        public IEnumerable<Professor> Professors { get; set; }
        [Display(Name = "Kritike")]
        public IEnumerable<SubjectReview> Reviews { get; set; }
        public IDictionary<string, GradeInfo> Grades { get; set; }

    }
}
