﻿using BookLibrary.BL.Contracts;
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

        private readonly IFinder<Book> _bookFinder;

        private readonly IRepository<Book> _bookRepository;

        public BookService(IUnitOfWork unitOfWork, IFinder<Book> bookFinder, IRepository<Book> bookRepository)
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

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookFinder.Entities.Include(c => c.Comments).Include(c => c.User).ToListAsync();
        }

        public Book? GetBook(int bookId)
        {
            return _bookFinder.GetById(bookId);
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

                book.UserId = userId;
                _bookRepository.Create(book);
                await _unitOfWork.Commit();
                return _bookFinder.Entities.Include(c => c.User).FirstOrDefault(_ => _.Id == book.Id);
            }

            return null;
        }
    }
}
