using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy.Repository.Specific.PersonRepo
{
    public interface IPersonRepository
    {
        Task<Person> Create(Person person);
        Task<Person> FindById(long id);
        Task<List<Person>> FindAll();
        Task<Person> Update(Person person);
        Task<Person> Disable(long id);
        Task<List<Person>> FindByName(string firstName, string lastName);
        Task<bool> Exists(long id);
    }
}
