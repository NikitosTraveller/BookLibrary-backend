﻿using BookLibrary.DAL.Contracts;
using BookLibrary.DAL.DataHelpers;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.DataHelpers
{
    public class BookFinder : Finder<Book>, IBookFinder
    {
        public BookFinder(DbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await Entities.Include(c => c.Comments).Include(c => c.User).ToListAsync();
        }

        public Book? GetUploadedBook(int bookId)
        {
            return Entities.Include(c => c.User).FirstOrDefault(_ => _.Id == bookId);
        }
    }
}
