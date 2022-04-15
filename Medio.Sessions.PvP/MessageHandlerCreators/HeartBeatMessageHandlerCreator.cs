using Medio.Domain;
using Medio.Network.ClientPools;
using Medio.Network.Clients;
using Medio.Network.MessageHandlers;
using Medio.Proto;
using Medio.Proto.Messages;
using Medio.Sessions.PvP.MessageHandlers;

namespace Medio.Sessions.PvP.MessageHandlerCreators;

public class HeartBeatMessageHandlerCreator : IMessageHandlerCreator
{
    ClientPool _clientPool;
    int _period;

    public int MessageTypeID => MessageTypeIdManager.GetMessageTypeId<HeartBeatMessage>();
    public HeartBeatMessageHandlerCreator(ClientPool clientPool, int period)
    {
        _clientPool = clientPool;
        _period = period; 
    }
    public IMessageHandler Create(Client client)
    {
        return new HeartBeatMessageHandler(_clientPool, _period, client);
    }
}
