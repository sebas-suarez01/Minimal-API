using Microsoft.Extensions.Caching.Memory;
using Minimal_API.Application.Interfaces;

namespace Minimal_API.Infrastructure.Cache;

public class MemoryCacheService : IMemoryCacheService
{
    private static readonly TimeSpan DefaultExpiration=TimeSpan.FromMinutes(1);
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
    {
        T result = (await _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(expiration ?? DefaultExpiration);

                return factory(cancellationToken);
            }))!;

        return result;
    }
    public Task RemoveAsync(string key)
    {
        _memoryCache.Remove(key);
        return Task.CompletedTask;
    }
}