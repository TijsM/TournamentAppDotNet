using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentApi.Models
{
    public class UserMatch
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public bool PlayerHawWon { get; set; }


        public UserMatch(int userId, User user, int matchId, Match match)
        {
            UserId = userId;
            User = user;
            MatchId = matchId;
            Match = match;
            PlayerHawWon = false;
        }


        public UserMatch()
        {

        }
    }
}
