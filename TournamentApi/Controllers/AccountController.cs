using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TournamentApi.DTO_s;
using TournamentApi.Models;

namespace TournamentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IConfiguration _config;


        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IUserRepository userRepository, IConfiguration config, ITournamentRepository tournamentRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _tournamentRepository = tournamentRepository;
            _config = config;
        }


        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model">Login details</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<String>> CreateToken(LoginDTO model)
        {
            IdentityUser idenUser = await _userManager.FindByNameAsync(model.Email);
            User user = _userRepository.GetByEmail(model.Email);
            if (idenUser != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(idenUser, model.Password, false);
                if (result.Succeeded)
                {
                    user.Token = GetToken(idenUser);
                    return Ok(user);
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<String>> Register(RegisterDTO model)
        {
            //IdentityUser user = new IdentityUser { UserName = model.Email, Email = model.Email };
            //User newUser = new User
            //{

            //    FirstName = model.FirstName,
            //    FamilyName = model.FamilyName,
            //    DateOfBirth = model.DateOfBirth,
            //    PhoneNumber = model.Phone,
            //    Email = model.Email,
            //    Gender = model.gender,
            //    TennisVlaanderenRanking = model.TennisVlaanderenScore

            //};
            //var result = await _userManager.CreateAsync(user, model.Password);

            //if (result.Succeeded)
            //{
            //    _userRepository.Add(newUser);
            //    _userRepository.SaveChanges();
            //    var us
            //}

            IdentityUser idenUser = new IdentityUser { UserName = model.Email, Email = model.Email };
            User user = new User
            {
                FirstName = model.FirstName,
                FamilyName = model.FamilyName,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.Phone,
                Email = model.Email,
                Gender = model.gender,
                TennisVlaanderenRanking = model.TennisVlaanderenScore
            };

            int genderid;
            if (user.Gender == Gender.Man)
                genderid = 2;
            else
            {
                genderid = 1;
            }

            Tournament tour = _tournamentRepository.GetById(genderid);
            tour.AddUser(user);
            var result = await _userManager.CreateAsync(idenUser, model.Password);
            if (result.Succeeded)
            {
                user.Token = GetToken(idenUser);

                _userRepository.Add(user);
                //_userRepository.SaveChanges();

                Console.WriteLine(user);
                UserDetailDTO usdto = new UserDetailDTO(user);

                return Ok(usdto);



            }
            return BadRequest();

        }

        [AllowAnonymous]
        [HttpGet("checkusername")]
        public async Task<ActionResult<Boolean>> CheckAvailableUserName(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            return user == null;
        }
            
        private String GetToken(IdentityUser user)
        {
            // Create the token
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.Email),//sub is voor subject
              new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              null, null,
              claims,
              expires: DateTime.Now.AddMinutes(30),//om de hoeveel tijd inactief opnieuw aanmelden
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }


}