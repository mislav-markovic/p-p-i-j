using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMyAppinion.Models;

namespace InMyAppinion.ViewModels
{
    public class ProfessorReviewViewModel : ProfessorReview
    {
        public ICollection<int> tags { get; set; }
    }
}
