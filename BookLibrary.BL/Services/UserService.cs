using BookLibrary.BL.Contracts;
using BookLibrary.DAL;
using BookLibrary.DAL.Contracts;
using BookLibrary.Helpers;
using BookLibrary.Models;

namespace BookLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IUserFinder _userFinder;

        private readonly IRepository<User> _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserFinder userFinder, IRepository<User> userRepository)
        {
            _unitOfWork = unitOfWork;
            _userFinder = userFinder;
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var nonUnique = GetByUsernameAsync(user.Username);
            if(nonUnique == null)
            {
                _userRepository.Create(user);
                await _unitOfWork.Commit();
                return user;
            }
            return null;
        }

        public Task<User?> GetByIdAsync(int userId)
        {
            return _userFinder.GetByIdAsync(userId);
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            return _userFinder.GetByUsernameAsync(username);
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
