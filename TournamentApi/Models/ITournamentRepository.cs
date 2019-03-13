using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentApi.Models
{
    public interface ITournamentRepository
    {
        IEnumerable<Tournament> GetAll();
        Tournament GetById(int id);
        void Add(Tournament tournament);
        void Delete(Tournament tournament);
        void update(Tournament tournament);
        void  SaveChanges();
    }
}
