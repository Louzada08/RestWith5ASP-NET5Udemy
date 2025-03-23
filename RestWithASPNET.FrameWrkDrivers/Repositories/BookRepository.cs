using AutoMapper;
using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.Interfaces.Repositories;
using RestWithASPNET.FrameWrkDrivers.Data.Context;
using RestWithASPNET.FrameWrkDrivers.Repositories.Generic;

namespace RestWithASPNET.FrameWrkDrivers.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly MySQLContext _context;
        public BookRepository(MySQLContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }
        IUnitOfWork UnitOfWork => _context;
    }
}
