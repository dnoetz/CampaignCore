namespace RPG.Core.Interfaces;

public interface IUnitOfWork
{
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}