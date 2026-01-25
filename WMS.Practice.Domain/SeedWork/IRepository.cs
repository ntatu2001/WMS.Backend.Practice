namespace WMS.Practice.Domain.SeedWork
{
    internal interface IRepository<T> where T : IAggregateModel
    {
        IUnitOfWork UnitOfWork { get; } 
    }
}
