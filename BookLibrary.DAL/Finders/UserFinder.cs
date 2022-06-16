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

        public Task<User?> GetByIdAsync(int id)
        {
            return Entities.FirstOrDefaultAsync(user => user.Id == id);
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            return Entities.FirstOrDefaultAsync(user => string.Equals(user.Username, username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
