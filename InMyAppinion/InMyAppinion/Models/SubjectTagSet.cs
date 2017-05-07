using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class SubjectTagSet
    {
        public int ID { get; set; }

        [Display(Name = "Predmet")]
        public int SubjectID { get; set; }

        [Display(Name = "Tag")]
        public int SubjectTagID { get; set; }

        public Subject Subject { get; set; }
        public SubjectTag SubjectTag { get; set; }
    }
}
