using Medio.Network.ClientHandlers;
using Medio.Network.Clients;
using Medio.Proto.MessageHandlers;
using Medio.Sessions.PvP.MessageHandlerCreators;
using System.Collections.Concurrent;

namespace Medio.Sessions.PvP.ClientHandlers;

public class MedioPvPClientHandlerCreator : IClientHandlerCreator
{
    private readonly MessageHandlerCreatorManager _messageHandlerCreatorManager;

    public MedioPvPClientHandlerCreator(MessageHandlerCreatorManager messageHandlerCreatorManager)
    {
        _messageHandlerCreatorManager = messageHandlerCreatorManager;
    }
    public ClientHandler Create(Client client)
    {
        return new MedioPvPClientHandler(client, _messageHandlerCreatorManager.GetHandlerManager(client));
    }
}
