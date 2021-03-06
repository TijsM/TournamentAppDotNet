﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.Models;

namespace TournamentApi.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TournamentContext _context;
        private readonly DbSet<User> _users;


        public UserRepository(TournamentContext context)
        {
            _context = context;
            _users = context.Users_domain;
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _users
                .Include(u => u.Tournament)
                .Include(u => u.UserMatches)
                .Include(u => u.PendingMatch)
                 .OrderBy(u => u.RankInTournament)
                 
                 .ThenBy(u => u.FirstName)
                 .ToList(); 

        }

        public User GetByEmail(string email)
        {
            return _users
                 .Where(u => u.Email.Equals(email))
                 .SingleOrDefault(u => u.Email.Equals(email));
        }

        public User GetById(int id)
        {
            return _users
                .Include(u => u.Tournament)
                .Include(u => u.UserMatches)
                .Include(u => u.PendingMatch)
                .SingleOrDefault(u => u.UserId == id);
        }

        public User getByIdNoIncludes(int id)
        {
            return _users.SingleOrDefault(u => u.UserId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void update(User user)
        {
            _users.Update(user);
        }

    }
}
