using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models.ProfessorViewModels
{
    public class ProfessorDetailViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Ime")]
        public string FirstName { get; set; }
        [Display(Name = "Prezime")]
        public string LastName { get; set; }
        [Display(Name = "Biografija")]
        public string Biography { get; set; }
        public bool Validated { get; set; }

        [Display(Name = "Kvaliteta")]
        public double AvgQuality { get; set; }
        [Display(Name = "Interaktivnost")]
        public double AvgInteractivity { get; set; }
        [Display(Name = "Pristupačnost")]
        public double AvgAccessibility { get; set; }
        [Display(Name = "Mentorstvo")]
        public double AvgMentoring { get; set; }
        [Display(Name = "Ukupna ocjena")]
        public double AvgTotal { get; set; }

        [Display(Name = "Predmeti")]
        public ICollection<Subject> Subjects { get; set; }
        [Display(Name = "Kritike")]
        public ICollection<ProfessorReview> Reviews { get; set; }

        [Display(Name = "Sveučilišta")]
        public ICollection<University> Universities { get; set; }
        [Display(Name = "Fakulteti")]
        public ICollection<Faculty> Faculties { get; set; }
        [Display(Name = "Interesi")]
        public ICollection<SubjectTag> Interests { get; set; }
        public IDictionary<string, GradeInfo> Grades { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
