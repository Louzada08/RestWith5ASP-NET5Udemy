using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNET.Domain.Interfaces
{
    public interface IPersonService
    {
        Task<PersonVO> Create(PersonVO person);
        Task<PersonVO> FindById(long id);
        Task<List<PersonVO>> FindByName(string firstName, string lastName); 
        Task<List<PersonVO>> FindAll();
        Task<PagedSearchVO<PersonVO>> FindWithPagedSearch(
                string name, string sortDirection, int pageSize, int page);
        Task<PersonVO> Update(PersonVO person);
        Task<PersonVO> Disable(long id);
    }
}
