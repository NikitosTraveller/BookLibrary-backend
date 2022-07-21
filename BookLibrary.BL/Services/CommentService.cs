using BookLibrary.BL.Contracts;
using BookLibrary.DAL;
using BookLibrary.DAL.Contracts;
using BookLibrary.Models;

namespace BookLibrary.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICommentFinder _commentFinder;

        private readonly IBookFinder _bookFinder;

        private readonly IRepository<Comment> _commentRepository;

        private readonly ApplicationContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="commentFinder">The comment finder.</param>
        /// <param name="bookFinder">The book finder.</param>
        /// <param name="commentRepository">The comment repository.</param>
        /// <param name="context">The context.</param>
        public CommentService(IUnitOfWork unitOfWork, ICommentFinder commentFinder, IBookFinder bookFinder, IRepository<Comment> commentRepository, ApplicationContext context)
        {
            _unitOfWork = unitOfWork;
            _commentFinder = commentFinder;
            _bookFinder = bookFinder;
            _commentRepository = commentRepository;
            _context = context;
        }

        /// <summary>
        /// Deletes the comment asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await _commentFinder.GetByIdAsync(commentId);

            if(comment != null)
            {

                _commentRepository.Delete(comment);
                await _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Gets the comments for book asynchronous.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns></returns>
        public Task<List<Comment>> GetCommentsForBookAsync(int bookId)
        {
            return _commentFinder.GetCommentsForBookAsync(bookId);
        }

        /// <summary>
        /// Posts the comment asynchronous.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the comment asynchronous.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<Comment?> UpdateCommentAsync(Comment comment, int userId)
        {
            _context.Attach(comment);
            _context.Entry(comment).State = true;
            await _unitOfWork.Commit();
        }
    }
}
