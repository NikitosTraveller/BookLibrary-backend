using BookLibrary.BL.Contracts;
using BookLibrary.Helpers;
using BookLibrary.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Services
{
    public class UserService : IUserService
    {
        private ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public User CreateUser(User user)
        {
            var nonUnique =  _context.Users.SingleOrDefault(u => u.Username == user.Username);
            if(nonUnique == null)
            {
                _context.Users.Add(user);
                user.Id = _context.SaveChanges();
                return user;
            }
            return null;
        }

        public User GetById(int userId)
        {
            return _context.Users.FirstOrDefault(user => user.Id == userId);
        }

        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public int GetUserId(string jwt, string secret)
        {
            var validatedToken = JwtHelper.Verify(jwt, secret);
            return int.Parse(validatedToken.Issuer);
        }

        public bool ValidatePassword(string realPassword, string passwordToVerify)
        {
            return BCrypt.Net.BCrypt.Verify(passwordToVerify, realPassword);
        }
    }
}
