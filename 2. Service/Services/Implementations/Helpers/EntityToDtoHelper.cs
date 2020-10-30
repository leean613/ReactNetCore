using DTOs.React.User;
using Entities.React;

namespace Services.Implementations.Helpers
{
    public static class EntityToDtoHelper
    {
        public static UserDto ToUserDto(this User entity)
        {
            return entity == null
                ? null
                : new UserDto
                {
                    Id = entity.Id,
                    UserName = entity.UserName,
                    Role = entity.Role
                };
        }
    }
}
