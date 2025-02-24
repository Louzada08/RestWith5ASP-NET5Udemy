using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Repository.Specific.BookRepo
{
    public interface IBookRepository
    {
        Task<Book> Create(Book book);
        Task<Book> FindById(long id);
        Task<List<Book>> FindAll();
        Task<Book> Update(Book book);
        Task Delete(long id);
        Task<bool> Exists(long id);
    }
}
