using Medio.Network.Clients;

namespace Medio.Network.ClientHandlers;

public interface IClientHandlerCreator
{
    ClientHandler Create(Client client);
}
