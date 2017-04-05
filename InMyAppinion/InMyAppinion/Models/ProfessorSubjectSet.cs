using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class ProfessorSubjectSet
    {
        public int ProfessorID { get; set; }
        public int SubjectID { get; set; }

        public Professor Professor { get; set; }
        public Subject Subject { get; set; }
    }
}
