using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RestWithASPNET.Application.Services.Books;
using RestWithASPNET.Application.Services.File;
using RestWithASPNET.Application.Services.Interfaces.Books;
using RestWithASPNET.Application.Services.Login;
using RestWithASPNET.Application.Services.Persons;
using RestWithASPNET.Application.Services.Token;
using RestWithASPNET.Domain.Interfaces.Repositories;
using RestWithASPNET.FrameWrkDrivers.Data.Context;
using RestWithASPNET.FrameWrkDrivers.Repositories;
using RestWithASPNET.FrameWrkDrivers.Repositories.Generic;

namespace RestWithASPNET.FrameWrkDrivers.Services;

public static class ServicesInjector
{
    public static IServiceCollection AddServicesInjector(this IServiceCollection services)
    {
        services.AddScoped<MySQLContext>();
        services.AddScoped<TokenService>();

        services.AddScoped<IPersonService, PersonServiceImplementation>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookServiceImplementation>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILoginService, LoginServiceImplementation>();
        services.AddScoped<IFileService, FileServiceImplementation>();

        services.AddTransient<ITokenService, TokenService>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }
    public static IServiceCollection AddDbContextInjector(this IServiceCollection services, string connection)
    {
        services.AddDbContext<MySQLContext>(options => options.UseMySQL(connection));

        return services;
    }
}
