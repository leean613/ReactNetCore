using Entities.React;
using Entities.ReactConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Contexts
{
    public class ReactDbContext : GlotechDbContext
    {
        public ReactDbContext(DbContextOptions<ReactDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }

        public DbSet<User> Users { get; set; }
    }
}
