using AutoMapper;
using BookLibrary.BL.Contracts;
using BookLibrary.Helpers;
using BookLibrary.Models;
using BookLibrary.Services;
using BookLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Mime;

namespace BookLibrary.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {

        private IBookService _bookService;

        private IUserService _userService;

        private IMapper _mapper;

        private IOptions<AppSettings> _jwtSettings;

        public BookController(IBookService bookService, IOptions<AppSettings> jwtSettings, IMapper mapper, IUserService userService)
        {
            _bookService = bookService;
            _mapper = mapper;
            _jwtSettings = jwtSettings;
            _userService = userService;
        }

        [HttpPost("upload")]
        public IActionResult UploadBook([FromForm] FileModel fileModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            int userId = _userService.GetUserId(Request.Cookies["jwt"], _jwtSettings.Value.Secret);

            var book = _mapper.Map<Book>(fileModel);
            var uploadedBook = _bookService.UploadBook(book, userId);
            return Ok(_mapper.Map<BookViewModel>(uploadedBook));
        }

        [HttpGet("books")]
        public IActionResult GetAllBooks()
        {
            var books = _bookService.GetAllBooks();
            return Ok(_mapper.Map<IEnumerable<BookViewModel>>(books));
        }

        [HttpGet("books/{bookId}")]
        public IActionResult GetBook(int bookId)
        {
            var book = _bookService.GetBook(bookId);

            if(book == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookViewModel>(book));
        }

        [HttpPost("download/{bookId}")]
        public IActionResult DownloadBook(int bookId)
        {
            var book = _bookService.GetBook(bookId);

            if(book == null)
            {
                return NotFound();
            }

            return new FileContentResult(book.Content, book.ContentType);
        }

        [HttpDelete("delete/{bookId}")]
        public IActionResult DeleteBook(int bookId)
        {
            _bookService.DeleteBook(bookId);
            return Ok(bookId);
        }
    }
}
