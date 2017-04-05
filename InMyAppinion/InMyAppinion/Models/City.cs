using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ZipCode { get; set; }

        public ICollection<University> Universities { get; set; }
    }
}
