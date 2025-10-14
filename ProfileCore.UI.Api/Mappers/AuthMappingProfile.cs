using AutoMapper;
using ProfileCore.Application.Commands.User;
using ProfileCore.Application.Dtos;
using ProfileCore.UI.Api.DTOs;

namespace ProfileCore.UI.Api.Mappers
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<RegisterRequest, RegisterUserCommand>()
                .ConstructUsing(src => new RegisterUserCommand(
                    src.Email,
                    src.FirstName,
                    src.LastName,
                    src.FatherName,
                    src.Password));

            CreateMap<LoginRequest, LoginUserCommand>()
                .ConstructUsing(src => new LoginUserCommand(
                    src.Email,
                    src.Password));

            CreateMap<RefreshRequest, UserRefreshTokenCommand>()
                .ConstructUsing(src => new UserRefreshTokenCommand(
                    src.UserId,
                    src.RefreshToken));
			
            CreateMap<TokenDto, AuthResponse>();
        }
    }
}