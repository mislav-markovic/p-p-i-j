using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMyAppinion.Models;

namespace InMyAppinion.ViewModels
{
    public class ProfessorCreateViewModel: Professor
    {
        public ICollection<int> SubjectIDs { get; set; }
        public bool isUser { get; set; }
    }
}
