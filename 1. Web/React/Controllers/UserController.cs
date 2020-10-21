using Abstractions.Interfaces;
using DTOs.React;
using Infrastructure.ApiControllers;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace React.Controllers
{
    [Produces("application/json")]
    [Route("api/glotech/user")]
    [Authorize]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Tạo mới người dùng
        /// </summary>
        [HttpPost("create")]
        [ModelValidationFilter]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var result = await _userService.CreateUserAsync(dto);
            return Success(result);
        }
    }
}
