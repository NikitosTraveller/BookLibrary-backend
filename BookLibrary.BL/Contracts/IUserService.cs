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
        User CreateUser(User user);

        User GetByUsername(string username);

        User GetById(int userId);

        int GetUserId(string jwt, string secret);

        bool ValidatePassword(string realPassword, string passwordToVerify);
    }
}
