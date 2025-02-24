﻿using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class BookServiceImplementation : IBookService
    {
        private readonly IRepository<Book> _repository;

        public BookServiceImplementation(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<Book> Create(Book book)
        {
            return await _repository.Create(book);
        }

        public async Task Delete(long id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<Book>> FindAll()
        {
            return await _repository.FindAll();
        }

        public async Task<Book> FindById(long id)
        {
            return await _repository.FindById(id);
        }

        public async Task<Book> Update(Book book)
        {
            return await _repository.Update(book);
        }

    }
}
