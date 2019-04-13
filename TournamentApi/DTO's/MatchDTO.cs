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


      


        public MatchDTO()
        {

        }

    }


}
