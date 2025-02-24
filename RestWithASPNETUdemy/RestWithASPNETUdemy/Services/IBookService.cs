using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Services
{
    public interface IBookService
    {
        Task<BookVO> Create(BookVO book);
        Task<BookVO> FindById(long id);
        Task<List<BookVO>> FindAll();
        Task<BookVO> Update(BookVO book);
        Task Delete(long id);
    }
}
