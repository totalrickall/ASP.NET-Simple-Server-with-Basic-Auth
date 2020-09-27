using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleServer.Models;

namespace SimpleServer
{
    public class NbaSecurity
    {
        public static bool Login(string username, string password)
        {
            using (NBAContext db = new NBAContext())
            {
                // comparison will be case insensitive
                return db.Users.Any(user => user.Username.Equals(username,
                   StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}