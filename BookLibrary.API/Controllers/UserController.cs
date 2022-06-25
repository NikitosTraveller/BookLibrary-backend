using AutoMapper;
using BookLibrary.API;
using BookLibrary.API.Controllers;
using BookLibrary.API.Requests;
using BookLibrary.BL.Contracts;
using BookLibrary.Helpers;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BookLibrary.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService, 
            IOptions<AppSettings> appSettings, 
            IMapper mapper) : base(userService, appSettings, mapper)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthenticateRequest model)
        {

            var user = await _userService.GetByUsernameAsync(model.Username);

            if(user == null || !_userService.ValidatePassword(user.Password, model.Password))
            {
                return BadRequest(new { message = "Invalid credentials" });
            }

            var jwt = JwtHelper.Generate(user.Id, Settings.Secret, Settings.LifeTime);

            Response.Cookies.Append("jwt", jwt, new CookieOptions { 
                HttpOnly = true
            });

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registrate(RegisterUserRequest registerUserRequest)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest(new { message = "Model state is invalid." });
            }

            var result = await _userService.CreateUserAsync(Mapper.Map<User>(registerUserRequest));

            if(result == null)
            {
                return BadRequest(new { message = "Username already exists" });
            }

            return Created("success", result);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var validatedToken = JwtHelper.Verify(jwt, Settings.Secret);

                int userId = int.Parse(validatedToken.Issuer);

                var user = await _userService.GetByIdAsync(userId);

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
