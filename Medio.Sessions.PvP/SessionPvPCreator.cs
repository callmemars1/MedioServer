using Medio.Network;
using Medio.Network.ClientPools;
using Medio.Proto.MessageHandlers;
using Medio.Sessions.PvP.Acceptors;
using Medio.Sessions.PvP.ClientHandlers;
using System.Net;

namespace Medio.Sessions.PvP;

public class SessionPvPCreator : ISessionCreator
{
    private readonly IPEndPoint localIP;

    public SessionPvPCreator(IPEndPoint localIP)
    {
        this.localIP = localIP;
    }

    public Session Create()
    {
        var acceptor = new MedioClientAcceptor(localIP);
        var clientPool = new ClientPool();
        var handlerPool = new ClientHandlerPool(clientPool);
        var messageHandlerManager = new MessageHandlerManager();
        var handlerCreator = new MedioPvPClientHandlerCreator(messageHandlerManager);
        var session = new SessionPvP(
            acceptor,
            clientPool,
            handlerPool,
            handlerCreator
            );
        return session;
    }
}
