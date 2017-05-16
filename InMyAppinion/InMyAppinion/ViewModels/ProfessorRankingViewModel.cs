using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMyAppinion.Models.ProfessorViewModels;

namespace InMyAppinion.ViewModels
{
    public class ProfessorRankingViewModel
    {
        public ICollection<ProfessorDetailViewModel> Professors { get; set; }
        public int tag { get; set; }
        public int sort { get; set; }
    }
}
