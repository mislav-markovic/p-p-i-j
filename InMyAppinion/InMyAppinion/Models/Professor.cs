using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class Professor
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public bool Validated { get; set; }

        public ICollection<ProfessorSubjectSet> Subjects { get; set; }
        public ICollection<ProfessorReview> Reviews { get; set; }
    }
}
