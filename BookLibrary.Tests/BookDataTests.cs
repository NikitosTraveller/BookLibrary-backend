using BookLibrary.DAL;
using BookLibrary.DAL.Contracts;
using BookLibrary.DAL.Finders;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Tests
{
    [TestFixture]
    public class BookDataTests : Tests
    {

        private readonly BookFinder _bookFinder;

        private readonly IRepository<Book> _bookRepository;

        private readonly IRepository<User> _userRepository;

        public BookDataTests(IRepository<Book> bookRepository, IRepository<User> userRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _bookFinder = new BookFinder(Context);
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task CreateBookTest()
        {
            var user = new User()
            {
                FirstName = "ForstName",
                LastName = "LastName",
                Username = "Username",
                Password = "Password"
            };

            _userRepository.Create(user);

            await UnitOfWork.Commit();

            var book = new Book()
            {
                Name = "Test Name",
                ContentType = "application/pdf",
                UserId = 1,
                Date = DateTime.Now
            };

            _bookRepository.Create(book);

            await UnitOfWork.Commit();

            var books = await _bookFinder.GetAllBooksAsync();
            Assert.AreEqual(1, books.Count);
        }

        [Test]
        public async Task CheckBooksCountTest()
        {
            var books = await _bookFinder.GetAllBooksAsync();
            Assert.AreEqual(0, books.Count);
        }
    }
}