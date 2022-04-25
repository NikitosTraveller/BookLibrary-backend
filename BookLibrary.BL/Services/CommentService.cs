using BookLibrary.BL.Contracts;
using BookLibrary.DAL;
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

        private readonly IBookService _bookService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IFinder<Comment> _commentFinder;

        private readonly IFinder<Book> _bookFinder;

        private readonly IRepository<Comment> _commentRepository;

        public CommentService(ApplicationContext context, IBookService bookService, IUnitOfWork unitOfWork, IFinder<Comment> commentFinder, IFinder<Book> bookFinder, IRepository<Comment> commentRepository)
        {
            _context = context;
            _bookService = bookService;
            _unitOfWork = unitOfWork;
            _commentFinder = commentFinder;
            _bookFinder = bookFinder;
            _commentRepository = commentRepository;
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = _context.Comments.FirstOrDefault(comm => comm.Id == commentId);
            if(comment != null)
            {

                _commentRepository.Delete(comment);
                await _unitOfWork.Commit();
            }
        }

        public async Task<IEnumerable<Comment>> GetCommentsForBookAsync(int bookId)
        {
            return await _commentFinder.GetListAsync(_ => _.BookId == bookId,
                includes: _ => _.Include(c => c.User),
                disableTracking: false);
        }

        public async Task<Comment> PostCommentAsync(Comment comment, int userId)
        {
            var book = await _bookFinder.GetByIdAsync(comment.BookId);

            if(book != null)
            {
                comment.Book = book;
                _commentRepository.Create(comment);
                comment.UserId = userId;
                await _unitOfWork.Commit();
                return _context.Comments.OrderByDescending(p => p.Date).Include(c => c.User).FirstOrDefault();
            }

            return null;
        }
    }
}
