using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy.Repository.Specific.PersonRepo
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context)
        {
        }

        public async Task<Person> Disable(long id)
        {
            if (!await _context.Persons.AnyAsync(p => p.Id.Equals(id))) return null;
            var person = await _context.Persons.SingleOrDefaultAsync(p => p.Id.Equals(id));
            if (person != null)
            {
                person.Enabled = false;

                try
                {
                    _context.Entry(person).CurrentValues.SetValues(person);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }
            }

            return person;
        }
    }
}
