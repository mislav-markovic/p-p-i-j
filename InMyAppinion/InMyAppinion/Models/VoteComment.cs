using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMyAppinion.Models
{
    public class VoteComment
    {
        public int ID { get; set; }
        public bool Vote { get; set; }
        public string VoterID { get; set; }
        public int CommentID { get; set; }

        public ApplicationUser Voter { get; set; }
        public Comment Comment { get; set; }
    }
}
