using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EntityFrameworkCore.UnitOfWork
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        public DbContext Context;

        public virtual DbSet<TEntity> Table => Context.Set<TEntity>();

        public EfRepository(DbContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> GetAll()
        {
            return Table;
        }
    }
}
