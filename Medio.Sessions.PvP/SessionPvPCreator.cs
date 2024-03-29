﻿using CSharpVitamins;
using Medio.Domain;
using Medio.Domain.Entities;
using Medio.Domain.EntityCollisionHandlers;
using Medio.Domain.Utilities;
using Medio.Network;
using Medio.Network.ClientPools;
using Medio.Proto.MessageHandlers;
using Medio.Sessions.PvP.Acceptors;
using Medio.Sessions.PvP.ClientHandlers;
using Medio.Sessions.PvP.CollisionHandlers;
using Medio.Sessions.PvP.MessageHandlerCreators;
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
        var messageHandlerCreatorManager = new MessageHandlerCreatorManager();
        var handlerCreator = new MedioPvPClientHandlerCreator(messageHandlerCreatorManager);
        var collisionHandlers = new EntityCollisionHandlerManager();
        var map = new MapPvP(Domain.Rules.GetDefaultPvPRules(), collisionHandlers);
        collisionHandlers.RegisterHandler(new PlayerAndFoodCollisionHandler(map));
        collisionHandlers.RegisterHandler(new PlayerAndPlayerCollisionHandler(map));
        messageHandlerCreatorManager.RegisterCreator(new ConnectToSessionRequestHandlerCreator(clientPool, map));
        messageHandlerCreatorManager.RegisterCreator(new HeartBeatMessageHandlerCreator(clientPool, 20));
        messageHandlerCreatorManager.RegisterCreator(new MoveRequestHandlerCreator(clientPool, map));
        messageHandlerCreatorManager.RegisterCreator(new SpawnRequestHandlerCreator(clientPool, map));
        GenerateEntities(map);

        clientPool.ItemRemoved += (key) => map.TryRemoveEntity(key);
        var session = new SessionPvP(
            acceptor,
            clientPool,
            handlerPool,
            handlerCreator
            );
        return session;
    }

    private void GenerateEntities(Map map) 
    {
        var rnd = new Random();
        foreach (var i in Enumerable.Range(0, map.Rules.FoodCount)) 
        {
            var food = new Food(ShortGuid.NewGuid(), map.Rules.SizeIncreaseCoefficient)
            {
                Points = rnd.Next(map.Rules.MinEntitySpawnSize, map.Rules.MaxEntitySpawnSize)
            };
            food.Color = Colors.Green;
            food.Pos = new Vector2D 
            { 
                X = (float)(rnd.NextDouble() * map.Rules.MapWidth),
                Y = (float)(rnd.NextDouble() * map.Rules.MapHeight) 
            };
            map.TryAddEntity(food);
        }
    }
}
