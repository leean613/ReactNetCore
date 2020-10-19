using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCore.Contexts
{
    public abstract class GlotechDbContext : DbContext
    {
        protected GlotechDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
