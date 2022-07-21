using AutoMapper;
using BookLibrary.API;
using BookLibrary.API.Controllers;
using BookLibrary.API.Requests;
using BookLibrary.API.Responses;
using BookLibrary.BL.Contracts;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BookLibrary.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BookController : BaseController
    {

        private readonly IBookService _bookService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookController"/> class.
        /// </summary>
        /// <param name="bookService">The book service.</param>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="userService">The user service.</param>
        public BookController(
            IBookService bookService, 
            IOptions<AppSettings> appSettings, 
            IMapper mapper, 
            IUserService userService) : base(userService, appSettings, mapper)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Uploads the book.
        /// </summary>
        /// <param name="uploadBookRequest">The upload book request.</param>
        /// <returns></returns>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadBook([FromForm] UploadBookRequest uploadBookRequest)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var book = Mapper.Map<Book>(uploadBookRequest);
            var uploadedBook = await _bookService.UploadBookAsync(book, Settings.StoragePath, UserId);
            return Ok(Mapper.Map<BookResponse>(uploadedBook));
        }

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns></returns>
        [HttpGet("books")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(Mapper.Map<IEnumerable<BookResponse>>(books));
        }

        /// <summary>
        /// Gets the book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns></returns>
        [HttpGet("books/{bookId}")]
        public async Task<IActionResult> GetBook(int bookId)
        {
            var book = await _bookService.GetBookAsync(bookId);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<BookResponse>(book));
        }

        /// <summary>
        /// Downloads the book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns></returns>
        [HttpPost("download/{bookId}")]
        public async Task<IActionResult> DownloadBook(int bookId)
        {
            var book = await _bookService.GetBookAsync(bookId);

            if(book == null)
            {
                return NotFound();
            }

            var content = _bookService.GetBookContent(book.Name, Settings.StoragePath);

            return new FileContentResult(content, book.ContentType);
        }

        /// <summary>
        /// Deletes the book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns></returns>
        [HttpDelete("delete/{bookId}")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            await _bookService.DeleteBookAsync(bookId);
            return Ok(bookId);
        }
    }
}
