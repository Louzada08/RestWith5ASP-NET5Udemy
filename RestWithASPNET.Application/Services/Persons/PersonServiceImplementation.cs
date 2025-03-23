using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.Filter;
using RestWithASPNET.Domain.Interfaces.Repositories;
using RestWithASPNET.Domain.ValueObjects;
using RestWithASPNET.Domain.ValueObjects.ConverterVO;

namespace RestWithASPNET.Application.Services.Persons
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

        public async Task<PersonVO> Create(Person person)
        {
            var personEntity = _repository.Create(person);
            await _repository.UnitOfWork.Commit();
            var personVO = _converter.Person(personEntity);

            return personVO;
        }

        public async Task<PersonVO> Update(PersonVO personvo)
        {
            var personEntity = _converter.Person(personvo);
            var person = _repository.Update(personEntity);
            await _repository.UnitOfWork.Commit();

            return _converter.Person(person);
        }

        public async Task<List<PersonVO>> GetAll()
        {
            var persons = await _repository.QueryableFor().ToListAsync();
            var personsVO = _converter.Person(persons);

            return personsVO;
        }

        public async Task<PersonVO> FindById(Guid id)
        {
            var person = await _repository.QueryableFor(p => p.Id == id)
                    .FirstOrDefaultAsync();
            var personVO = _converter.Person(person!);

            return personVO;
        }

        public async Task<List<PersonVO>> FindByName(PersonFilter filter)
        {
            var persons = await _repository.QueryableFor(p =>
                (filter.firstName == null || p.FirstName.Contains(filter.firstName)) &&
                (filter.lastName == null || p.LastName.Contains(filter.lastName)))
            .OrderBy(oR => oR.FirstName)
            .AsNoTracking()
            .ToListAsync();
            
            var personsVO = _converter.Person(persons);
            return personsVO;
        }

        //public async Task<PersonVO> Disable(long id)
        //{
        //    var personEntity = _converter.Parse(await _repository.Disable(id));
        //    return personEntity;
        //}

        public async Task<PagedSearchVO<PersonVO>> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = !string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = pageSize < 1 ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from person p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) query = query + $" and p.first_name like '%{name}%' ";
            query += $" order by p.first_name {sort} limit {size} offset {offset}";

            string countQuery = @"select count(*) from person p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" and p.first_name like '%{name}%' ";

            var persons = await _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO>
            {
                CurrentPage = page,
                List = _converter.Person(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }
    }
}
