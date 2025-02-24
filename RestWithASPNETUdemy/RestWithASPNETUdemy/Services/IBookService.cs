﻿using RestWithASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Services
{
    public interface IBookService
    {
        Task<Book> Create(Book book);
        Task<Book> FindById(long id);
        Task<List<Book>> FindAll();
        Task<Book> Update(Book book);
        Task Delete(long id);
    }
}
