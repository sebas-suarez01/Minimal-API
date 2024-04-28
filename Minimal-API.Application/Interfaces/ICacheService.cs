namespace Minimal_API.Application.Interfaces;

public interface ICacheService
{
    Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan? expiration = null,
        CancellationToken cancellationToken = default);

    Task RemoveAsync(string key);
}