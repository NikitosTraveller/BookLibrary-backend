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

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentController"/> class.
        /// </summary>
        /// <param name="commentService">The comment service.</param>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="userService">The user service.</param>
        public CommentController(
            ICommentService commentService,
            IOptions<AppSettings> appSettings, 
            IMapper mapper, 
            IUserService userService) : base(userService, appSettings, mapper)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Gets all comments for book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns></returns>
        [HttpGet("comments/{bookId}")]
        public async Task<IActionResult> GetAllCommentsForBook(int bookId)
        {
            var comments = await _commentService.GetCommentsForBookAsync(bookId);
            return Ok(Mapper.Map<IEnumerable<CommentResponse>>(comments));
        }

        /// <summary>
        /// Posts the comment.
        /// </summary>
        /// <param name="postCommentRequest">The post comment request.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the comment.
        /// </summary>
        /// <param name="updateCommentRequest">The update comment request.</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateComment(PostCommentRequest updateCommentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var comment = Mapper.Map<Comment>(updateCommentRequest);
            var updatedComment = await _commentService.UpdateCommentAsync(comment, UserId);
            return Ok(Mapper.Map<CommentResponse>(updatedComment));
        }

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        [HttpDelete("delete/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            await _commentService.DeleteCommentAsync(commentId);
            return Ok(commentId);
        }
    }
}
