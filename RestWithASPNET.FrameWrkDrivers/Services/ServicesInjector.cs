using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RestWithASPNET.Domain.Interfaces.Book;
using RestWithASPNET.Domain.Services.Token;
using RestWithASPNET.FrameWrkDrivers.Gateways;
using RestWithASPNET.FrameWrkDrivers.Gateways.Generic;
using RestWithASPNET.FrameWrkDrivers.Repositories;
using RestWithASPNET.FrameWrkDrivers.Repositories.Generic;

namespace RestWithASPNET.FrameWrkDrivers.Services
{
    public static class ServicesInjector
    {
        public static IServiceCollection AddServicesInjector(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonServiceImplementation>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookServiceImplementation>();
            services.AddScoped<ILoginService, LoginServiceImplementation>();
            services.AddScoped<IFileService, FileServiceImplementation>();

            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
