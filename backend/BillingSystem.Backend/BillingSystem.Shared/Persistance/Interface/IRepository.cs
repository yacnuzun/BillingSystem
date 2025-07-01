using BillingSystem.Shared.Persistance.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem.Shared.Persistance.Interface
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>>? predicate = null);
        Task AddAsync(T entity);
        Task<T> AddAsyncT(T entity);
        void Update(T entity);
        void SoftDelete(T entity);
        void Delete(T entity);
    }
}
