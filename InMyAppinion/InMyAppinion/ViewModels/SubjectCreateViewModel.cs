using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMyAppinion.Models;

namespace InMyAppinion.ViewModels
{
    public class SubjectCreateViewModel: Subject
    {
        public ICollection<int> professorIDs { get; set; }
        public ICollection<int> tags { get; set; }
        public bool isUser { get; set; }
    }
}
