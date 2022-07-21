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

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="userFinder">The user finder.</param>
        /// <param name="userRepository">The user repository.</param>
        public UserService(IUnitOfWork unitOfWork, IUserFinder userFinder, IRepository<User> userRepository)
        {
            _unitOfWork = unitOfWork;
            _userFinder = userFinder;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Creates the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task<User?> GetByIdAsync(int userId)
        {
            return _userFinder.GetByIdAsync(userId);
        }

        /// <summary>
        /// Gets the by username asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public Task<User?> GetByUsernameAsync(string username)
        {
            return _userFinder.GetByUsernameAsync(username);
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <param name="jwt">The JWT.</param>
        /// <param name="secret">The secret.</param>
        /// <returns></returns>
        public int GetUserId(string jwt, string secret)
        {
            var validatedToken = JwtHelper.Verify(jwt, secret);
            return int.Parse(validatedToken.Issuer);
        }

        /// <summary>
        /// Validates the password.
        /// </summary>
        /// <param name="realPassword">The real password.</param>
        /// <param name="passwordToVerify">The password to verify.</param>
        /// <returns></returns>
        public bool ValidatePassword(string realPassword, string passwordToVerify)
        {
            return BCrypt.Net.BCrypt.Verify(passwordToVerify, realPassword);
        }
    }
}
