using Abstractions.Interfaces;
using Common.Runtime.Security;
using Common.Unknown;
using DTOs.React;
using Entities.React;
using EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Services.Implementations.Helpers;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;

        private IRepository<User> _userRepository => _unitOfWork.GetRepository<User>();

        public LoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResult> LoginAsync(LoginDto dto)
        {
            var userToVerify = await GetUserByUserNameAsync(dto.UserName);

            if (userToVerify == null)
            {
                return new LoginResult(LoginResultType.InvalidUserNameOrPassword);
            }

            if (!LoginHelper.CheckPassword(dto.Password, userToVerify.Password))
            {
                return new LoginResult(LoginResultType.InvalidUserNameOrPassword);
            }

            var claimIdentity = GenerateClaimsIdentity(userToVerify);
            return new LoginResult(claimIdentity);
        }

        private async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserName == userName);
        }

        private ClaimsIdentity GenerateClaimsIdentity(User user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(GlotechClaimTypes.UserId, user.Id.ToString()),
                new Claim(GlotechClaimTypes.UserName, user.UserName),
                new Claim(GlotechClaimTypes.Role, user.Role.ToString()),
            };

            return new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), userClaims);
        }
    }
}
