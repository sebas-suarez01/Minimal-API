using Minimal_API.Domain.Primitives;

namespace Minimal_API.Domain.DomainEvents;

public record UserCreatedDomainEvent(Guid Id, Guid UserId, string Username) : DomainEvent(Id);