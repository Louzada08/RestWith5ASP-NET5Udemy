using RestWithASPNET.Domain.Entities;
using System.Security.Claims;

namespace RestWithASPNET.Domain.Interfaces.Users;

public interface IAuthService
{
    ClaimsPrincipal GetLoggedUserData();
    Task<User?> GetLoggedUserDbDataAsync();
    Task<bool> CreateUserOnFirstAccess();

}