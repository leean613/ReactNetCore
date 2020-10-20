using Abstractions.Interfaces;
using Common.Unknown;
using DTOs.React;
using Infrastructure.ApiControllers;
using Infrastructure.ApiResults;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace React.Controllers
{
    [Produces("application/json")]
    [Route("api/glotech")]
    [Authorize]
    public class LoginController : ApiControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;

        public LoginController(ITokenService tokenService, ILoginService loginService)
        {
            _tokenService = tokenService;
            _loginService = loginService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ModelValidationFilter]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var loginResult = await _loginService.LoginAsync(dto);

            switch (loginResult.Result)
            {
                case LoginResultType.InvalidUserNameOrPassword:
                    return BadRequest(ApiErrorCodes.UserLoginInvalidUserNameOrPassword, "Invalid username or password");

                case LoginResultType.Success:
                    var jwtToken = await _tokenService.RequestTokenAsync(loginResult.Identity);
                    return Success(jwtToken);

                default:
                    throw new ArgumentOutOfRangeException();
            }    
        }
    }
}
