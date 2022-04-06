using Medio.Network.ClientHandlers;
using Medio.Network.Clients;
using CSharpVitamins;

namespace Medio.Network.ClientPools;

public class ClientHandlerPool : Pool<ShortGuid, ClientHandler>
{
    public delegate void AddHandlerDelegate(Client client);
    public event AddHandlerDelegate? OnAdd;
    public delegate void RemoveHandlerDelegate(Client client);
    public event RemoveHandlerDelegate? OnRemove;

    protected ClientPool _clientPool;

    public ClientHandlerPool(ClientPool clientPool)
    {
        _clientPool = clientPool;
    }

    public virtual IReadOnlyDictionary<ShortGuid, IReadOnlyClient> Clients
    {
        get => _values.Select(p => KeyValuePair.Create<ShortGuid, IReadOnlyClient>(p.Key, p.Value.Client))
                        .ToDictionary(p => p.Key, p => p.Value);
    }

    public override void Add(ShortGuid key, ClientHandler value)
    {
        if (_values.ContainsKey(key))
            return;

        _values.TryAdd(key, value);

        Task.Run(() =>
        {
            try
            {
                value.StartHandle();
            }
            catch (Exception)
            {
                Remove(key);
            }
        });
    }

    public override void Remove(ShortGuid key)
    {
        if (_values.ContainsKey(key) == false)
            return;

        _values.TryRemove(key, out var handler);
        handler?.StopHandle();
    }
    public void StopAll()
    {
        foreach (var valuePair in _values) 
            valuePair.Value.StopHandle();

        _values.Clear();
    }
    ~ClientHandlerPool() 
    {
        StopAll();
    }
}