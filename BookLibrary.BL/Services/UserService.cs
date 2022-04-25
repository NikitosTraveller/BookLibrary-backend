using BookLibrary.BL.Contracts;
using BookLibrary.DAL;
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

        private readonly IUnitOfWork _unitOfWork;

        private readonly IFinder<User> _userFinder;

        private readonly IRepository<User> _userRepository;

        public UserService(ApplicationContext context, IUnitOfWork unitOfWork, IFinder<User> userFinder, IRepository<User> userRepository)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _userFinder = userFinder;
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var nonUnique =  _context.Users.SingleOrDefault(u => u.Username == user.Username);
            if(nonUnique == null)
            {
                _userRepository.Create(user);
                user.Id = await _unitOfWork.Commit();
                return user;
            }
            return null;
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _userFinder.GetByIdAsync(userId);
        }

        public async Task<User> GetByUsernameAsync(string username)
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
