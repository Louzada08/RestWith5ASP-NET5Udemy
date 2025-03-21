﻿using RestWithASPNET.Domain.Entities.Base;

namespace RestWithASPNET.FrameWrkDrivers.Gateways.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> Create(T item);
        Task<T> FindById(long id);
        Task<List<T>> FindAll();
        Task<T> Update(T item);
        Task Delete(long id);
        Task<bool> Exists(long id);
        Task<List<T>> FindWithPagedSearch(string query);
        int GetCount(string query);
    }
}
