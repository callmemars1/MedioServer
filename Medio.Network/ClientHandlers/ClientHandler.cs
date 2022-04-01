using Medio.Network.Clients;

namespace Medio.Network.ClientHandlers;

public abstract class ClientHandler
{
    protected Client _client;
    public ClientHandler(Client client)
    {
        _client = client;
    }
    public abstract void StartHandle();
    public abstract void StopHandle();
}
