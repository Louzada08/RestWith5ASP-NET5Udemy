using RestWithASPNET.Domain.Entities.Base;
using System.Linq.Expressions;

namespace RestWithASPNET.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
        T Create(T entity);
        T FindById(params object[] keyValues);
        T Update(T entity);
        T Delete(T entity);
        IQueryable<T> QueryableFilter();
        IQueryable<T> QueryableFor(Expression<Func<T, bool>> criterio = null, 
            bool @readonly = false, params Expression<Func<T, object>>[] includes);
        Task<List<T>> FindWithPagedSearch(string query);
        int GetCount(string query);
    }
}
