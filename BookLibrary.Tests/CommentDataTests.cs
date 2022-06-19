using BookLibrary.DAL.Finders;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Tests
{
    public class CommentDataTests : Tests
    {
        private readonly CommentFinder _commentFinder;

        public CommentDataTests()
        {
            _commentFinder = new CommentFinder(Context);
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