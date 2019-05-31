using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OPS_API.Domain.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> ListAllAsync();
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindByIdAsync(Guid id);
        Task AddAsync(TEntity user);
    }
}