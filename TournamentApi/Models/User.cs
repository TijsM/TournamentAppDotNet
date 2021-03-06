﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.Data.Repositories;
using TournamentApi.DTO_s;

namespace TournamentApi.Models
{
    public class User
    {
           #region Properties
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public int TennisVlaanderenRanking { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public int RankInTournament { get; set; }
        public Tournament Tournament { get; set; }
        public ICollection<UserMatch> UserMatches { get; set; }
        //public ICollection<Match> Matches { get; set; }
        public string Token { get;  set; }
        public Boolean HasChallenge { get; set; }
        public Match PendingMatch { get; set; }
        #endregion


        #region Constructor
        public User()
        {
            UserMatches = new List<UserMatch>();
            HasChallenge = false;

           
        }
        
        #endregion


        public string getFullName()
        {
            return FirstName + " " + FamilyName;
        }
    }




}
