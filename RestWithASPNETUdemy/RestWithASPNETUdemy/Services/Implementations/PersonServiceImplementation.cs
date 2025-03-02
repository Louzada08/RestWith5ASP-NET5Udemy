using RestWithASPNETUdemy.Data.Converter.Implementations;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository.Specific.PersonRepo;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonServiceImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public async Task<PersonVO> Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = await _repository.Create(personEntity);

            return _converter.Parse(personEntity);
        }

        public async Task<PersonVO> Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = await _repository.Update(personEntity);

            return _converter.Parse(personEntity);
        }

        public async Task<List<PersonVO>> FindAll()
        {
            return _converter.Parse(await _repository.FindAll());
        }

        public async Task<PersonVO> FindById(long id)
        {
            return  _converter.Parse(await _repository.FindById(id));
        }

        public async Task<List<PersonVO>> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(await _repository.FindByName(firstName, lastName));
        }

        public async Task<PersonVO> Disable(long id)
        {
            var personEntity = _converter.Parse(await _repository.Disable(id));
            return personEntity;
        }

    }
}
