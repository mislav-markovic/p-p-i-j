﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InMyAppinion.Models
{
    public class SubjectReviewTag
    {
        public int ID { get; set; }
        [Display(Name = "Tag")]
        [Required(ErrorMessage = "Ime taga je obavezno", AllowEmptyStrings = false)]
        [StringLength(20)]
        public string Name { get; set; }

        public ICollection<SubjectReviewTagSet> SubjectReviewTagSet { get; set; }
    }
}
