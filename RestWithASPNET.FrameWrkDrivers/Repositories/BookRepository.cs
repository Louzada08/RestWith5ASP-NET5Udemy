using RestWithASPNET.Domain.Entities;
using RestWithASPNET.FrameWrkDrivers.Data.Context;
using RestWithASPNET.FrameWrkDrivers.Gateways;
using RestWithASPNET.FrameWrkDrivers.Repositories.Generic;

namespace RestWithASPNET.FrameWrkDrivers.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(MySQLContext context) : base(context)
        {
        }
    }
}
