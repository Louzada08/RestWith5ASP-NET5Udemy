using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy.Repository.Specific.PersonRepo
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IRepository<Person> _repository;

        public PersonRepository(IRepository<Person> repository)
        {
            _repository = repository;
        }

        public async Task<Person> Create(Person person)
        {
            return await _repository.Create(person);
        }

        public async Task<bool> Exists(long id)
        {
            return await _repository.Exists(id);
        }

        public async Task<List<Person>> FindAll()
        {
            return await _repository.FindAll();
        }

        public async Task<Person> FindById(long id)
        {
            return await _repository.FindById(id);
        }

        public async Task<Person> Update(Person person)
        {
            return await _repository.Update(person);
        }
    }
}
