﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class Faculty
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int? UniversityID { get; set; }

        public University University { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}