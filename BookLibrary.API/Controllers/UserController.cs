using AutoMapper;
using BookLibrary.BL.Contracts;
using BookLibrary.Helpers;
using BookLibrary.Models;
using BookLibrary.Services;
using BookLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookLibrary.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private IMapper _mapper;

        private IOptions<AppSettings> _jwtSettings;

        public UserController(IUserService userService, IOptions<AppSettings> jwtSettings, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("login")]
        public IActionResult Login(AuthenticateRequest model)
        {

            var user = _userService.GetByUsername(model.Username);

            if(user == null || !_userService.ValidatePassword(user.Password, model.Password))
            {
                return BadRequest(new { message = "Invalid credentials" });
            }

            var jwt = JwtHelper.Generate(user.Id, _jwtSettings.Value.Secret, _jwtSettings.Value.LifeTime);

            Response.Cookies.Append("jwt", jwt, new CookieOptions { 
                HttpOnly = true
            });

            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult Registrate(UserViewModel userViewModel)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest(new { message = "Model state is invalid." });
            }

            var result = _userService.CreateUser(_mapper.Map<User>(userViewModel));

            if(result == null)
            {
                return BadRequest(new { message = "Username already exists" });
            }

            return Created("success", result);
        }

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var validatedToken = JwtHelper.Verify(jwt, _jwtSettings.Value.Secret);

                int userId = int.Parse(validatedToken.Issuer);

                var user = _userService.GetById(userId);

                return Ok(user);
            }
            catch(Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new {
                message = "success"
            });
        }

    }
}
