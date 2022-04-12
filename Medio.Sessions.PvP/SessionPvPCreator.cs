using Medio.Domain;
using Medio.Domain.EntityCollisionHandlers;
using Medio.Network;
using Medio.Network.ClientPools;
using Medio.Proto;
using Medio.Proto.MessageHandlers;
using Medio.Proto.Messages;
using Medio.Sessions.PvP.Acceptors;
using Medio.Sessions.PvP.ClientHandlers;
using Medio.Sessions.PvP.CollisionHandlers;
using Medio.Sessions.PvP.MessageHandlers;
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
        var collisionHandlers = new EntityCollisionHandlerManager();
        var map = new MapPvP(Domain.Rules.GetDefaultPvPRules(), collisionHandlers);
        collisionHandlers.RegisterHandler(new PlayerAndFoodCollisionHandler(map));
        collisionHandlers.RegisterHandler(new PlayerAndPlayerCollisionHandler(map));
        messageHandlerManager.RegisterHandler(MessageTypeIdManager.GetMessageTypeId<ConnectToSessionRequest>(),
                                              new ConnectToSessionRequestHandler(clientPool, map));
        messageHandlerManager.RegisterHandler(MessageTypeIdManager.GetMessageTypeId<HeartBeatMessage>(),
                                              new HeartBeatMessageHandler(clientPool, 20));
        messageHandlerManager.RegisterHandler(MessageTypeIdManager.GetMessageTypeId<MoveRequest>(),
                                              new MoveRequestHandler(clientPool, map));
        messageHandlerManager.RegisterHandler(MessageTypeIdManager.GetMessageTypeId<SpawnRequest>(),
                                              new SpawnRequestHandler(clientPool, map));
        var session = new SessionPvP(
            acceptor,
            clientPool,
            handlerPool,
            handlerCreator
            );
        return session;
    }
}
