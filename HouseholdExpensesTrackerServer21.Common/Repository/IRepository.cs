using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Common.Repository
{
    public interface IRepository<TModel>
    {

        #region synchronous

        ICollection<TModel> FindAll(Expression<Func<TModel, bool>> predicate);

        TModel Find(Expression<Func<TModel, bool>> predicate);

        ICollection<TModel> GetAll();

        TModel GetById(Guid id);

        void Add(TModel entity);

        void Delete(TModel entity);

        void DeleteById(Guid id);

        int SaveChanges();

        #endregion

        #region asynchronous

        Task AddAsync(TModel entity, CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        Task<ICollection<TModel>> FindAllAsync(Expression<Func<TModel, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<TModel> FindAsync(Expression<Func<TModel, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<ICollection<TModel>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<TModel> GetByIdAsync(Guid id,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        #endregion
    }
}
