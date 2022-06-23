using BookLibrary.BL.Contracts;
using BookLibrary.DAL;
using BookLibrary.DAL.Contracts;
using BookLibrary.Models;

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
            var comment = await _commentFinder.GetByIdAsync(commentId);

            if(comment != null)
            {

                _commentRepository.Delete(comment);
                await _unitOfWork.Commit();
            }
        }

        public Task<List<Comment>> GetCommentsForBookAsync(int bookId)
        {
            return _commentFinder.GetCommentsForBookAsync(bookId);
        }

        public async Task<Comment?> PostCommentAsync(Comment comment, int userId)
        {
            var book = await _bookFinder.GetByIdAsync(comment.BookId);

            if(book != null)
            {
                comment.Book = book;
                _commentRepository.Create(comment);
                comment.UserId = userId;
                await _unitOfWork.Commit();
                return await _commentFinder.GetPostedCommentAsync(comment.Id);
            }

            return null;
        }
    }
}
