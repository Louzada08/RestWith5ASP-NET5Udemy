﻿
using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Domain.Entities.Base;
using RestWithASPNET.FrameWrkDrivers.Data.Context;
using RestWithASPNET.FrameWrkDrivers.Gateways.Generic;

namespace RestWithASPNET.FrameWrkDrivers.Repositories.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected MySQLContext _context;
        private DbSet<T> dataset;
        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public async Task<T> Create(T item)
        {
            try
            {
                await dataset.AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<T> Update(T item)
        {
            var result = await dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    await _context.SaveChangesAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task Delete(long id)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<List<T>> FindAll()
        {
            return await dataset.ToListAsync();
        }

        public async Task<T> FindById(long id)
        {
            return await dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<bool> Exists(long id)
        {
            return await dataset.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<List<T>> FindWithPagedSearch(string query)
        {
            return await dataset.FromSqlRaw<T>(query).ToListAsync();
        }

        public int GetCount(string query)
        {
            var result = string.Empty;
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }
            return int.Parse(result);
        }
    }
}
