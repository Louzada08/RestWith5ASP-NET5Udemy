using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNET.Domain.Interfaces
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
