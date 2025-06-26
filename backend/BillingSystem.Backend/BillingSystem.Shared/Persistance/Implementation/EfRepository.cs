using BillingSystem.Shared.Persistance.Entity;
using BillingSystem.Shared.Persistance.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem.Shared.Persistance.Implementation
{
    public class EfRepository<T, TContext> : IRepository<T>
    where T : TEntity, IEntity
    where TContext : DbContext
    {
        protected readonly TContext Context;
        public EfRepository(TContext context) => Context = context;

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate) =>
            await Context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>>? predicate = null) =>
            predicate == null
                ? await Context.Set<T>().ToListAsync()
                : await Context.Set<T>().Where(predicate).ToListAsync();

        public async Task AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }

        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            Context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
    }
}
