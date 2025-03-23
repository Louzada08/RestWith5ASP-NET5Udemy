using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RestWithASPNET.FrameWrkDrivers.AutoMappers;

namespace RestWithASPNET.FrameWrkDrivers.Services;

public static class AutoMapperServiceInjector
{
       public static IServiceCollection AddAutoMapperInjector(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new PersonProfile());
        });

        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}
