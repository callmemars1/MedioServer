using Medio.Domain;
using Medio.Domain.Entities;
using Medio.Domain.Utilities;
using Medio.Proto.Exceptions;
using Medio.Proto.MessageHandlers;
using Medio.Proto.Messages;

namespace Medio.Sessions.PvP.MessageHandlers;

public class SpawnRequestHandler : MessageHandlerBase<SpawnRequest>
{
    private readonly Map _map;

    public SpawnRequestHandler(Map map)
    {
        _map = map;
    }

    protected override void Process(SpawnRequest message)
    {
        if (_map.Entities.ContainsKey(message.Id) == false)
            return;

        if (_map.Entities[message.Id].Pos != new Vector2D { X = -1, Y = -1 })
            return;

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
    }
}
