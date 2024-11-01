using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace ControllerAPI.Controllers
{
    public class UserLogin{
        public string? Username {get;set;}
        public string? Password {get;set;}
    }

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly string _key;

        public AuthController(){
            _key = "This is a sample secret key for JWT token";
        }

        private string GenerateJwtToken(string username) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLogin userLogin){
            if (userLogin.Username != "admin" || userLogin.Password != "admin"){
                return Unauthorized();
            }

            var token = GenerateJwtToken(userLogin.Username);

            return Ok(new {token});
        }
    }
}