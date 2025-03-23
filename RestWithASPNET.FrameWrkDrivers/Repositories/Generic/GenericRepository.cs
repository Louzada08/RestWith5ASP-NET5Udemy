
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Domain.Interfaces.Repositories;
using RestWithASPNET.FrameWrkDrivers.Data.Context;
using System.Linq.Expressions;

namespace RestWithASPNET.FrameWrkDrivers.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        protected MySQLContext _context { get; }
        protected IUnitOfWork UnitOfWork { get; set; }

        IUnitOfWork IGenericRepository<T>.UnitOfWork => UnitOfWork;

        protected readonly DbSet<T> dataset;
        private readonly IMapper _mapper;

        public GenericRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            dataset = _context.Set<T>();
            this.UnitOfWork = _context as IUnitOfWork;
            _mapper = mapper;
        }

        public T Create(T entity)
        {
            //if(entity is BaseEntity baseEntity)
            //{
            //    baseEntity.CreatedAt = DateTime.UtcNow;
            //    baseEntity.UpdatedAt = DateTime.UtcNow; 
            //}
                dataset.Add(entity);
                return entity;
        }

        public virtual T Update(T entity)
        {
            dataset.Remove(entity);

            return entity;
        }

        public T Delete(T entity)
        {
            var entry = _context.Entry(entity);
            dataset.Attach(entity);
            entry.State = EntityState.Modified;

            return entity;
        }

        public T FindById(params object[] ids)
        {
            return dataset.Find(ids);
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

        public IQueryable<T> QueryableFilter() => dataset.AsQueryable();

        public IQueryable<T> QueryableFor(Expression<Func<T, bool>> criterio = null, 
            bool @readonly = false, params Expression<Func<T, object>>[] includes)
        {
            if (criterio == null)
            {
                if (includes == null)
                {
                    return dataset.Where(criterio);
                }

                var queryAll = dataset.AsQueryable();
                foreach (var include in includes)
                {
                    queryAll.Include(include);
                }

                return @readonly ? queryAll.AsNoTracking() : queryAll;
            }

            var queryWhere = dataset.Where(criterio);

            if (includes == null)
            {
                return queryWhere;
            }

            foreach (var include in includes)
            {
                queryWhere = queryWhere.Include(include);
            }

            return queryWhere;
        }

        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            _context.Dispose();
        }

    }
}
