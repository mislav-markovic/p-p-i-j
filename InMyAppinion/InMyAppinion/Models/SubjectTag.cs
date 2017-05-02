using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InMyAppinion.Models
{
    public class SubjectTag
    {
        public int ID { get; set; }
        [Display(Name = "Područje")]
        [Required(ErrorMessage = "Ime taga je obavezno", AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<SubjectTagSet> SubjectTagSet { get; set; }
    }
}
