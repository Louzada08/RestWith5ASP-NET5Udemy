using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Repository.Specific.PersonRepo
{
    public interface IPersonRepository
    {
        Task<Person> Create(Person person);
        Task<Person> FindById(long id);
        Task<List<Person>> FindAll();
        Task<Person> Update(Person person);
        Task<bool> Exists(long id);
    }
}
