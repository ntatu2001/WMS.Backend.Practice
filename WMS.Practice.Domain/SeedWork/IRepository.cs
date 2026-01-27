namespace WMS.Practice.Domain.SeedWork
{
    /// <summary>
    /// Defines a contract for a repository that manages aggregate root entities and coordinates data persistence operations.
    /// </summary>
    /// <remarks>Repositories provide an abstraction over data access, enabling decoupling of domain logic
    /// from data storage concerns. This interface exposes the associated unit of work, which is responsible for
    /// tracking changes and managing transactional consistency.</remarks>
    /// <typeparam name="T">The type of aggregate model that the repository manages. Must implement <see cref="IAggregateModel"/>.</typeparam>
    public interface IRepository<T> where T : IAggregateModel
    {
        IUnitOfWork UnitOfWork { get; } 
    }
}
