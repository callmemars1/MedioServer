using CSharpVitamins;
using Medio.Network.ClientHandlers;
using Medio.Network.Clients;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Medio.Network.ClientPools;

public class ClientPool
{
    protected ConcurrentDictionary<ShortGuid, Client> _clients = new();
    protected ConcurrentDictionary<ShortGuid, ClientHandler> _handlers = new();
    public ClientPool()
    {

    }
    public virtual IReadOnlyDictionary<ShortGuid, IReadOnlyClient> Clients
    {
        get => _clients.Select(p => KeyValuePair.Create<ShortGuid, IReadOnlyClient>(p.Key, p.Value))
                       .ToDictionary(p => p.Key, p => p.Value);
    }
    public virtual void AddClient(Client client, IClientHandlerCreator clientHandlerCreator)
    {
        if (_clients.ContainsKey(client.Id) && _handlers.ContainsKey(client.Id))
            return;

        _clients.TryAdd(client.Id, client);
        var handler = clientHandlerCreator.Create(client);
        _handlers.TryAdd(client.Id, handler);
        Task.Run(() => handler.StartHandle());
        Console.WriteLine("added " + client?.Id);
    }
    public virtual void RemoveClient(Guid id)
    {
        if (_clients.ContainsKey(id) == false || _handlers.ContainsKey(id) == false)
            return;

        _clients.TryRemove(id, out var client);
        _handlers.TryRemove(id, out var handler);
        handler?.StopHandle();
        client?.Close();
        Console.WriteLine("removed " + client?.Id);
    }
}
