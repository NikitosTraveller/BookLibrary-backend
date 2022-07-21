using BookLibrary.BL.Contracts;
using BookLibrary.DAL;
using BookLibrary.DAL.Contracts;
using BookLibrary.Models;

namespace BookLibrary.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IBookFinder _bookFinder;

        private readonly IRepository<Book> _bookRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="bookFinder">The book finder.</param>
        /// <param name="bookRepository">The book repository.</param>
        public BookService(IUnitOfWork unitOfWork, IBookFinder bookFinder, IRepository<Book> bookRepository)
        {
            _unitOfWork = unitOfWork;
            _bookFinder = bookFinder;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Deletes the book asynchronous.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        public async Task DeleteBookAsync(int bookId)
        {
            var book = await _bookFinder.GetByIdAsync(bookId);
            if (book != null)
            {
                _bookRepository.Delete(book);
                await _unitOfWork.Commit();
            }
        }


        /// <summary>
        /// Gets all books asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<List<Book>> GetAllBooksAsync()
        {
            return _bookFinder.GetAllBooksAsync();
        }

        /// <summary>
        /// Gets the book asynchronous.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns></returns>
        public Task<Book?> GetBookAsync(int bookId)
        {
            return _bookFinder.GetByIdAsync(bookId);
        }

        /// <summary>
        /// Gets the content of the book.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="uploadPath">The upload path.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public byte[] GetBookContent(string name, string uploadPath)
        {
            string filePath = Path.Combine(uploadPath, name);
            if (File.Exists(filePath))
            {
                return File.ReadAllBytes(filePath);
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Uploads the book asynchronous.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <param name="uploadPath">The upload path.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
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
