using Entities.Interfaces;
using System.Linq;

namespace EntityFrameworkCore.UnitOfWork
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();
    }
}
