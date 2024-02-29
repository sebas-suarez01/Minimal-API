namespace Minimal_API.Domain.Primitives;

public interface IEntity<out TId>
    where TId : ValueObjectId
{
    public TId Id { get; }
}