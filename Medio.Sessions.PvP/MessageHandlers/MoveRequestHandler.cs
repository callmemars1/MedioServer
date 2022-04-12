using Medio.Proto.Messages;
using Medio.Domain;
using Medio.Domain.Entities;
using Medio.Proto.MessageHandlers;
using Medio.Proto.Exceptions;
using Medio.Proto;

namespace Medio.Sessions.PvP.MessageHandlers;

public class MoveRequestHandler : MessageHandlerBase<MoveRequest>
{
    private readonly Map _map;

    public MoveRequestHandler(Map map)
    {
        _map = map;
    }
    protected override void Process(MoveRequest message)
    {
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
        _map.UpdateEntityState(message.Id, newState);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
    }
}
