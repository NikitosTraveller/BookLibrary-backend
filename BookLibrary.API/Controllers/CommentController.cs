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
        private ICommentService _commentService;

        private IUserService _userService;

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
        public IActionResult GetAllCommentsForBook(int bookId)
        {
            var comments = _commentService.GetCommentsForBook(bookId);
            return Ok(_mapper.Map<IEnumerable<CommentViewModel>>(comments));
        }

        [HttpPost("post")]
        public IActionResult PostComment(CommentModel commentViewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            int userId = _userService.GetUserId(Request.Cookies["jwt"], _jwtSettings.Value.Secret);

            var comment = _mapper.Map<Comment>(commentViewModel);
            var postedComment = _commentService.PostComment(comment, userId);
            return Ok(_mapper.Map<CommentViewModel>(postedComment));
        }

        [HttpDelete("delete/{commentId}")]
        public IActionResult DeleteComment(int commentId)
        {
            _commentService.DeleteComment(commentId);
            return Ok(commentId);
        }
    }
}
