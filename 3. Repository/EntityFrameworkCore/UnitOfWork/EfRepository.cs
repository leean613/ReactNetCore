using Common.Exceptions;
using Common.Extentions;
using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public virtual Task DeleteAsync(Guid id)
        {
            Delete(id);
            return Task.FromResult(0);
        }

        public void Delete(Guid id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            entity = Get(id);

            if (entity != null)
            {
                Delete(entity);
            }

            //Could not found the entity, do nothing.
        }

        private TEntity GetFromChangeTrackerOrNull(Guid id)
        {
            var entry = Context.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<Guid>.Default.Equals(id, ((TEntity)ent.Entity).Id)
                );

            return entry?.Entity as TEntity;
        }

        public void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        public TEntity Get(Guid id)
        {
            var entity = GetAll().FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        public TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
