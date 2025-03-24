using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace RestWithASPNET.IOC.AutoMapperInjector;

public static class AutoMapperServiceInjector
{
       public static IServiceCollection AddAutoMapperInjector(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new PersonProfile());
            mc.AddProfile(new UserProfile());
        });

        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}
