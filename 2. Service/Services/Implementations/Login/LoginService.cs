using Abstractions.Interfaces;
using Abstractions.Interfaces.Mail;
using Common.Runtime.Security;
using Common.Unknown;
using DTOs.React;
using DTOs.Share;
using Entities.React;
using EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Services.Implementations.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;

        private IRepository<User> _userRepository => _unitOfWork.GetRepository<User>();

        private readonly ISendMailService _sendMailService;

        public LoginService(IUnitOfWork unitOfWork, ISendMailService sendMailService)
        {
            _unitOfWork = unitOfWork;
            _sendMailService = sendMailService;
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

        public async Task SendMail()
        {
            var attachmentFile = new FileDescription[0];
            attachmentFile = attachmentFile.Append(new FileDescription
            {
                FileName = "BinhNguyen attachment file name",
                Data = await File.ReadAllBytesAsync(@"E:\\config_background.txt")
            }).ToArray();

            var sendEmailInput = new SendEmailInput
            {
                Content = string.Empty,
                Subject = string.Empty,
                RecipientEmails = new string[] { "ndbinh280697@gmail.com" },
                AttachmentFiles = attachmentFile
            };

            await _sendMailService.SendMails(sendEmailInput);
        }
    }
}
