using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentApi.Models
{
    public class User
    {
        #region Properties
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public int TennisVlaanderenRanking { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public int RankInTournament { get; set; }
        public Tournament Tournament { get; set; }
        public IEnumerable<UserMatch> UserMatches { get; set; }
        public IEnumerable<Match> Matches { get; set; }
        #endregion


        #region Const
        public User()
        {

        }





        #endregion
    }




}
