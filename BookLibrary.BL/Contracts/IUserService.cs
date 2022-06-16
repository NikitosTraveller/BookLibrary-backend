using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.BL.Contracts
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user);

        Task<User?> GetByUsernameAsync(string username);

        Task<User?> GetByIdAsync(int userId);

        int GetUserId(string jwt, string secret);

        bool ValidatePassword(string realPassword, string passwordToVerify);
    }
}
