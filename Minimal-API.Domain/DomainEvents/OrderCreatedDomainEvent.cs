using Minimal_API.Domain.Primitives;

namespace Minimal_API.Domain.DomainEvents;

public record OrderCreatedDomainEvent(Guid Id, Guid OrderId, decimal TotalPrice) : DomainEvent(Id);