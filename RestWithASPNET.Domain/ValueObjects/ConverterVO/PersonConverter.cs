using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.Interfaces.ConverterVO;

namespace RestWithASPNET.Domain.ValueObjects.ConverterVO
{
    public class PersonConverter : IPerson<PersonVO, Person>, IPerson<Person, PersonVO>
    {
        public Person Person(PersonVO origin)
        {
            if (origin == null) return null;
            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender,
                CreatedAt = origin.createdat,
                UpdatedAt = origin.updatedat,
                DeletedAt  = origin.deletedat
            };
        }
        public PersonVO Person(Person origin)
        {
            if (origin == null) return null;
            return new PersonVO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender,
                createdat = origin.CreatedAt,
                updatedat = origin.UpdatedAt,
                deletedat = origin.DeletedAt
            };
        }

        public List<Person> Person(List<PersonVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Person(item)).ToList();
        }

        public List<PersonVO> Person(List<Person> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Person(item)).ToList();
        }
    }
}
