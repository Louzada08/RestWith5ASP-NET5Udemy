using RestWithASPNETUdemy.Data.Converter.Implementations;
using RestWithASPNETUdemy.Data.VO;
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
            var result = _converter.Parse(await _repository.FindAll());
            return result;


        }

        public async Task<PersonVO> FindById(long id)
        {
            var result =  _converter.Parse(await _repository.FindById(id));
            return result;
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

        public async Task<PagedSearchVO<PersonVO>> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
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
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }
    }
}
