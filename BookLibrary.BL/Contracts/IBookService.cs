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
        Book UploadBook(Book book, int userid);

        Book GetBook(int bookId);

        void DeleteBook(int bookId);

        IEnumerable<Book> GetAllBooks();
    }
}
