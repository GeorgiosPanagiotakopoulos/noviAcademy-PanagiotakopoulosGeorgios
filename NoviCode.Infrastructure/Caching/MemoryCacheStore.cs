using Microsoft.Extensions.Caching.Memory;

namespace NoviCode;

// In-memory implementation of the Application's ICache port, backed by IMemoryCache.
// This is where the caching *technology* lives (Infrastructure), not in the Application.
// Swapping to Redis later means adding another ICache implementation here — the Application
// services never change.
public class MemoryCacheStore : ICache
{
	private readonly IMemoryCache _cache;

	public MemoryCacheStore(IMemoryCache cache) => _cache = cache;

	public bool TryGet<T>(string key, out T? value) => _cache.TryGetValue(key, out value);

	public void Set<T>(string key, T value, TimeSpan ttl) => _cache.Set(key, value, ttl);

	public void Remove(string key) => _cache.Remove(key);
}
