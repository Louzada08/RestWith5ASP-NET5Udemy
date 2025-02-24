﻿using RestWithASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Services
{
    public interface IPersonService
    {
        Task<Person> Create(Person person);
        Task<Person> FindById(long id);
        Task<List<Person>> FindAll();
        Task<Person> Update(Person person);
    }
}
