using BookLibrary.BL.Contracts;
using BookLibrary.DAL;
using BookLibrary.DAL.Contracts;
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
        private readonly IBookService _bookService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ICommentFinder _commentFinder;

        private readonly IBookFinder _bookFinder;

        private readonly IRepository<Comment> _commentRepository;

        public CommentService(IBookService bookService, IUnitOfWork unitOfWork, ICommentFinder commentFinder, IBookFinder bookFinder, IRepository<Comment> commentRepository)
        {
            _bookService = bookService;
            _unitOfWork = unitOfWork;
            _commentFinder = commentFinder;
            _bookFinder = bookFinder;
            _commentRepository = commentRepository;
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = _commentFinder.GetById(commentId);

            if(comment != null)
            {

                _commentRepository.Delete(comment);
                await _unitOfWork.Commit();
            }
        }

        public async Task<IEnumerable<Comment>> GetCommentsForBookAsync(int bookId)
        {
            return await _commentFinder.GetCommentsForBookAsync(bookId);
        }

        public async Task<Comment?> PostCommentAsync(Comment comment, int userId)
        {
            var book = _bookFinder.GetById(comment.BookId);

            if(book != null)
            {
                comment.Book = book;
                _commentRepository.Create(comment);
                comment.UserId = userId;
                await _unitOfWork.Commit();
                return _commentFinder.GetPostedComment(comment.Id);
            }

            return null;
        }
    }
}
