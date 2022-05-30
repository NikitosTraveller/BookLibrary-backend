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
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        private readonly IUserService _userService;

        private IMapper _mapper;

        private AppSettings _appSettings;

        public CommentController(ICommentService commentService, IOptions<AppSettings> appSettings, IMapper mapper, IUserService userService)
        {
            _commentService = commentService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _userService = userService;
        }

        [HttpGet("comments/{bookId}")]
        public async Task<IActionResult> GetAllCommentsForBook(int bookId)
        {
            var comments = await _commentService.GetCommentsForBookAsync(bookId);
            return Ok(_mapper.Map<IEnumerable<CommentResponse>>(comments));
        }

        [HttpPost("post")]
        public async Task<IActionResult> PostComment(PostCommentRequest commentViewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            int userId = _userService.GetUserId(Request.Cookies["jwt"], _appSettings.Secret);

            var comment = _mapper.Map<Comment>(commentViewModel);
            var postedComment = await _commentService.PostCommentAsync(comment, userId);
            return Ok(_mapper.Map<CommentResponse>(postedComment));
        }

        [HttpDelete("delete/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            await _commentService.DeleteCommentAsync(commentId);
            return Ok(commentId);
        }
    }
}
