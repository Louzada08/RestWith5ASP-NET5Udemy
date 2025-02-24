using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Base;

namespace RestWithASPNETUdemy.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> Create(T item);
        Task<T> FindById(long id);
        Task<List<T>> FindAll();
        Task<T> Update(T item);
        Task Delete(long id);
        Task<bool> Exists(long id);
    }
}
