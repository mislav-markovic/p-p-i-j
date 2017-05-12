using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMyAppinion.Models.SubjectViewModels;

namespace InMyAppinion.ViewModels
{
    public class SubjectRankingViewModel
    {
        public ICollection<SubjectDetailViewModel> Subjects { get; set; }
    }
}
