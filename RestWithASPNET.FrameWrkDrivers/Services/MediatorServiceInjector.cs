using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestWithASPNET.Application.Commands.Users.Requests;
using RestWithASPNET.Application.Commands.Users;
using RestWithASPNET.Domain.Mediator.Interfaces;
using RestWithASPNET.Domain.Mediator;
using RestWithASPNET.Domain.Validation;
using RestWithASPNET.Application.Commands.Persons.Requests;
using RestWithASPNET.Application.Commands.Persons.Handlers;

namespace RestWithASPNET.FrameWrkDrivers.Services
{
    public static class MediatorServiceInjector
    {
        public static IServiceCollection AddMediatorInjector(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IRequestHandler<CreateUserRequest, ValidationResultBag>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserRequest, ValidationResultBag>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<PatchUserRequest, ValidationResultBag>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserRequest, ValidationResultBag>, UserCommandHandler>();

            services.AddScoped<IRequestHandler<CreatePersonRequest, ValidationResultBag>, PersonCommandHandler>();

            return services;
        }
    }
}
