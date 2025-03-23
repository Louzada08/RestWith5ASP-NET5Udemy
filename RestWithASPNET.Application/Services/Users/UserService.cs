using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Application.Interfaces.Users;
using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.Filter;
using RestWithASPNET.Domain.Interfaces.Repositories;

namespace RestWithASPNET.Application.Services.Users;
public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public User GetById(Guid id)
    {
        var user = _repository.QueryableFor(p => p.Id == id)
                .FirstOrDefault();

        return user;
    }

    public async Task<User> Create(User user)
    {
        var ret = _repository.Create(user);
        await _repository.UnitOfWork.Commit();
        return ret;
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _repository.QueryableFor(p => p.FullName.Equals(email))
                        .FirstOrDefaultAsync();
    }

    public async Task<IList<User>> GetAll(UserFilter filter)
    {
        var filterQuery = await _repository
            .QueryableFor(pX =>
                (filter.Name == null || pX.UserName!.Contains(filter.Name)) &&
                (filter.FullName == null || pX.FullName!.Contains(filter.FullName)))
            .AsNoTracking()
            .ToListAsync();

        return filterQuery;
    }

    public async Task<IList<User>> GetAllGerentes(string nomeLoja)
    {
        if(string.IsNullOrEmpty(nomeLoja)) return new List<User>();

        var users = await _repository
            .QueryableFor(x => x.FullName.Equals(nomeLoja))
            .AsNoTracking()
            .ToListAsync();

        return users;
    }
}
