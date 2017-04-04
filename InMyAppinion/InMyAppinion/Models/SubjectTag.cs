using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class SubjectTag
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<SubjectTagSet> SubjectTagSet { get; set; }
    }
}
