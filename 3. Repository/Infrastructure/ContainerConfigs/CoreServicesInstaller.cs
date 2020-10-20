using EntityFrameworkCore.Contexts;
using EntityFrameworkCore.UnitOfWork;
using Infrastructure.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ContainerConfigs
{
    public static class CoreServicesInstaller
    {
        public static void ConfigureCoreServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GlotechDbContext>(
                option =>
                {
                    option.UseNpgsql(configuration.GetConnectionString("ReactConnectionString"));
                });

            services.AddScoped<IUnitOfWork, UnitOfWork<GlotechDbContext>>();
            services.AddScoped<IUnitOfWork<GlotechDbContext>, UnitOfWork<GlotechDbContext>>();
            services.AddScoped<IRepositoryFactory, UnitOfWork<GlotechDbContext>>();

            services.AddScoped<ModelValidationFilterAttribute>();
            services.AddMemoryCache();
            //services.AddCors();
        }
    }
}
