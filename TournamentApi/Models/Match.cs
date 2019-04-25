using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentApi.Models
{
    public class Match
    {
        #region Fields
        int _amountOfGamesWonPlayer1 = 0;
        #endregion

        #region Properties
        public int MatchId { get; set; }

        public ICollection<UserMatch> UserMatches { get; set; }

        public UserMatch Player1 { get { return UserMatches.FirstOrDefault(u => u.Player1 == true); } }
        public UserMatch Player2 { get { return UserMatches.FirstOrDefault(u => u.Player1 == false);} }
        public UserMatch Winner { get { return UserMatches.FirstOrDefault(u => u.Winner == true); } }
        public UserMatch Loser { get { return UserMatches.FirstOrDefault(u => u.Winner == false); } }
        #endregion

        public Match(User player1, User player2)
        {
            UserMatches = new List<UserMatch>();
            
           
            UserMatches.Add(new UserMatch()
            {
                User = player1,
                Player1 = true,
                UserId = player1.UserId,
                MatchId = this.MatchId,
                Match = this        
            });

            UserMatches.Add(new UserMatch()
            {
                User = player2,
                Player1 = false,
                UserId = player2.UserId,
                MatchId = this.MatchId,
                Match = this
            });



        }

        public Match()
        {
            UserMatches =  new List<UserMatch>();
            
        }


        public void RegisterScore(int gp1s1, int gp2s1, int gp1s2, int gp2s2, int gp1s3 = 0, int gp2s3 = 0)
        {
            //gp1s1 = games player 1 set 0

            Player1.GamesWonSet1 = gp1s1;
            Player1.GamesWonSet2 = gp1s2;
            Player1.GamesWonSet3 = gp1s3;

            Player2.GamesWonSet1 = gp2s1;
            Player2.GamesWonSet2 = gp2s2;
            Player2.GamesWonSet3 = gp2s3;


            if (gp1s1 > gp2s1)
                _amountOfGamesWonPlayer1++;

            if (gp1s2 > gp2s2)
                _amountOfGamesWonPlayer1++;

            if (gp1s3 > gp2s3)
                _amountOfGamesWonPlayer1++;

            CalculateWinner();
        }

        private void CalculateWinner()
        {
            if (_amountOfGamesWonPlayer1 >= 2)
            {

                Player2.Winner = false;
                Player1.Winner = true;

            }
            else
            {
                Player2.Winner = true;
                Player1.Winner = false;

            }
        }

    }
}
