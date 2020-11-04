using DTOs.React;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResult> LoginAsync(LoginDto dto);

        Task SendMail();
    }
}
