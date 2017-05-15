using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMyAppinion.Models;
using System.ComponentModel;
using InMyAppinion.Data;

namespace InMyAppinion.ViewModels.Filters
{
    public class SearchFilter:IPageFilter
    {

        public IEnumerable<Subject> Subjects { get; set; }

        public IEnumerable<SubjectReview> SubjectReviews { get; set; }

        public IEnumerable<SubjectReviewTag> SubRevTag { get; set; }
        
        public IEnumerable<SubjectTag> SubTag { get; set; }

        public IEnumerable<Professor> Professors { get; set; }

        public IEnumerable<ProfessorReview> ProfessorReviews { get; set; }

        public IEnumerable<ProfessorReviewTag> ProfRevTag { get; set; }

        public IEnumerable<City> Cities { get; set; }

        public IEnumerable<Faculty> Faculties { get; set; }

        [DisplayName("Grad")]
        public string CityName { get; set; }

        [DisplayName("Fakultet")]
        public string FacultyName { get; set; }

        [DisplayName("Profesor")]
        public string ProfessorName { get; set; }

        [DisplayName("Minimum reviewa")]
        public int? MinReview { get; set; }

        [DisplayName("Maximum reviewa")]
        public int? MaxReview { get; set; }


        public bool IsEmpty() {

            var active = CityName != "" 
                |FacultyName != ""
                |ProfessorName!=""
                |MinReview.HasValue
                |MaxReview.HasValue;

            return !active;
        }

        public override string ToString()
        {
            return string.Format($"{CityName}|{FacultyName}|{ProfessorName}|{MinReview}|{MaxReview}");
        }

        public static SearchFilter FromString(string s)
        {
            var filter = new SearchFilter();
            var arr = s.Split(new char[] { '|' }, StringSplitOptions.None);
            try
            {

                filter.CityName = arr[0];
                filter.FacultyName = arr[1];
                filter.ProfessorName = arr[2];
                if (arr[3] != null) {
                    filter.MinReview =Convert.ToInt32(arr[3]);
                }
                else
                {
                    filter.MinReview = null;
                }
                if (arr[4] != null)
                {
                    filter.MaxReview = Convert.ToInt32(arr[4]);
                }
                else {
                    filter.MinReview = null;
                }
            }
            catch { } //to do: log...
            return filter;
        }

        public SearchFilter ApplyFilter() {

            if (CityName != "") {
                Cities = Cities.Where(city=>city.Name.ToLower().Contains(CityName.ToLower()));
            }
            if (FacultyName != "") {
                Faculties = Faculties.Where(faculty => faculty.Name.ToLower().Contains(FacultyName.ToLower()));
            }
            if (ProfessorName != "") {
                Professors = Professors.Where(professor => professor.FullName.ToLower().Contains(ProfessorName.ToLower()));
            }
            if (MinReview.HasValue) {
                Professors = Professors.Where(professor => professor.Reviews.Count() > MinReview);
                //Subjects = Subjects.Where(subject => subject.Reviews.Count() > MinReview);
            }
            if (MaxReview.HasValue) {
                Professors = Professors.Where(professor => professor.Reviews.Count() > MaxReview);
                //Subjects = Subjects.Where(subject => subject.Reviews.Count() > MaxReview);
            }
            return this;
        }

        public SearchFilter Initialize(ApplicationDbContext _context) {
            Cities = _context.City.ToList();
            Faculties = _context.Faculty.ToList();
            ProfessorReviews = _context.ProfessorReview.ToList();
            SubjectReviews = _context.SubjectReview.ToList();
            Professors = _context.Professor.Where(p=>p.Validated).ToList();
            Subjects = _context.Subject.Where(s=>s.Validated).ToList();
            return this;
        }
    }
}
