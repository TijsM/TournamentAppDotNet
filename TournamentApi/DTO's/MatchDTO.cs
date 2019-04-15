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

        public int p1s1 { get; set; }
        public int p1s2 { get; set; }
        public int p1s3 { get; set; }
        public int p2s1 { get; set; }
        public int p2s2 { get; set; }
        public int p2s3 { get; set; }






        public MatchDTO()
        {

        }

    }


}
