using Medio.Network.Clients;

namespace Medio.PvPSession.Exceptions;

public class ClientConnectionInterruptedException : Exception
{
    public Client Client { get; init; }
    public ClientConnectionInterruptedException(Client client, string? message) : base(message)
    {
        Client = client;
    }
}
