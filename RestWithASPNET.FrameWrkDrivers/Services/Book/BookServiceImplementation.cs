using RestWithASPNET.Domain.Interfaces.Book;
using RestWithASPNET.Domain.ValueObjects;
using RestWithASPNET.FrameWrkDrivers.ConverterVO;
using RestWithASPNET.FrameWrkDrivers.Gateways;

namespace RestWithASPNET.FrameWrkDrivers.Services.Book
{
    public class BookServiceImplementation : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly BookConverter _converter;

        public BookServiceImplementation(IBookRepository repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public async Task<BookVO> Create(BookVO book)
        {
            var bookEntity = _converter.Book(book);
            bookEntity = await _repository.Create(bookEntity);

            return _converter.Book(bookEntity);
        }

        public async Task Delete(long id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<BookVO>> FindAll()
        {
            return _converter.Book(await _repository.FindAll());
        }

        public async Task<BookVO> FindById(long id)
        {
            return _converter.Book(await _repository.FindById(id));
        }

        public async Task<BookVO> Update(BookVO book)
        {
            var bookEntity = _converter.Book(book);
            bookEntity = await _repository.Update(bookEntity);

            return _converter.Book(bookEntity);
        }
    }
}
