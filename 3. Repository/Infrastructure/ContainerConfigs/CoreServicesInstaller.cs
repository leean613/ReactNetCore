﻿using AutoMapper;
using Common.Configurations;
using EntityFrameworkCore.Contexts;
using EntityFrameworkCore.UnitOfWork;
using FluentValidation.AspNetCore;
using Infrastructure.Filters;
using Mapper.User;
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            services.AddAutoMapper(typeof(UserProfile).Assembly);

            services.AddMvcCore(
                    options =>
                    {
                        options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                    }
                )
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining(typeof(IValidator)));

            services.Configure<SmtpConfig>(configuration.GetSection(nameof(SmtpConfig)));

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
        }
    }
}
