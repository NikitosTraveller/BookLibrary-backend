using BookLibrary.DAL;
using BookLibrary.DAL.Contracts;
using BookLibrary.DAL.Finders;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Tests
{
    public class UserDataTests : Tests
    {
        private readonly UserFinder _userFinder;

        private readonly IRepository<User> _userRepository;

        public UserDataTests(IUnitOfWork unitOfWork, IRepository<User> userRepository) : base(unitOfWork)
        {
            _userFinder = new UserFinder(Context);
            _userRepository = userRepository;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            Assert.AreEqual(1, 1);
        }
    }
}