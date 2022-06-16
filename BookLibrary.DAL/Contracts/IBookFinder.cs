using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Contracts
{
    public interface IBookFinder
    {
        Task<List<Book>> GetAllBooksAsync();

        Task<Book?> GetUploadedBookAsync(int bookId);

        Task<Book?> GetByIdAsync(int id);
    }
}
