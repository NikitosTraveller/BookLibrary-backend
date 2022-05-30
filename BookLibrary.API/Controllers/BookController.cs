using AutoMapper;
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
    public class BookController : ControllerBase
    {

        private readonly IBookService _bookService;

        private readonly IUserService _userService;

        private IMapper _mapper;

        private AppSettings _appSettings;

        public BookController(IBookService bookService, IOptions<AppSettings> appSettings, IMapper mapper, IUserService userService)
        {
            _bookService = bookService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _userService = userService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadBook([FromForm] UploadBookRequest fileModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            int userId = _userService.GetUserId(Request.Cookies["jwt"], _appSettings.Secret);

            var book = _mapper.Map<Book>(fileModel);
            var uploadedBook = await _bookService.UploadBookAsync(book, _appSettings.StoragePath, userId);
            return Ok(_mapper.Map<BookResponse>(uploadedBook));
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(_mapper.Map<IEnumerable<BookResponse>>(books));
        }

        [HttpGet("books/{bookId}")]
        public IActionResult GetBook(int bookId)
        {
            var book = _bookService.GetBook(bookId);

            if(book == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookResponse>(book));
        }

        [HttpPost("download/{bookId}")]
        public IActionResult DownloadBook(int bookId)
        {
            var book = _bookService.GetBook(bookId);

            if(book == null)
            {
                return NotFound();
            }

            var content = _bookService.GetBookContent(book.Name, _appSettings.StoragePath);

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
