using AutoMapper;
using DTOs.React.User;
using Mapper.Utils;

namespace Mapper.User
{
    public class UserProfile : Profile
    {
       public UserProfile()
       {
            CreateMap<Entities.React.User, UserDto>()
                .IgnoreAllNonExisting();

            CreateMap<UpdateUserDto, Entities.React.User>()
                .IgnoreAllNonExisting();
        }
    }
}
