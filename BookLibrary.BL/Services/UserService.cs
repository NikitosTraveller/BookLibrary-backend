﻿using BookLibrary.BL.Contracts;
using BookLibrary.DAL;
using BookLibrary.DAL.Contracts;
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
        private readonly IUnitOfWork _unitOfWork;

        private readonly IFinder<User> _userFinder;

        private readonly IRepository<User> _userRepository;

        public UserService(IUnitOfWork unitOfWork, IFinder<User> userFinder, IRepository<User> userRepository)
        {
            _unitOfWork = unitOfWork;
            _userFinder = userFinder;
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var nonUnique = GetByUsername(user.Username);
            if(nonUnique == null)
            {
                _userRepository.Create(user);
                await _unitOfWork.Commit();
                return user;
            }
            return null;
        }

        public User? GetById(int userId)
        {
            return _userFinder.GetById(userId);
        }

        public User? GetByUsername(string username)
        {
            return _userFinder.Entities.FirstOrDefault(_ => string.Equals(_.Username, username, StringComparison.OrdinalIgnoreCase));
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
