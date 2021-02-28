using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebapiJwt.Data;
using WebapiJwt.Models;

namespace WebapiJwt.Controllers
{
    public class TokenController : Controller
    {
        private const string SECRET_KEY = "HELLISBETTERTHANHEAVENHELLISBETTERTHANHEAVENHELLISBETTERTHANHEAVEN";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenController.SECRET_KEY));

        private readonly ApplicationDbContext _db;

        public TokenController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("api/Token/user")]
        public IActionResult Post(string username, string password)
        {
            IEnumerable<User2> objList = _db.User2;
            var queryUser = objList.Where(x => x.Username == username && x.Password == password).FirstOrDefault();

            if (queryUser != null)
            {
                return new ObjectResult(GenerateToken(username));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/Token/{username}/{password}")]
        public IActionResult Get(string username, string password)
        {
            if (username == password)
                return new ObjectResult(GenerateToken(username));
            else
                return BadRequest();
        }

        private object GenerateToken(string username)
        {
            var token = new JwtSecurityToken(
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                },
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
                signingCredentials: new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
