namespace Minimal_API.Application.Interfaces;

public interface IDistributedCacheService
{
    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        where T : class;
    public Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default);
    public Task<T?> GetOrSet<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default)
        where T:class;
    public Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    public Task RemoveByPrefix<T>(string prefixKey, CancellationToken cancellationToken = default);
}