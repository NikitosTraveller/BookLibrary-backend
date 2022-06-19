using BookLibrary.DAL.Finders;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Tests
{
    public class UserDataTests : Tests
    {
        private readonly UserFinder _userFinder;
        public UserDataTests()
        {
            _userFinder = new UserFinder(Context);
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