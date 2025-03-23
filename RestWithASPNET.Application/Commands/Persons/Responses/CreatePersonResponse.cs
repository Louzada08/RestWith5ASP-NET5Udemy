using RestWithASPNET.Domain.Dtos;

namespace RestWithASPNET.Application.Commands.Persons.Responses
{
    public class CreatePersonResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public bool Enabled { get; set; }
        public Guid? CreatedById { get; set; }
        public UserDTO? CreatedBy { get; set; }
    }
}
