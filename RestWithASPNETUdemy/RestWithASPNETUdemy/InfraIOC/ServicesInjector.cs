using RestWithASPNETUdemy.Repository.Abstractions;
using RestWithASPNETUdemy.Repository.Generic;
using RestWithASPNETUdemy.Repository.Specific.PersonRepo;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Services.Implementations;
using RestWithASPNETUdemy.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RestWithASPNETUdemy.InfraIOC
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
