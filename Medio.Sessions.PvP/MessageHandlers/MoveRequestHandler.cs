using Medio.Proto.Messages;
using Medio.Domain;
using Medio.Domain.Entities;
using Medio.Proto.MessageHandlers;
using Medio.Proto.Exceptions;
using Medio.Proto;
using Medio.Network.ClientPools;
using NLog;

namespace Medio.Sessions.PvP.MessageHandlers;

public class MoveRequestHandler : MessageHandlerBase<MoveRequest>
{
    private readonly ClientPool _clientPool;
    private readonly Map _map;
    private ILogger? _logger;

    public MoveRequestHandler(ClientPool clientPool, Map map)
    {
        _clientPool = clientPool;
        _map = map;
        _logger = LogManager.GetCurrentClassLogger();
    }
    protected override void Process(MoveRequest message)
    {
        _logger?.Info("Processing....");
        if (_map.Entities.ContainsKey(message.Id) == false)
            throw new InvalidRequestException(message, "no player with id");

        var entity = _map.Entities[message.Id];
        Entity? newState = null;

        if (entity is Food food)
        {
            newState = new Food(food.Id, food.SizeIncreaseCoefficient)
            {
                Points = food.Points,
            };
            ((Food)newState).Color = food.Color;
            message.Pos.Map(out ((Food)newState).Pos);
        }
        else if (entity is Player player)
        {
            newState = new Player(player.Id, player.SizeIncreaseCoefficient)
            {
                Name = player.Name,
            };
            ((Player)newState).Color = player.Color;
            message.Pos.Map(out ((Player)newState).Pos);
        }

#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
        var changedEntities = _map.TryUpdateEntityState(message.Id, newState);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
        // рассылка
        // переделать рассылку на игроков в сессии
        // нет смысла отсылать данные игрокам в процессе подключения
        foreach (var client in _clientPool.Clients.Values)
        {
            foreach (var changedEntity in changedEntities)
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
            }
        }
    }
}
