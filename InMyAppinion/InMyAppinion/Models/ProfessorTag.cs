using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class ProfessorTag
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<ProfessorTagSet> ProfessorTagSet { get; set; }
    }
}
