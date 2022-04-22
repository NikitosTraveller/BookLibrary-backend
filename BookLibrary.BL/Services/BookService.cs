using BookLibrary.BL.Contracts;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookLibrary.Services
{
    public class BookService : IBookService
    {

        private ApplicationContext _context;

        public BookService(ApplicationContext context)
        {
            _context = context;
        }

        public void DeleteBook(int bookId)
        {
            var book = _context.Books.FirstOrDefault(book => book.Id == bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.Include(book => book.Comments).Include(book => book.User).ToList();
        }

        public Book GetBook(int bookId)
        {
            return _context.Books.FirstOrDefault(book => book.Id == bookId);
        }

        public Book UploadBook(Book book, int userId)
        {
            if (book.FormFile != null && book.FormFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    book.FormFile.CopyTo(ms);
                    book.Content = ms.ToArray();
                    book.ContentType = book.FormFile.ContentType;
                }

                book.UserId = userId;
                _context.Books.Add(book);
                _context.SaveChanges();
                return _context.Books.OrderByDescending(book => book.Date).Include(book => book.User).FirstOrDefault();

            }

            return null;
        }
    }
}
