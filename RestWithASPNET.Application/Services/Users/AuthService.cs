using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.Enums.Extension;
using RestWithASPNET.Domain.Interfaces.Repositories;
using RestWithASPNET.Domain.Interfaces.Users;
using System.Security.Claims;

namespace RestWithASPNET.Domain.Services.Users;

public class AuthService : IAuthService
{
    private readonly HttpContext _httpContext;
    private readonly IUserRepository _userRepository;

    public AuthService(IHttpContextAccessor httpContext, IUserRepository userRepository)
    {
        _httpContext = httpContext.HttpContext;
        _userRepository = userRepository;
    }

    public ClaimsPrincipal GetLoggedUserData()
    {
        return _httpContext.User;
    }

    public async Task<User?> GetLoggedUserDbDataAsync()
    {
        try
        {
            var loggedUser = GetLoggedUserData();
            var userEmail = loggedUser.Claims.Where(c => c.Type.Contains("emailaddress")).FirstOrDefault()?.Value;
            if (userEmail is null) return null;

            return await _userRepository.QueryableFor(p => p.UserName.Equals(userEmail)).FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<bool> CreateUserOnFirstAccess()
    {
        try
        {
            var loggedUser = GetLoggedUserData();
            var userId = loggedUser.Claims.Where(c => c.Type.Contains("nameidentifier")).FirstOrDefault()?.Value;
            var userName = loggedUser.Claims.Where(c => c.Type.Equals("name")).FirstOrDefault()?.Value;
            var userEmail = loggedUser.Claims.Where(c => c.Type.Contains("emailaddress")).FirstOrDefault()?.Value;
            var userRole = loggedUser.Claims.Where(c => c.Type.Contains("role") && c.Value.Contains("Backoffice")).FirstOrDefault()?.Value;
            var userFullName = loggedUser.Claims.Where(c => c.Type.Equals("fullname")).FirstOrDefault()?.Value;
            var userLoja = loggedUser.Claims.Where(c => c.Type.Equals("Loja")).FirstOrDefault()?.Value;

            var userRoleId = (userRole is not null) ? (UserRolesEnum)Enum.Parse(typeof(UserRolesEnum), ConvertDescriptionToEnum(userRole)) : 0;

            var intId = Guid.Empty;
            Guid.TryParse(userId, out intId);

            if (userName is not null)
            {
                var user = await _userRepository
                    .QueryableFor(p => p.Id == intId)
                    .FirstOrDefaultAsync();

                if (user is null)
                {
                    user = new User()
                    {
                        FullName = userFullName,
                        UserName = userName,
                    };

                    _userRepository.Create(user);
                    await _userRepository.UnitOfWork.Commit();
                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return await Task.FromResult(true);
    }

    private string ConvertDescriptionToEnum(string? value)
    {
        foreach (var role in Enum.GetNames(typeof(UserRolesEnum)))
        {
            var valueDescription = EnumExtensions.GetEnumDescription((UserRolesEnum)Enum.Parse(typeof(UserRolesEnum), role));

            if (valueDescription.ToLower().Equals(value.ToLower())) return role;
        }

        return String.Empty;
    }
}
