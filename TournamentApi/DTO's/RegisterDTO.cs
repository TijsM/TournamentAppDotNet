using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.Models;

namespace TournamentApi.DTO_s
{
    public class RegisterDTO : LoginDTO
    {
        [Required]
        [StringLength(200)]
        public String FirstName { get; set; }

        [Required]
        [StringLength(250)]
        public String FamilyName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        [Compare("Password")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Wachtwoord moet minstens 8 tekens bevatten met minsten één hoofdletter en één nummer")]
        public String PasswordConfirmation { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public Gender gender { get; set; }

        [Required]
        public int TennisVlaanderenScore { get; set; }


    }
}
