using Medio.Network.Clients;

namespace Medio.Network.ClientHandlers;

public abstract class ClientHandler
{
    private readonly Client _client;
    public ClientHandler(Client client)
    {
        _client = client;
    }
    public Client Client { get => _client; }
    public abstract bool Active { get; protected set; }
    public abstract void StartHandle();
    public abstract void StopHandle();
}
