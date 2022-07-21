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

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="mapper">The mapper.</param>
        public UserController(
            IUserService userService, 
            IOptions<AppSettings> appSettings, 
            IMapper mapper) : base(userService, appSettings, mapper)
        {
            _userService = userService;
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Registrates the specified register user request.
        /// </summary>
        /// <param name="registerUserRequest">The register user request.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns></returns>
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
