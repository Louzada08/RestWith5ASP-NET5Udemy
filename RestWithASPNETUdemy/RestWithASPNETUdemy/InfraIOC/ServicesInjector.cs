using RestWithASPNETUdemy.Repository.Abstractions;
using RestWithASPNETUdemy.Repository.Generic;
using RestWithASPNETUdemy.Repository.Specific.PersonRepo;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Services.Implementations;
using RestWithASPNETUdemy.Services;

namespace RestWithASPNETUdemy.InfraIOC
{
    public static class ServicesInjector
    {
        public static IServiceCollection AddServicesInjector(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonService, PersonServiceImplementation>();
            services.AddScoped<IBookService, BookServiceImplementation>();
            services.AddScoped<ILoginService, LoginServiceImplementation>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}
