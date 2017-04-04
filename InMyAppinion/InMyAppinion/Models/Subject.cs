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

        //FK
        public int FacultyID { get; set; }     
        //Navigation property
        public Faculty Faculty { get; set; }

        public ICollection<ProfessorSubjectSet> Professors { get; set; }
    }
}
