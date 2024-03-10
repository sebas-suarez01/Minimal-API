namespace Minimal_API.Domain.Primitives;

public interface IEntity<out TId>
{
    public TId Id { get; }
}