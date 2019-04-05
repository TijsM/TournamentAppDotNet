using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.Models;

namespace TournamentApi.DTO_s
{
    public class UserDetailDTO
    {
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public int TennisVlaanderenRanking { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public int RankInTournament { get; set; }
        public IEnumerable<MatchDTO> Matches { get; set; }



        public UserDetailDTO(User user)
        {
            FirstName = user.FirstName;
            FamilyName = user.FamilyName;
            TennisVlaanderenRanking = user.TennisVlaanderenRanking;
            DateOfBirth = user.DateOfBirth;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;
            Gender = user.Gender;
            RankInTournament = user.RankInTournament;
            //Matches = user.User.Select(m => new MatchDTO()
            //{
            //    WinnerFullName = m.Winner.getFullName(),
            //    WinnerId = m.Winner.UserId,
            //    LoserFullName = m.Loser.getFullName(),
            //    LoserId = m.Loser.UserId
            //});
        }

        public UserDetailDTO()
        {

        }

    }
}
