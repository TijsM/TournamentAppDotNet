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

        #endregion

        #region Constuctors
        public Tournament()
        {
            Users = new List<User>();
        }
        #endregion

        #region Methods
        public void AddUser(User user)
        {
            Users.Add(new User() { UserId = user.UserId, FirstName = user.FirstName, FamilyName = user.FamilyName, TennisVlaanderenRanking = user.TennisVlaanderenRanking, DateOfBirth = user.DateOfBirth, Password = user.Password, PhoneNumber = user.PhoneNumber, Email = user.Email, Gender = user.Gender });
        }
        #endregion
    }
}
