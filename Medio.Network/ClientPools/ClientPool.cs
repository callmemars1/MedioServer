using CSharpVitamins;
using Medio.Network.Clients;

namespace Medio.Network.ClientPools;

public class ClientPool : Pool<ShortGuid, Client>
{
    public delegate void ItemAddedHandler(ShortGuid key);
    public event ItemAddedHandler? ItemAdded;

    public delegate void ItemRemovedHandler(ShortGuid key);
    public event ItemRemovedHandler? ItemRemoved;

    public virtual IReadOnlyDictionary<ShortGuid, IReadOnlyClient> Clients
    {
        get => _values.Select(p => KeyValuePair.Create<ShortGuid, IReadOnlyClient>(p.Key, p.Value))
                      .ToDictionary(p => p.Key, p => p.Value);
    }

    public override void Add(ShortGuid key, Client value)
    {
        if (_values.ContainsKey(key))
            return;

        _values.TryAdd(key, value);
        ItemAdded?.Invoke(key);
    }

    public override void Remove(ShortGuid key)
    {
        if (_values.ContainsKey(key) == false)
            return;

        _values.TryRemove(key, out var client);
        client?.Close();
        ItemRemoved?.Invoke(key);
    }
    public void CloseAll() 
    {
        foreach (var clientPair in _values)
            clientPair.Value.Close();
    }
}
