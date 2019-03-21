using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public void Delete(Tournament tournament)
        {
            _tournaments.Remove(tournament);
        }

        public IEnumerable<Tournament> GetAll()
        {
            return _tournaments
                .Include(t => t.Users)
                .ToList();
        }

        public Tournament GetById(int id)
        {
            return _tournaments
                .Include(t => t.Users)
                .SingleOrDefault(t => t.TournamentId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void update(Tournament tournament)
        {
            _tournaments.Update(tournament);
        }
    }
}
