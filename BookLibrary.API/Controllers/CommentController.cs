using AutoMapper;
using BookLibrary.BL.Contracts;
using BookLibrary.Helpers;
using BookLibrary.Models;
using BookLibrary.Services;
using BookLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        private IOptions<AppSettings> _jwtSettings;

        public CommentController(ICommentService commentService, IOptions<AppSettings> jwtSettings, IMapper mapper, IUserService userService)
        {
            _commentService = commentService;
            _mapper = mapper;
            _jwtSettings = jwtSettings;
            _userService = userService;
        }

        [HttpGet("comments/{bookId}")]
        public async Task<IActionResult> GetAllCommentsForBook(int bookId)
        {
            var comments = await _commentService.GetCommentsForBookAsync(bookId);
            return Ok(_mapper.Map<IEnumerable<CommentViewModel>>(comments));
        }

        [HttpPost("post")]
        public async Task<IActionResult> PostComment(CommentModel commentViewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            int userId = _userService.GetUserId(Request.Cookies["jwt"], _jwtSettings.Value.Secret);

            var comment = _mapper.Map<Comment>(commentViewModel);
            var postedComment = await _commentService.PostCommentAsync(comment, userId);
            return Ok(_mapper.Map<CommentViewModel>(postedComment));
        }

        [HttpDelete("delete/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            await _commentService.DeleteCommentAsync(commentId);
            return Ok(commentId);
        }
    }
}
