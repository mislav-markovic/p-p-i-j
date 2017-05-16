using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    /*
     * Koristi se za grafički prikaz prosječnih ocjena predmeta i profesora.
     */
    public class GradeInfo
    {
        public const int MAX_GRADE = 5;
        // postotak od ukupne ocjene zaokružen na int
        public int Percentage
        {
            get {
                return (int)((Grade / MAX_GRADE) * 100);
            }
        }

        public StatusCode Status
        {
            get
            {
                if (Percentage <= 20.0)
                {
                    return StatusCode.danger;
                }
                else if (Percentage >= 80.0)
                {
                    return StatusCode.success;
                }
                else
                {
                    return StatusCode.warning;
                }
            }
        }
        public double Grade { get; set; }
    }

    /*Koristi se za odabir klase u bootstrapu*/
    public enum StatusCode{
        danger, warning, success
    }
}
