using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

        int Complete(bool ensureAutoHistory = false);

        Task<int> CompleteAsync(bool ensureAutoHistory = false);

        int ExecuteSqlRaw(string sql, params object[] parameters);

        IQueryable<TEntity> FromSqlRaw<TEntity>(string sql, params object[] parameters) where TEntity : class;
    }
}
