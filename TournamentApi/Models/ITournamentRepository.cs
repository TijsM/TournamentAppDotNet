﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.DTO_s;

namespace TournamentApi.Models
{
    public interface ITournamentRepository
    {
        IEnumerable<TournamentDTO> GetAll();
        TournamentDTO GetByIdDTO(int id);
        Tournament GetById(int id);
        void Add(Tournament tournament);
        void Delete(int id);
        void update(Tournament tournament);
        void  SaveChanges();
        IEnumerable<UserDetailDTO> giveRanking(int tournamentId);
    }
}
