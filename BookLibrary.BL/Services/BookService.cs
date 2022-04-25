using BookLibrary.BL.Contracts;
using BookLibrary.DAL;
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

        private readonly IUnitOfWork _unitOfWork;

        private readonly IFinder<Book> _bookFinder;

        private readonly IRepository<Book> _bookRepository;

        public BookService(ApplicationContext context, IUnitOfWork unitOfWork, IFinder<Book> bookFinder, IRepository<Book> bookRepository)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _bookFinder = bookFinder;
            _bookRepository = bookRepository;
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = await _bookFinder.GetByIdAsync(bookId);
            if (book != null)
            {
                _bookRepository.Delete(book);
                await _unitOfWork.Commit();
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookFinder.GetListAsync(null,
                includes: _ => _.Include(c => c.Comments).Include(c => c.User),
                disableTracking: false);
        }

        public async Task<Book> GetBookAsync(int bookId)
        {
            return await _bookFinder.GetByIdAsync(bookId);
        }

        public async Task<Book> UploadBookAsync(Book book, int userId)
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
                _bookRepository.Create(book);
                await _unitOfWork.Commit();
                return await _context.Books.OrderByDescending(book => book.Date).Include(book => book.User).FirstOrDefault();

            }

            return null;
        }
    }
}
