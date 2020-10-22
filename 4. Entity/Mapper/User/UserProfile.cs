using AutoMapper;
using DTOs.React;
using Mapper.Utils;

namespace Mapper.User
{
    public class UserProfile : Profile
    {
       public UserProfile()
       {
            CreateMap<Entities.React.User, UserDto>()
                .IgnoreAllNonExisting();
       }
    }
}
