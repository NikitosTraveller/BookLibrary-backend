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

        public BaseController(IUserService userService, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        protected int UserId
        {
            get
            {
                return _userService.GetUserId(Request.Cookies["jwt"], _appSettings.Secret);
            }
        }

        protected AppSettings Settings
        {
            get
            {
                return _appSettings;
            }
        }

        protected IMapper Mapper
        {
            get
            {
                return _mapper;
            }
        }
    }
}
