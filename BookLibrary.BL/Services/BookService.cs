using BookLibrary.BL.Contracts;
using BookLibrary.DAL;
using BookLibrary.DAL.Contracts;
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
        private readonly IUnitOfWork _unitOfWork;

        private readonly IBookFinder _bookFinder;

        private readonly IRepository<Book> _bookRepository;

        public BookService(IUnitOfWork unitOfWork, IBookFinder bookFinder, IRepository<Book> bookRepository)
        {
            _unitOfWork = unitOfWork;
            _bookFinder = bookFinder;
            _bookRepository = bookRepository;
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = _bookFinder.GetById(bookId);
            if (book != null)
            {
                _bookRepository.Delete(book);
                await _unitOfWork.Commit();
            }
        }

        public Task<List<Book>> GetAllBooksAsync()
        {
            return _bookFinder.GetAllBooksAsync();
        }

        public Book? GetBook(int bookId)
        {
            return _bookFinder.GetById(bookId);
        }

        public byte[] GetBookContent(string name, string uploadPath)
        {
            string filePath = Path.Combine(uploadPath, name);
            if (File.Exists(filePath))
            {
                return File.ReadAllBytes(filePath);
            }
            throw new NotImplementedException();
        }

        public async Task<Book?> UploadBookAsync(Book book, string uploadPath, int userId)
        {
            if (book.FormFile != null && book.FormFile.Length > 0)
            {

                Directory.CreateDirectory(uploadPath);
                string filePath = Path.Combine(uploadPath, book.Name);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    book.FormFile.CopyTo(fileStream);
                }

                book.ContentType = book.FormFile.ContentType;
                book.UserId = userId;
                _bookRepository.Create(book);
                await _unitOfWork.Commit();
                return await _bookFinder.GetUploadedBookAsync(book.Id);
            }

            return null;
        }
    }
}
