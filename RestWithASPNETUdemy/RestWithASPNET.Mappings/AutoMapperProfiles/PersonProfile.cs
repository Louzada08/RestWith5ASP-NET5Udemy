using AutoMapper;
using RestWithASPNET.Application.Commands.Persons.Requests;
using RestWithASPNET.Application.Commands.Persons.Responses;
using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNET.Mappings.AutoMapperProfiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, CreatePersonRequest>().ReverseMap();
        CreateMap<PersonVO, CreatePersonResponse>().ReverseMap();
    }
}
