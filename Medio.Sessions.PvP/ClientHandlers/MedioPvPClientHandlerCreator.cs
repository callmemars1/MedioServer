using Medio.Network.ClientHandlers;
using Medio.Network.Clients;
using Medio.Proto.MessageHandlers;
using System.Collections.Concurrent;

namespace Medio.Sessions.PvP.ClientHandlers;

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
