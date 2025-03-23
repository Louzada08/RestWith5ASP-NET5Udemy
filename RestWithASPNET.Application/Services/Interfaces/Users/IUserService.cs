using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.Filter;

namespace RestWithASPNET.Application.Interfaces.Users;

public interface IUserService
{
    User GetById(Guid id);
    Task<User?> GetByEmail(string email);
    Task<User> Create(User user);
    Task<IList<User>> GetAll(UserFilter filter);
    Task<IList<User>> GetAllGerentes(string nomeLoja);
}