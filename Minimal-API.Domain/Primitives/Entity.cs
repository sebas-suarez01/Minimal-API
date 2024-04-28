namespace Minimal_API.Domain.Primitives;

public class Entity<TId> : IEntity<TId>, IDomainEventContainer, IAuditableEntity, ISoftDeletable
{
    protected readonly List<DomainEvent> _domainEvents = new();
    public TId Id { get; protected set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime? ModifiedUtc { get; set; }

    public ICollection<DomainEvent> GetDomainEvents() => _domainEvents.ToList();
    public void RaiseDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public bool IsDeleted { get; set; }
}