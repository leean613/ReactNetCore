using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EntityFrameworkCore.Contexts
{
    public class ReactDbContextFactory : IDesignTimeDbContextFactory<ReactDbContext>
    {
        public ReactDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ReactDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("ReactConnectionString"));

            return new ReactDbContext(optionsBuilder.Options);
        }
    }
}
