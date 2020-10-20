using Common.Extentions;
using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.AsQueryable();

            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query;
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public TEntity Insert(TEntity entity)
        {
            SetId(entity);
            CheckAndSetDefaultValues(entity);

            Context.Entry(entity).State = EntityState.Added;

            return Table.Add(entity).Entity;
        }

        private void SetId(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
        }

        private static void CheckAndSetDefaultValues(TEntity entity)
        {
            var createDateProperty = entity.GetType().GetProperty("CreateDate");
            if(createDateProperty != null)
            {
                createDateProperty.SetValue(entity, DateTime.Now);
            }    
        }
    }
}
