﻿using Medio.Domain;
using Medio.Domain.Entities;
using Medio.Domain.Utilities;
using Medio.Network.ClientPools;
using Medio.Proto;
using Medio.Proto.Exceptions;
using Medio.Proto.MessageHandlers;
using Medio.Proto.Messages;
using NLog;

namespace Medio.Sessions.PvP.MessageHandlers;

public class SpawnRequestHandler : MessageHandlerBase<SpawnRequest>
{
    private readonly ClientPool _clientPool;
    private readonly Map _map;
    private ILogger? _logger;

    public SpawnRequestHandler(ClientPool clientPool, Map map)
    {
        _clientPool = clientPool;
        _map = map;
        _logger = LogManager.GetCurrentClassLogger();
    }

    protected override void Process(SpawnRequest message)
    {
        _logger?.Info("Processing...");
        if (_map.Entities.ContainsKey(message.Id) == false)
            return;

        /*if (_map.Entities[message.Id].Pos != new Vector2D { X = -1, Y = -1 })
            return;*/

        var previousStateEntity = _map.Entities[message.Id] as Player ?? throw new InvalidRequestException(message, "Cant spawn entity on request!");
        var entity = new Player(message.Id)
        {
            Color = previousStateEntity.Color,
            Name = previousStateEntity.Name,
            SizeIncreaseCoefficient = previousStateEntity.SizeIncreaseCoefficient
        };
        var rnd = new Random();
        entity.Pos = new()
        {
            X = rnd.Next(0, (int)_map.Rules.MapWidth * 100) / 100,
            Y = rnd.Next(0, (int)_map.Rules.MapHeight * 100) / 100
        };
        entity.Points = rnd.Next(_map.Rules.MinPlayerSize, _map.Rules.MaxPlayerSpawnSize);
        _map.ExplicitUpdateEntityState(entity.Id, entity);

        var changedEntity = _map.Entities[entity.Id];
        foreach (var client in _clientPool.Clients.Values)
        {
            changedEntity.Pos.Map(out var pos);
            int points = 0;
            if (changedEntity is IPoints entityWithPoints)
                points = entityWithPoints.Points;

            var msg = new EntityUpdatedState()
            {
                Id = changedEntity.Id,
                Pos = pos,
                Points = points
            };
            client.Send(new ByteArr(msg).ToByteArray());
            Console.WriteLine($"Sended new state to: {client.Id}");
        }
    }
}
