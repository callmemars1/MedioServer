using Medio.Network;
using Medio.Network.ClientAcceptors;
using Medio.Network.ClientHandlers;
using Medio.Network.ClientPools;

namespace Medio.Sessions.PvP;

public class SessionPvP : Session
{
    public SessionPvP(
        IClientAcceptor acceptor,
        ClientPool clientPool,
        ClientHandlerPool handlerPool,
        IClientHandlerCreator handlerCreator) : base(acceptor, clientPool, handlerPool, handlerCreator)
    {
        
    }
}
