using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InMyAppinion.Models
{
    public class University
    {
        public int ID { get; set; }
        [Display(Name = "Sveučilište")]
        [Required(ErrorMessage = "Ime sveučilišta je obavezno", AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Name { get; set; }
        [Display(Name = "Skraćenica")]
        [Required(ErrorMessage = "Skraćenica sveučilišta je obavezna", AllowEmptyStrings = false)]
        [StringLength(20)]
        public string ShortName { get; set; }
        [Required(ErrorMessage = "Sveučilište mora biti pridruženo gradu")]
        public int CityID { get; set; }

        public City City { get; set; }
        public ICollection<Faculty> Faculties { get; set; }
    }
}
