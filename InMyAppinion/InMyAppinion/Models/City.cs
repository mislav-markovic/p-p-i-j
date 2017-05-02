using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InMyAppinion.Models
{
    public class City
    {
        public int ID { get; set; }
        [Display(Name = "Grad")]
        [Required(ErrorMessage = "Ime grada je obavezno", AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Name { get; set; }
        [Display(Name = "Poštanski broj")]
        [Required(ErrorMessage = "Poštanski broj je obavezan", AllowEmptyStrings = false)]
        [Range(0, int.MaxValue, ErrorMessage = "Poštanski broj mora biti pozitivan")]
        public int ZipCode { get; set; }

        public ICollection<University> Universities { get; set; }
    }
}
