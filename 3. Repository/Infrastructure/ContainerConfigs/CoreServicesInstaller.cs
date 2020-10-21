using EntityFrameworkCore.Contexts;
using EntityFrameworkCore.UnitOfWork;
using FluentValidation.AspNetCore;
using Infrastructure.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IValidator = DTOs.Validators.IValidator;

namespace Infrastructure.ContainerConfigs
{
    public static class CoreServicesInstaller
    {
        public static void ConfigureCoreServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvcCore()
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining(typeof(IValidator)));

            services.AddDbContext<ReactDbContext>(
                option =>
                {
                    option.UseNpgsql(configuration.GetConnectionString("ReactConnectionString"));
                });

            services.AddScoped<IUnitOfWork, UnitOfWork<ReactDbContext>>();
            services.AddScoped<IUnitOfWork<ReactDbContext>, UnitOfWork<ReactDbContext>>();
            services.AddScoped<IRepositoryFactory, UnitOfWork<ReactDbContext>>();

            services.AddScoped<ModelValidationFilterAttribute>();
            services.AddMemoryCache();
            //services.AddCors();
        }
    }
}
