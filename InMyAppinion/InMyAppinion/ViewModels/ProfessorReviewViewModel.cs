using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.ViewModels
{
    public class ProfessorReviewViewModel : InMyAppinion.Models.ProfessorReview
    {
        public ICollection<int> tags { get; set; }
    }
}
