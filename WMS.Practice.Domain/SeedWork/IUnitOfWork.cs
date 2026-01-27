namespace WMS.Practice.Domain.SeedWork
{
    /// <summary>
    /// Defines a contract for a unit of work that coordinates the writing of changes and the persistence of entities as a single transaction.
    /// </summary>
    /// <remarks>Implementations of this interface are responsible for tracking changes to entities and
    /// ensuring that all modifications are committed to the underlying data store atomically. It is recommended to call
    /// SaveEntitiesAsync to persist changes before disposing of the unit of work. This interface extends IDisposable,
    /// so resources should be released appropriately when the unit of work is no longer needed.</remarks>
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}
