using Medio.Network.ClientHandlers;
using Medio.Network.Clients;
using Medio.Session.Client.MessageHandlers;

namespace Medio.PvPSession.ClientHandlers;

public class MedioPvPClientHandlerCreator : IClientHandlerCreator
{
    private readonly MessageHandlerManager _messageHandlerManager;

    public MedioPvPClientHandlerCreator(MessageHandlerManager messageHandlerManager)
    {
        _messageHandlerManager = messageHandlerManager;
    }
    public ClientHandler Create(Client client)
    {
        return new MedioPvPClientHandler(client, _messageHandlerManager);
    }
}
