using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class University
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int CityID { get; set; }

        public City City { get; set; }
        public ICollection<Faculty> Faculties { get; set; }
    }
}
