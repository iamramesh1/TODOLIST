using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TODOLIST.Models;

namespace TODOLIST.Controllers
{
    public class AuthController : ControllerBase
    {


        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var key = Encoding.ASCII.GetBytes("dB1siH6ePfqU4AeiH7tXk9FkFLn6cYxUgUncXx/aQ+4=");
            if (login.Username == "user" && login.Password == "password")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, login.Username),
                new Claim(JwtRegisteredClaimNames.Iss, "https://localhost:7265")
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { Token = tokenHandler.WriteToken(token) });
            }
            return BadRequest("Please try again");


        }


    }
}
