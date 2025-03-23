using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNET.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User> 
    {
        User ValidateCredentials(UserVO user);
    }
}
