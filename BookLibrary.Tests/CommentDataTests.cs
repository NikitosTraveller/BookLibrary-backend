using BookLibrary.DAL;
using BookLibrary.DAL.Contracts;
using BookLibrary.DAL.Finders;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Tests
{
    public class CommentDataTests : Tests
    {
        private readonly CommentFinder _commentFinder;

        private readonly IRepository<Comment> _commentRepository;

        private readonly IRepository<User> _userRepository;

        public CommentDataTests(IUnitOfWork unitOfWork, IRepository<Comment> commentRepository, IRepository<User> userRepository) : base(unitOfWork)
        {
            _commentFinder = new CommentFinder(Context);
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            //var books = await commentFinder.;
            Assert.AreEqual(1, 1);
        }
    }
}