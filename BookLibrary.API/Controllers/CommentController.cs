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
    [Route("[controller]")]
    [ApiController]
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;

        public CommentController(
            ICommentService commentService,
            IOptions<AppSettings> appSettings, 
            IMapper mapper, 
            IUserService userService) : base(userService, appSettings, mapper)
        {
            _commentService = commentService;
        }

        [HttpGet("comments/{bookId}")]
        public async Task<IActionResult> GetAllCommentsForBook(int bookId)
        {
            var comments = await _commentService.GetCommentsForBookAsync(bookId);
            return Ok(Mapper.Map<IEnumerable<CommentResponse>>(comments));
        }

        [HttpPost("post")]
        public async Task<IActionResult> PostComment(PostCommentRequest postCommentRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var comment = Mapper.Map<Comment>(postCommentRequest);
            var postedComment = await _commentService.PostCommentAsync(comment, UserId);
            return Ok(Mapper.Map<CommentResponse>(postedComment));
        }

        [HttpDelete("delete/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            await _commentService.DeleteCommentAsync(commentId);
            return Ok(commentId);
        }
    }
}
