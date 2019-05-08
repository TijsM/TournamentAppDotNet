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
        public MatchDTO PendingMatch { get; set; }



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
            if (user.Tournament != null)
            {
                TournamentId = user.Tournament.TournamentId; 
            }
            
            HasChallenge = user.HasChallenge;

            Matches = user.UserMatches.Select(m => new MatchDTO()
            {
                MatchId = m.MatchId,
                WinnerFullName = m.WinnerFullName,
                LoserFullName = m.LoserFullName,
                WinnerId = m.WinnerId,
                LoserId = m.LoserId,
            });

            if (user.PendingMatch != null)
            {

                if (user.PendingMatch.Player1 != null)
                {
                    PendingMatch = new MatchDTO
                    {
                        MatchId = user.PendingMatch.MatchId,
                        WinnerFullName = user.PendingMatch.Player1.WinnerFullName,
                        WinnerId = user.PendingMatch.Player1.UserId,
                        WinnerEmail = user.Email,
                        WinnerPhoneNumber = user.PhoneNumber,
                        LoserFullName = "tegenstander",
                        LoserId = 0,
                        loserEmail = "onbekend",
                        LoserPhoneNumber = "onbekend"
                    };
                }
                if (user.PendingMatch.Player2 != null)
                {
                    PendingMatch = new MatchDTO
                    {
                        MatchId = user.PendingMatch.MatchId,
                        WinnerFullName = "tegenstander",
                        WinnerId = 0,
                        WinnerEmail = "onbekend",
                        WinnerPhoneNumber = "onbekend",
                        LoserFullName = user.PendingMatch.Player2.WinnerFullName,
                        LoserId = user.PendingMatch.Player2.UserId,
                        LoserPhoneNumber = user.PhoneNumber,
                        loserEmail = user.Email
                    };
                }
            }
            else
            {
                PendingMatch = new MatchDTO
                {
                    MatchId = 999999999,
                    WinnerFullName = "heeft geen uitdaging",
                    WinnerId = 999999999,
                    LoserFullName = "heeft geen uitdaging",
                    LoserId = 999999999
                };
            }
        }


        public UserDetailDTO()
        {

        }

    }
}
