using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.Models;

namespace TournamentApi.DTO_s
{
    public class TournamentDTO
    {

        public int TournamentId { get; set; }
        public string Name { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public Gender Gender { get; set; }
        public IEnumerable<UserDetailDTO> Users { get; set; }
        public IEnumerable<Match> Matches { get; set; }

        public TournamentDTO(Tournament tournament)
        {
            TournamentId = tournament.TournamentId;
            Name = tournament.Name;
            EndDate = tournament.EndDate;
            StartDate = tournament.StartDate;
            Gender = tournament.Gender;
            Users = tournament.Users.Select(t => new UserDetailDTO(t));
            Matches = tournament.Matches;
        }

    }
}
