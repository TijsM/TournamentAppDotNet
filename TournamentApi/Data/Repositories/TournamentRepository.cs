using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.DTO_s;
using TournamentApi.Models;

namespace TournamentApi.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentContext _context;
        private readonly DbSet<Tournament> _tournaments;


        public TournamentRepository(TournamentContext context)
        {
            _context = context;
            _tournaments = context.Tournaments;
        }
        public void Add(Tournament tournament)
        {
            _tournaments.Add(tournament);
        }

        public void Delete(int id)
        {
             var t = _tournaments
                .SingleOrDefault(n => n.TournamentId == id);

            _tournaments.Remove(t);
        }

        public IEnumerable<TournamentDTO> GetAll()
        {
            var tournaments =  _tournaments
                .Include(t => t.Users)
                .ToList();

            return MakeTournamentDTOList(tournaments);
        }

        public TournamentDTO GetById(int id)
        {
            var tour =  _tournaments
                .Include(t => t.Users)
                .SingleOrDefault(t => t.TournamentId == id);

            return new TournamentDTO(tour);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void update(Tournament tournament)
        {
            _tournaments.Update(tournament);
        }

        private IList<TournamentDTO> MakeTournamentDTOList(List<Tournament> tournamentList)
        {
            IList<TournamentDTO> dtoList = new List<TournamentDTO>();
            foreach(var t in tournamentList)
            {
                dtoList.Add(new TournamentDTO(t));
            }

            return dtoList;
        }



    }
}
