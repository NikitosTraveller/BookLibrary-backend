using BookLibrary.DAL.Finders;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Tests
{
    public class Tests
    {
        public readonly DbContextOptions<ApplicationContext> dbContextOptions;

        public readonly ApplicationContext context;

        public Tests()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "booklibrarydb")
                .Options;

            context = new ApplicationContext(dbContextOptions); 
        }

        protected ApplicationContext Context
        {
            get
            {
                return context;
            }
        }
    }
}