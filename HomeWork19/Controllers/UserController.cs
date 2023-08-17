﻿using HomeWork19.Data;
using HomeWork19.Domain;
using HomeWork19.Helpers;
using HomeWork19.Models;
using HomeWork19.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace HomeWork19.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;
        private readonly PersonContext _context;

        public UserController(IUserService userService, IOptions<AppSettings> appSettings, PersonContext personContext)
        {
            _context = personContext;
            _userService = userService;
            _appSettings = appSettings.Value;
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginModel user)
        {
            var userLogin = _userService.Login(user);
            if (userLogin == null)
            {
                return BadRequest(new { message = "Username or Password is incorrect!" });
            }

            var tokenString = GenerateToken(userLogin);

            return Ok(new
            {
                Id = userLogin.Id,
                Username = userLogin.UserName,
                FirstName = userLogin.FirstName,
                LastName = userLogin.LastName,
                Role = userLogin.Role,
                Token = tokenString
            });
        }


        [HttpPost("register")]
        public IActionResult Register(UserRegisterModel registrationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = _userService.Register(registrationModel);

            return Ok(newUser);
        }


        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.UserName.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(365),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
