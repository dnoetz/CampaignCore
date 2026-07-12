using Mapster;
using RPG.API.DTOs.User;
using RPG.Core.Entities;

namespace RPG.API.Mapping;

public class UserProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserResponseDto>();
        config.NewConfig<User, UserSummaryDto>();
    }
}