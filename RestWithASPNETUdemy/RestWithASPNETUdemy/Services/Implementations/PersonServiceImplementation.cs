﻿using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository.Specific.PersonRepo;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonServiceImplementation(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<Person> Create(Person person)
        {
            return await _repository.Create(person);
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
