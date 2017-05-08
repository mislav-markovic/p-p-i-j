using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InMyAppinion.Models
{
    public class Faculty
    {
        public int ID { get; set; }
        [Display(Name = "Fakultet")]
        [Required(ErrorMessage = "Ime fakulteta je obavezno", AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Name { get; set; }
        [Display(Name = "Skraćenica")]
        [Required(ErrorMessage = "Skraćenica fakulteta je obavezna", AllowEmptyStrings = false)]
        [StringLength(20)]
        public string ShortName { get; set; }
        [Required(ErrorMessage = "Fakultet mora biti pridružen sveučilištu")]
        public int? UniversityID { get; set; }

        public University University { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
