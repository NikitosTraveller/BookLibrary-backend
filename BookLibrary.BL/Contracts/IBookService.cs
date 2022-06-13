using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.BL.Contracts
{
    public interface IBookService
    {
        Task<Book?> UploadBookAsync(Book book, string path, int userid);

        Book? GetBook(int bookId);

        byte[] GetBookContent(string name, string uploadPath);

        Task DeleteBookAsync(int bookId);

        Task<List<Book>> GetAllBooksAsync();
    }
}
