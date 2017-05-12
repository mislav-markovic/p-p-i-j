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
        public string Name { get; set; }

        [Display(Name = "Skraćenica")]
        public string ShortName { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Potvrđen?")]
        public bool Validated { get; set; }

        [Display(Name = "Korisnost")]
        public double AvgUsefulnessGrade { get; set; }
        [Display(Name = "Zanimljivost")]
        public double AvgInterestGrade { get; set; }
        [Display(Name = "Težina")]
        public double AvgDifficultyGrade { get; set; }
        [Display(Name = "Ukupno")]
        public double AvgTotal { get; set; }

        [Display(Name = "Fakultet")]
        public Faculty Faculty { get; set; }
        [Display(Name = "Područja")]
        public ICollection<SubjectTag> SubjectTags { get; set; }
        [Display(Name = "Predavači")]
        public ICollection<Professor> Professors { get; set; }
        [Display(Name = "Kritike")]
        public ICollection<SubjectReview> Reviews { get; set; }
        public IDictionary<string, GradeInfo> Grades { get; set; }

    }
}
