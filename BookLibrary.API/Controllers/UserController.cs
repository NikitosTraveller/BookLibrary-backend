using AutoMapper;
using BookLibrary.API;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private IMapper _mapper;

        private AppSettings _appSettings;

        public UserController(IUserService userService, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost("login")]
        public IActionResult Login(AuthenticateRequest model)
        {

            var user = _userService.GetByUsername(model.Username);

            if(user == null || !_userService.ValidatePassword(user.Password, model.Password))
            {
                return BadRequest(new { message = "Invalid credentials" });
            }

            var jwt = JwtHelper.Generate(user.Id, _appSettings.Secret, _appSettings.LifeTime);

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

            var result = await _userService.CreateUserAsync(_mapper.Map<User>(registerUserRequest));

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

                var validatedToken = JwtHelper.Verify(jwt, _appSettings.Secret);

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
