namespace Minimal_API.Domain.Primitives;

public abstract class ValueObjectId : ValueObject
{
    public Guid Value { get; protected set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static T Create<T>(Guid id) where T : ValueObjectId, new()
    {
        var instance = new T();
        instance.Value = id;
        return instance;
    }
    public static T CreateUnique<T>() where T : ValueObjectId, new()
    {
        var instance = new T();
        instance.Value = Guid.NewGuid();
        return instance;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}