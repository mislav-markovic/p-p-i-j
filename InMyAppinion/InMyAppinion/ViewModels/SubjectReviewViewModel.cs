using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.ViewModels
{
    public class SubjectReviewViewModel : InMyAppinion.Models.SubjectReview
    {
        public ICollection<int> tags { get; set; }
    }
}
