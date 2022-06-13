using BookLibrary.DAL.Contracts;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Finders
{
    public class UserFinder : Finder<User>, IUserFinder
    {
        public UserFinder(ApplicationContext context) : base(context)
        {

        }

        public User? GetByUsername(string username)
        {
            return Entities.FirstOrDefault(_ => string.Equals(_.Username, username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
