﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentApi.Models
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User getByIdNoIncludes(int id);
        User GetByEmail(string email);
        void Add(User user);
        void Delete(User user);
        void update(User user);
        void SaveChanges();
      
    }
}
