using Medio.Domain;
using Medio.Domain.Entities;
using Medio.Domain.Utilities;
using Medio.Messages;
using Medio.Network.MessageHandlers;
using Medio.PvPSession.Exceptions;

namespace Medio.PvPSession.MessageHandlers;

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
            Pos = MapGenerator.GeneratePos(_map.Rules),
            Points = MapGenerator.GeneratePointsForPlayer(_map.Rules),
            SizeIncreaseCoefficient = previousStateEntity.SizeIncreaseCoefficient
        };
        _map.ExplicitChangeEntityState(entity.Id, entity);
    }
}
