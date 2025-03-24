using AutoMapper;
using RestWithASPNET.Adapter.ViewModels;
using RestWithASPNET.Application.Commands.Users.Requests;
using RestWithASPNET.Domain.Entities;

namespace RestWithASPNET.Mappings.AutoMapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        //<Origem, Destino>
        CreateMap<User, CreateUserRequest>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken))
            .ForMember(dest => dest.RefreshTokenExpiryTime, opt => opt.MapFrom(src => src.RefreshTokenExpiryTime))
            .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.UserRole))
            .ReverseMap();
        CreateMap<User, CreateUserResponse>().ReverseMap();

        CreateMap<User, UpdateUserRequest>().ReverseMap();
        CreateMap<User, UpdateUserResponse>().ReverseMap();

        CreateMap<User, PatchUserRequest>().ReverseMap();
        CreateMap<User, PatchUserResponse>().ReverseMap();

        CreateMap<User, FindUserByIdRequest>().ReverseMap();
        CreateMap<User, FindUserByIdResponse>().ReverseMap();

        CreateMap<User, UserResponse>().ReverseMap();

        CreateMap<CreateUserResponse, TokenViewModel>().ReverseMap();
    }
}
