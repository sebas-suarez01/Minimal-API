namespace Minimal_API.Domain.Primitives;

public interface IAuditableEntity
{
    public DateTime CreatedUtc { get; set; }
    public DateTime? ModifiedUtc { get; set; }
}