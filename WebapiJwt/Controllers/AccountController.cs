using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebapiJwt.Data;
using WebapiJwt.Models;

namespace WebapiJwt.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        [HttpGet]
        [Route("api/account")]
        public IActionResult Get()
        {
            IEnumerable<User2> objList = _db.User2;
            var queryUser = objList.Select(x => new
            {
                x.Username,
                x.Name,
                x.Age
            }).ToList();
            return Json(queryUser);
        }
    }
}