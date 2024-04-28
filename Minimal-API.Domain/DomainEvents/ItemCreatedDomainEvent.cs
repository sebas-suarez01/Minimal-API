using Minimal_API.Domain.Primitives;

namespace Minimal_API.Domain.DomainEvents;

public record ItemCreatedDomainEvent(Guid Id, Guid ItemId, string Name, decimal Price) : DomainEvent(Id);
