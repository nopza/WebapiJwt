using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebapiJwt.ViewModels
{
    public class UserViewModel
    {
        public List<UserModel> UserList { get; set; }
    }
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

}
