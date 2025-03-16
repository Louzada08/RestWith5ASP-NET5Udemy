using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Domain.Entities;
using RestWithASPNET.FrameWrkDrivers.Data.Context;
using RestWithASPNET.FrameWrkDrivers.Gateways;
using RestWithASPNET.FrameWrkDrivers.Repositories.Generic;

namespace RestWithASPNET.FrameWrkDrivers.Repositories
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

        public async Task<List<Person>> FindByName(string firstName, string lastName)
        {
            var person = new List<Person>();

            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            { 
                 person = await _context.Persons.Where(p => p.FirstName.Contains(firstName) &&
                        p.LastName.Contains(lastName)).ToListAsync();
            }
            else if (string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                person = await _context.Persons.Where(p => p.LastName.Contains(lastName)).ToListAsync();
            }
            else if (!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                person = await _context.Persons.Where(p => p.FirstName.Contains(firstName)).ToListAsync();
            }
            return person;
        }
    }
}
