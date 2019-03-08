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
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }


        #endregion
    }

    
}
