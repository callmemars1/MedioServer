using Medio.Domain;
using Medio.Network.ClientPools;
using Medio.Network.Clients;
using Medio.Network.MessageHandlers;
using Medio.Proto;
using Medio.Proto.Messages;
using Medio.Sessions.PvP.MessageHandlers;

namespace Medio.Sessions.PvP.MessageHandlerCreators;

public class ConnectToSessionRequestHandlerCreator : IMessageHandlerCreator
{
    Map _map;
    ClientPool _clientPool;
    public ConnectToSessionRequestHandlerCreator(ClientPool clientPool, Map map)
    {
        _clientPool = clientPool;
        _map = map;
    }

    public int MessageTypeID => MessageTypeIdManager.GetMessageTypeId<ConnectToSessionRequest>();

    public IMessageHandler Create(Client client)
    {
        return new ConnectToSessionRequestHandler(_clientPool, _map);
    }
}
