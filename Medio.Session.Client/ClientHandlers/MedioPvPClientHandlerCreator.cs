using Medio.Network.ClientHandlers;
using Medio.Network.Clients;
using Medio.Session.Client.MessageHandlers;

namespace Medio.PvPSession.ClientHandlers;

public class MedioPvPClientHandlerCreator : IClientHandlerCreator
{
    private readonly MessageHandlerManager messageHandlerManager;

    public MedioPvPClientHandlerCreator(MessageHandlerManager messageHandlerManager)
    {
        this.messageHandlerManager = messageHandlerManager;
    }
    public ClientHandler Create(Client client)
    {
        return new MedioPvPClientHandler(client, messageHandlerManager);
    }
}
