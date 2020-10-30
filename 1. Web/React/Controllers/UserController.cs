using Abstractions.Interfaces;
using DTOs.React;
using DTOs.React.User;
using DTOs.Share;
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
        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var result = await _userService.CreateUserAsync(dto);
            return Success(result);
        }

        /// <summary>
        /// Danh sách người dùng
        /// </summary>
        [HttpGet("{page:int}/{pageSize:int}")]
        [ModelValidationFilter]
        public async Task<IActionResult> Filter(
            [FromRoute] int page,
            [FromRoute] int pageSize,
            [FromQuery] string searchTerm)
        {
            var result = await _userService.FilterUsersAsync(
                new PagedResultRequestDto(page, pageSize),
                new FilterUsersDto
                {
                    SearchTerm = searchTerm
                });

            return Success(result);
        }

        /// <summary>
        /// Thông tin người dùng
        /// </summary>
        [HttpGet("{id:guid}")]
        [ModelValidationFilter]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _userService.GetUserAsync(id);
            return Success(result);
        }

        /// <summary>
        /// Chỉnh sửa người dùng
        /// </summary>
        [HttpPut]
        [ModelValidationFilter]
        public async Task<IActionResult> Edit([FromBody] UpdateUserDto dto)
        {
            var result = await _userService.UpdateUserAsync(dto);
            return Success(result);
        }

        /// <summary>
        /// Xóa người dùng
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ModelValidationFilter]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return Success();
        }
    }
}
