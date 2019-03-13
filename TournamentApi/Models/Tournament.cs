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
        public ICollection<User> Participants { get; set; }

        #endregion

        #region Constuctors
        public Tournament()
        {
            Participants = new List<User>();
        }
        #endregion

        #region Methods
        public void AddUser(User user)
        {
            Participants.Add(user);

            
        }

        private void OrderParticpantsbyRankTennisVlaanderen()
        {
            Participants = Participants
                .OrderBy(t => t.TennisVlaanderenRanking)
                .ToList();
        }

        private void OrderParticipantsByRankInTournament()
        {
            Participants = Participants
                .OrderBy(t => t.RankInTournament)
                .ToList();
        }
        #endregion
    }
}
