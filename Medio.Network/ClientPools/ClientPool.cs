using CSharpVitamins;
using Medio.Network.Clients;

namespace Medio.Network.ClientPools;

public class ClientPool : Pool<ShortGuid, Client>
{
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
    }

    public override void Remove(ShortGuid key)
    {
        if (_values.ContainsKey(key) == false)
            return;

        _values.TryRemove(key, out var client);
        client?.Close();
    }
    public void CloseAll() 
    {
        foreach (var clientPair in _values)
            clientPair.Value.Close();
    }
}
