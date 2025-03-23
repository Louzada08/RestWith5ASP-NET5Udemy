using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.Filter;
using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNET.Application.Services.Persons;

public interface IPersonService
{
    Task<PersonVO> Create(Person person);
    Task<PersonVO> FindById(Guid id);
    Task<List<PersonVO>> FindByName(PersonFilter filter); 
    Task<List<PersonVO>> GetAll();
    //Task<PagedSearchVO<PersonVO>> FindWithPagedSearch(
    //        string name, string sortDirection, int pageSize, int page);
    Task<PersonVO> Update(PersonVO person);
   // Task<PersonVO> Disable(long id);
}
