using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class SubjectTagSet
    {
        public int SubjectID { get; set; }
        public int SubjectTagID { get; set; }

        public Subject Subject { get; set; }
        public SubjectTag SubjectTag { get; set; }
    }
}
