using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Application.Services.Interfaces.Books;
using RestWithASPNET.Domain.Interfaces.Repositories;
using RestWithASPNET.Domain.ValueObjects;
using RestWithASPNET.Domain.ValueObjects.ConverterVO;

namespace RestWithASPNET.Application.Services.Books;

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
        //var userLogged = await _authService.GetLoggedUserDbDataAsync();
        //if (userLogged != null) customer.CreatedById = userLogged.Id;

        var bookEntity = _converter.Book(book);
        bookEntity = _repository.Create(bookEntity);
        await _repository.UnitOfWork.Commit();

        return _converter.Book(bookEntity);
    }

    public async Task Delete(Guid id)
    {
        var bookEntity = await _repository.QueryableFor(p => p.Id == id).FirstOrDefaultAsync();

        if (bookEntity == null) return;

        var result = _repository?.Delete(bookEntity!);

        await Task.FromResult(result);
    }

    public async Task<IList<BookVO>> GetAll()
    {
        var books = await _repository.QueryableFor().ToListAsync();
        var booksVO = _converter.Book(books);

        return booksVO;
    }

    public async Task<BookVO> FindById(Guid id)
    {
        var book = await _repository.QueryableFor(p => p.Id == id)
                .FirstOrDefaultAsync();
        var bookVO = _converter.Book(book!);

        return bookVO;
    }

    public async Task<BookVO> Update(BookVO bookvo)
    {
        var book = _converter.Book(bookvo);
        var ret = _repository.Update(book);
        await _repository.UnitOfWork.Commit();

        return _converter.Book(ret);
    }
}
