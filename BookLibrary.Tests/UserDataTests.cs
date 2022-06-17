using BookLibrary.DAL.Finders;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Tests
{
    public class UserDataTests : Tests
    {
        public UserDataTests()
        {

        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var finder = new BookFinder(Context);
            var books = await finder.GetAllBooksAsync();
            Assert.AreEqual(books.Count, books.Count);
        }
    }
}