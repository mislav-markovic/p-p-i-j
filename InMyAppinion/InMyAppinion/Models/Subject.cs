using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class Subject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public int FacultyID { get; set; }

        public Faculty Faculty { get; set; }
        public ICollection<SubjectTagSet> SubjectTagSet { get; set; }
        public ICollection<ProfessorSubjectSet> Professors { get; set; }
    }
}
