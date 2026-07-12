using Mapster;
using RPG.API.DTOs.Character;
using RPG.Core.Entities.Characters;

namespace RPG.API.Mapping;

public class CharacterProfile :IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Character, CharacterSummaryDto>();
        config.NewConfig<Character, CharacterResponseDto>();
    }
}