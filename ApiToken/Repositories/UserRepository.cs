using ApiToken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToken.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>
            {
                new User {Id = 1, UserName = "Gutemberg", Password = "Gutemberg", Role = "Manager"},
                new User {Id = 2, UserName = "Ricardo", Password = "Ricardo", Role = "employee"}
            };
            return users
                .FirstOrDefault(x => 
                x.UserName == username
                && x.Password == password);
        }
    }
}
