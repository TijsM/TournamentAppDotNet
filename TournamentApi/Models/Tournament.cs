using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentApi.Models
{
    public class Tournament
    {

        #region Properties

        public int TournamentId { get; set; }
        public string Name { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public Gender Gender { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Match> Matches { get; set; }

        #endregion

        #region Constuctors
        public Tournament()
        {
            Users = new List<User>();
            Matches = new List<Match>();
        }
        #endregion

        #region Methods
        public void AddUser(User user)
        {
            user.RankInTournament = Users.Count() + 1;
            Users.Add(user);
        }

        public void AddMatch(Match match)
        {
            Matches.Add(match);

        }

        private void OderUsersbyRankTennisVlaanderen()
        {
            Users = Users
                .OrderBy(t => t.TennisVlaanderenRanking)
                .ToList();
        }

        private void OrderUsersByRankInTournament()
        {
            Users = Users
                .OrderBy(t => t.RankInTournament)
                .ToList();
        }


        #endregion
    }
}
