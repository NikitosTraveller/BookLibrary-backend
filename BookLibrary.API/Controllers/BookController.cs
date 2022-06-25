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

        public BookController(
            IBookService bookService, 
            IOptions<AppSettings> appSettings, 
            IMapper mapper, 
            IUserService userService) : base(userService, appSettings, mapper)
        {
            _bookService = bookService;
        }

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

        [HttpGet("books")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(Mapper.Map<IEnumerable<BookResponse>>(books));
        }

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

        [HttpDelete("delete/{bookId}")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            await _bookService.DeleteBookAsync(bookId);
            return Ok(bookId);
        }
    }
}
