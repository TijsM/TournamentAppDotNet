using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.Models;

namespace TournamentApi.DTO_s
{
    public class UserDetailDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public int TennisVlaanderenRanking { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public int RankInTournament { get; set; }
        public IEnumerable<MatchDTO> Matches { get; set; }
        public int TournamentId { get; set; }
        public Boolean HasChallenge { get; set; }


        public UserDetailDTO(User user)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            FamilyName = user.FamilyName;
            TennisVlaanderenRanking = user.TennisVlaanderenRanking;
            DateOfBirth = user.DateOfBirth;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;
            Gender = user.Gender;
            RankInTournament = user.RankInTournament;
            TournamentId = user.Tournament.TournamentId;
            HasChallenge = user.HasChallenge;

            Matches = user.UserMatches.Select(m => new MatchDTO()
            {
                MatchId = m.MatchId,
                WinnerFullName = m.WinnerFullName,
                LoserFullName = m.LoserFullName,
                WinnerId = m.WinnerId,
                LoserId = m.LoserId,
            });
           
        }

        public UserDetailDTO()
        {

        }

    }
}
