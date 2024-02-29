using System.Net;

namespace Minimal_API.Domain.Shared;

public class Error : IEquatable<Error>
{
    public Error(HttpStatusCode httpStatusCode, string code, string description)
    {
        HttpStatusCode = httpStatusCode;
        Code = code;
        Description = description;
    }

    public HttpStatusCode HttpStatusCode { get; }
    public string Code { get; }
    public string Description { get; }

    public static implicit operator string(Error error) => error.Code;

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var error = (Error)obj;

        return error.Code == Code;
    }
    public static bool operator ==(Error? left, Error? right)
    {
        if (left is null && right is null) return true;

        if (left is null || right is null) return false;

        return Equals(left, right);
    }

    public static bool operator !=(Error? left, Error? right)
    {
        if (left is null && right is null) return false;

        if (left is null || right is null) return true;

        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }

    public bool Equals(Error? other)
    {
        return Equals((object?)other);
    }
    public override string ToString()
    {
        return Description;
    }
}
