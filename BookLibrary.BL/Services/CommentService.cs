using BookLibrary.BL.Contracts;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Services
{
    public class CommentService : ICommentService
    {

        private ApplicationContext _context;

        private IBookService _bookService;

        public CommentService(ApplicationContext context, IBookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }

        public void DeleteComment(int commentId)
        {
            var comment = _context.Comments.FirstOrDefault(comm => comm.Id == commentId);
            if(comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Comment> GetCommentsForBook(int bookId)
        {
            return _context.Comments.Where(comm => comm.BookId == bookId).Include(c => c.User);
        }

        public Comment PostComment(Comment comment, int userId)
        {
            var book = _bookService.GetBook(comment.BookId);

            if(book != null)
            {
                comment.Book = book;
                _context.Comments.Add(comment);
                comment.UserId = userId;
                _context.SaveChanges();
                return _context.Comments.OrderByDescending(p => p.Date).Include(c => c.User).FirstOrDefault();
            }

            return null;
        }
    }
}
