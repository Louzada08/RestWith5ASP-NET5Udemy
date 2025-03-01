using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Services
{
    public interface IPersonService
    {
        Task<PersonVO> Create(PersonVO person);
        Task<PersonVO> FindById(long id);
        Task<List<PersonVO>> FindAll();
        Task<PersonVO> Update(PersonVO person);
        Task<PersonVO> Disable(long id);
    }
}
