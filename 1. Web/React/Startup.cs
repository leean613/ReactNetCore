using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.ContainerConfigs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Reflection;

namespace React
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            CoreServicesInstaller.ConfigureCoreServices(services, Configuration);
            AuthServicesInstaller.ConfigureServicesAuth(services, Configuration);
            ApplicationServicesInstaller.ConfigureApplicationServices(services, Configuration);

            services
                .AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddHangfire(config =>
            {
                config.UsePostgreSqlStorage(Configuration.GetConnectionString("ReactConnectionString"));
            });

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(
                        "v5",
                        new OpenApiInfo
                        {
                            Title = "Glotech React API",
                            Version = "v5",
                            Description = "ASP.NET Core Web API",
                            Contact = new OpenApiContact
                            {
                                Name = "Nguyen Dinh Binh",
                                Email = "ndbinh280697@gmail.com",
                            },
                            License = new OpenApiLicense
                            {
                                Name = "Copyright by Binh Nguyen",
                            }
                        });

                    c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("AllowAll");

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Glotech React Document";
                c.SwaggerEndpoint("v5/swagger.json", "My API V5");
            });

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
