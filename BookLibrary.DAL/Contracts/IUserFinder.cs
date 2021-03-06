using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Contracts
{
    public interface IUserFinder
    {
        Task<User?> GetByUsernameAsync(string username);

        Task<User?> GetByIdAsync(int id);
    }
}
