using Medio.Domain;
using Medio.Network.ClientPools;
using Medio.Network.Clients;
using Medio.Network.MessageHandlers;
using Medio.Proto;
using Medio.Proto.Messages;
using Medio.Sessions.PvP.MessageHandlers;

namespace Medio.Sessions.PvP.MessageHandlerCreators;

public class SpawnRequestHandlerCreator : IMessageHandlerCreator
{
    ClientPool _clientPool;
    Map _map;
    public int MessageTypeID => MessageTypeIdManager.GetMessageTypeId<SpawnRequest>();
    public SpawnRequestHandlerCreator(ClientPool clientPool, Map map)
    {
        _clientPool = clientPool;
        _map = map;
    }
    public IMessageHandler Create(Client client)
    {
        return new SpawnRequestHandler(_clientPool, _map);
    }
}
