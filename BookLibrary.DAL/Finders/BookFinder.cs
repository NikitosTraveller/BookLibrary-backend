using BookLibrary.DAL.Contracts;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Finders
{
    public class BookFinder : Finder<Book>, IBookFinder
    {
        public BookFinder(ApplicationContext context) : base(context)
        {

        }

        public Task<List<Book>> GetAllBooksAsync()
        {
            return Entities.Include(c => c.Comments).Include(c => c.User).ToListAsync();
        }

        public Task<Book?> GetByIdAsync(int id)
        {
            return Entities.FirstOrDefaultAsync(book => book.Id == id);
        }

        public Task<Book?> GetUploadedBookAsync(int bookId)
        {
            return Entities.Include(c => c.User).FirstOrDefaultAsync(book => book.Id == bookId);
        }
    }
}
