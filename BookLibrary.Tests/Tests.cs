using BookLibrary.DAL.Contracts;
using BookLibrary.DAL.Finders;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Tests
{
    public class Tests
    {
        public readonly DbContextOptions<ApplicationContext> dbContextOptions;

        public readonly ApplicationContext context;

        private readonly IUnitOfWork _unitOfWork;

        public Tests(IUnitOfWork unitOfWork)
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "booklibrarydb")
                .Options;

            context = new ApplicationContext(dbContextOptions);

            _unitOfWork = unitOfWork;
        }

        protected ApplicationContext Context
        {
            get
            {
                return context;
            }
        }

        protected IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }
    }
}