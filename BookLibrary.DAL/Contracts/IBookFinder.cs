using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Contracts
{
    public interface IBookFinder: IFinder<Book>
    {
        Task<List<Book>> GetAllBooksAsync();

        Book? GetUploadedBook(int bookId);
    }
}
