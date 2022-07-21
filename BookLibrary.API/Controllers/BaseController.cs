using AutoMapper;
using BookLibrary.BL.Contracts;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BookLibrary.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly AppSettings _appSettings;

        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="mapper">The mapper.</param>
        public BaseController(IUserService userService, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        protected int UserId
        {
            get
            {
                return _userService.GetUserId(Request.Cookies["jwt"], _appSettings.Secret);
            }
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        protected AppSettings Settings
        {
            get
            {
                return _appSettings;
            }
        }

        /// <summary>
        /// Gets the mapper.
        /// </summary>
        /// <value>
        /// The mapper.
        /// </value>
        protected IMapper Mapper
        {
            get
            {
                return _mapper;
            }
        }
    }
}
