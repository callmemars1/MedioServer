using System.Collections.Concurrent;

namespace Medio.Network.ClientPools;

public abstract class Pool<TKey,TValue>
    where TKey : notnull
{
    protected readonly ConcurrentDictionary<TKey, TValue> _values = new();
    public abstract void Add(TKey key, TValue value);
    public abstract void Remove(TKey key);
}
