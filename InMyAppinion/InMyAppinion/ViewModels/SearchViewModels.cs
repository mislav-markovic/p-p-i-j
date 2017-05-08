using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMyAppinion.Models;

namespace InMyAppinion.ViewModels
{

        public class SubjectSearchViewModel {

            public IEnumerable<Subject> subjects { get; set; }
            //public IEnumerable<SubjectReview> subrevs { get; set; }

            public IEnumerable<SubjectTagSet> subtagset { get; set; }

            public IEnumerable<SubjectTag> tags { get; set; }

            public string query { get; set; }

        }

        public class ProfessorSearchViewModel {
            public IEnumerable<Professor> professors { get; set; }

            public IEnumerable<ProfessorReview> profrevs { get; set; }

            public IEnumerable<ProfessorReviewTagSet> profrevtagset { get; set; }

            public ProfessorReviewTag tag { get; set; }

        }




    }

