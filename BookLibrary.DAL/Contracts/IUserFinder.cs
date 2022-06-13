using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Contracts
{
    public interface IUserFinder : IFinder<User>
    {
        Task<User?> GetByUsernameAsync(string username);
    }
}
