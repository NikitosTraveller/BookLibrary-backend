using BookLibrary.DAL.Finders;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Tests
{
    public class BookDataTests : Tests
    {

        private readonly BookFinder _bookFinder;

        public BookDataTests()
        {
            _bookFinder = new BookFinder(Context);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var books = await _bookFinder.GetAllBooksAsync();
            Assert.AreEqual(books.Count, books.Count);
        }
    }
}