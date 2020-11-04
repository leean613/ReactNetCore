using Abstractions.Interfaces;
using Abstractions.Interfaces.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Implementations;
using Services.Implementations.Mail;

namespace Infrastructure.ContainerConfigs
{
    public static class ApplicationServicesInstaller
    {
        public static void ConfigureApplicationServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITokenService, JwtTokenService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISendMailService, SendMailService>();
        }
    }
}
