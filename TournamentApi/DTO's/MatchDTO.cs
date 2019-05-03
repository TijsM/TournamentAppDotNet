using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentApi.DTO_s
{
    public class MatchDTO
    {
        public int MatchId { get; set; }
        public string WinnerFullName { get; set; }
        public int WinnerId { get; set; }
        public string LoserFullName { get; set; }
        public int LoserId { get; set; }

        public int WinnerSet1 { get; set; }
        public int WinnerSet2 { get; set; }
        public int WinnerSet3 { get; set; }
        public int LoserSet1 { get; set; }
        public int LoserSet2 { get; set; }
        public int LoserSet3 { get; set; }


        public String WinnerPhoneNumber { get; set; }
        public String WinnerEmail { get; set; }
        public String LoserPhoneNumber { get; set; }
        public String loserEmail { get; set; }






        public MatchDTO()
        {

        }

    }


}
