using HouseholdExpensesTrackerServer21.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HouseholdExpensesTrackerServer21.Common.Repository;
using HouseholdExpensesTrackerServer21.Common.Object;

namespace HouseholdExpensesTrackerServer21.Infrastructure.Repositories
{
    public class EntityFrameworkRepository<TModel> : IRepository<TModel>
        where TModel : class, IEntity
    {
        private readonly IDbContext _context;
        protected readonly DbSet<TModel> _dbSet;

        public EntityFrameworkRepository(IDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TModel>();
        }

        public virtual void Add(TModel entity)
        {
            _dbSet.Add(entity);
        }

        public virtual async Task AddAsync(TModel entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual void Delete(TModel entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void DeleteById(Guid id)
        {
            var entity = this.GetById(id);
            this.Delete(entity);
        }

        public virtual async Task DeleteByIdAsync(Guid id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await this.GetByIdAsync(id, cancellationToken);
            this.Delete(entity);
        }

        public virtual TModel Find(Expression<Func<TModel, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        public virtual ICollection<TModel> FindAll(Expression<Func<TModel, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public virtual async Task<ICollection<TModel>> FindAllAsync(Expression<Func<TModel, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public virtual async Task<TModel> FindAsync(Expression<Func<TModel, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbSet.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual ICollection<TModel> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual async Task<ICollection<TModel>> GetAllAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public virtual TModel GetById(Guid id)
        {
            return _dbSet.Find();
        }

        public virtual async Task<TModel> GetByIdAsync(Guid id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        protected IQueryable<TModel> QueryDb(Expression<Func<TModel, bool>> filter, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy, Func<IQueryable<TModel>, IQueryable<TModel>> includes)
        {
            IQueryable<TModel> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }
    }
}
