using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebapiJwt.Data;
using WebapiJwt.Models;
using WebapiJwt.ViewModels;

namespace WebapiJwt.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        
        [HttpGet]
        [Route("api/account")]
        public IEnumerable<User2> Get()
        {
            IEnumerable<User2> objList = _db.User2;
            var queryUser = objList.Select(x => new User2
            {
                Id = x.Id,
                Username = x.Username,
                Password = x.Password,
                Name = x.Name
            }).ToArray();
            return queryUser;
        }

        [HttpPost]
        [Route("api/account/SearchName")]
        public IEnumerable<User2> SearchName(string nameIn)
        {
            var nameCheck = nameIn ?? string.Empty;
            IEnumerable<User2> objList = _db.User2;
            var queryUser = objList.Select(x => new User2
            {
                Id = x.Id,
                Username = x.Username,
                Password = x.Password,
                Name = x.Name
            }).Where(y => y.Name.Contains(nameCheck, StringComparison.InvariantCultureIgnoreCase)).ToArray();
            return queryUser;
        }


        [HttpPost]
        [Route("api/account/SearchId/{id}")]
        public IActionResult SearchById(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.User2.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        [HttpPost]
        [Route("api/account/Create")]
        public IActionResult CreateUser([FromBody] User2 obj)
        {
            if (ModelState.IsValid)
            {
                _db.User2.Add(obj);
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("api/account/Edit/{id}")]
        //[Route("api/account/Edit/{id}")]
        public IActionResult EditeUser([FromBody] User2 obj)
        {
            if (ModelState.IsValid)
            {
                _db.User2.Update(obj);
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("api/account/Delete/{id}")]
        public IActionResult DeleteUser(int? id)
        {
            var obj = _db.User2.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.User2.Remove(obj);
            _db.SaveChanges();
            return Ok();
        }
    }
}