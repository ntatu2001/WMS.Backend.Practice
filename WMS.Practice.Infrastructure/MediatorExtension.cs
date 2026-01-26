namespace WMS.Practice.Infrastructure
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, WMSDbContext context)
        {
            // Get all Entities with domain events
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            // Get all Domain Events from these Entities
            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            // Clear Domain Events for each Entity
            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            // Publish each Domain Event through Mediator
            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
