namespace NoviCode;

// Application-owned cache abstraction (a "port"). The Application layer declares WHAT it
// needs from a cache; it does not know or care HOW the cache is implemented. The concrete
// technology (in-memory, Redis, ...) lives in Infrastructure and implements this interface.
//
// This is the Clean Architecture fix for Day 6: the Application no longer references
// Microsoft.Extensions.Caching.Memory — dependencies point inward, toward an abstraction
// the inner layer owns, so caching stays a swappable infrastructure detail.
public interface ICache
{
	// Try to read a cached value. Returns true and sets `value` on a hit; false on a miss.
	bool TryGet<T>(string key, out T? value);

	// Store a value under `key` with a time-to-live.
	void Set<T>(string key, T value, TimeSpan ttl);

	// Evict an entry (used to invalidate list views after a write).
	void Remove(string key);
}
