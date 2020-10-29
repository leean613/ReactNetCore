using DTOs.React;
using DTOs.React.User;
using DTOs.Share;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(CreateUserDto dto);

        Task<IPagedResultDto<UserDto>> FilterUsersAsync(PagedResultRequestDto pagedResultRequest, FilterUsersDto filter);

        Task<UserDto> GetUserAsync(Guid id);

        Task DeleteUserAsync(Guid id);
    }
}
