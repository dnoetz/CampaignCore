using Microsoft.Extensions.DependencyInjection;
using RPG.Core.Enums;
using RPG.Core.Interfaces;

namespace RPG.Infrastructure.Data;

public class AbilityProvider : IAbilityProvider
{
    private readonly IServiceProvider _serviceProvider;

    public AbilityProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public IEnumerable<ICombatAbility> GetAbilitiesForClass(PlayableClasses playableClass)
    {
        return _serviceProvider.GetKeyedServices<ICombatAbility>(playableClass);
    }
}