using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Minimal_API.Domain.Primitives;

namespace Minimal_API.Persistance.Interceptors;

public class PublishDomainEventInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _publisher;

    public PublishDomainEventInterceptor(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        DbContext? context = eventData.Context;

        if (context is not null)
        {
            var domainEvents = context.ChangeTracker
                .Entries<IDomainEventContainer>()
                .Select(x => x.Entity)
                .SelectMany(e =>
                {
                    var domainEvents = e.GetDomainEvents();

                    e.ClearDomainEvents();

                    return domainEvents;
                })
                .ToList();

            var tasks = domainEvents.Select(de => _publisher.Publish(de, cancellationToken));

            await Task.WhenAll(tasks);
        }
        
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}