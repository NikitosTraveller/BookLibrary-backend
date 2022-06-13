﻿using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Contracts
{
    public interface ICommentFinder : IFinder<Comment>
    {
        Task<List<Comment>> GetCommentsForBookAsync(int bookId);

        Task<Comment?> GetPostedCommentAsync(int commentId);
    }
}
