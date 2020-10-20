using DTOs.React;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Abstractions.Interfaces
{
    public interface ITokenService
    {
        Task<JwtTokenResultDto> RequestTokenAsync(ClaimsIdentity identity);
    }
}
