using RestWithASPNETUdemy.Data.Converter.Implementations;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository.Generic;
using RestWithASPNETUdemy.Repository.Specific.PersonRepo;
using System;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class BookServiceImplementation : IBookService
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookServiceImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public async Task<BookVO> Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = await _repository.Create(bookEntity);

            return _converter.Parse(bookEntity);
        }

        public async Task Delete(long id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<BookVO>> FindAll()
        {
            return _converter.Parse(await _repository.FindAll());
        }

        public async Task<BookVO> FindById(long id)
        {
            return _converter.Parse(await _repository.FindById(id));
        }

        public async Task<BookVO> Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = await _repository.Update(bookEntity);

            return _converter.Parse(bookEntity);
        }
    }
}
