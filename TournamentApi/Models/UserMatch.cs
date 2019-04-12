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

        public bool Winner { get; set; }
        public string WinnerFullName
        {
            get{
                if (Winner)
                    return User.getFullName();
                else
                    return "";
            }
        }

        public string LoserFullName
        {
            get{
                if (Winner == false)
                    return User.getFullName();
                else
                    return "";
            }
        }

        public int WinnerId
        {
            get
            {
                if (Winner)
                    return User.UserId;
                else
                    return 0;
            }
        }

        public int LoserId
        {
            get
            {
                if (Winner == false)
                    return User.UserId;
                else
                    return 0;
            }
        }


        public bool Player1 { get; set; }

        public int GamesWonSet1 { get; set; }
        public int GamesWonSet2 { get; set; }
        public int GamesWonSet3 { get; set; }




        public UserMatch(int userId, User user, int matchId, Match match)
        {
            UserId = userId;
            User = user;
            MatchId = matchId;
            Match = match;
        }


        public UserMatch()
        {

        }
    }
}
