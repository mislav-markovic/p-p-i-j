using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMyAppinion.Models;

namespace InMyAppinion.ViewModels
{
    public class SubjectReviewViewModel : SubjectReview
    {
        public ICollection<int> tags { get; set; }
    }
}
