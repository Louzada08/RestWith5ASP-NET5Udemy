using RestWithASPNET.Domain.Entities;

namespace RestWithASPNET.Domain.Interfaces.Repositories
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
    }

    //public interface IPersonRepository
    //{
    //    Task<Person> Create(Person person);
    //    Task<Person> FindById(long id);
    //    Task<List<Person>> FindAll();
    //    Task<Person> Update(Person person);
    //    Task<Person> Disable(long id);
    //    Task<List<Person>> FindByName(string firstName, string lastName);
    //    Task<List<Person>> FindWithPagedSearch(string query);
    //    int GetCount(string query);
    //    Task<bool> Exists(long id);
    //}
}
