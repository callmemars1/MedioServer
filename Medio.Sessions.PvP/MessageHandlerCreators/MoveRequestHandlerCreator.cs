using Medio.Domain;
using Medio.Network.ClientPools;
using Medio.Network.Clients;
using Medio.Network.MessageHandlers;
using Medio.Proto;
using Medio.Proto.Messages;
using Medio.Sessions.PvP.MessageHandlers;

namespace Medio.Sessions.PvP.MessageHandlerCreators;

public class MoveRequestHandlerCreator : IMessageHandlerCreator
{
    ClientPool _clientPool;
    Map _map;
    public int MessageTypeID => MessageTypeIdManager.GetMessageTypeId<MoveRequest>();
    public MoveRequestHandlerCreator(ClientPool clientPool, Map map)
    {
        _clientPool = clientPool;
        _map = map;
    }
    public IMessageHandler Create(Client client)
    {
        return new MoveRequestHandler(_clientPool, _map);
    }
}
