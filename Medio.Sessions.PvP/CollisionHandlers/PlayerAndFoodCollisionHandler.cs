using Medio.Domain;
using Medio.Domain.Entities;
using Medio.Domain.EntityCollisionHandlers;

namespace Medio.Sessions.PvP.CollisionHandlers;

public class PlayerAndFoodCollisionHandler : EntityCollisionHandlerBase<Player, Food>
{
    Map _map;
    public PlayerAndFoodCollisionHandler(Map map)
    {
        _map = map;
    }
    protected override IReadOnlyCollection<IReadOnlyEntity> HandleCollision(Player entity, Food collider)
    {
        if (entity.Points / (float)collider.Points < _map.Rules.CanEatSizeDifference)
            return new List<IReadOnlyEntity>();

        entity.Points += collider.Points;
        collider.Points = 0;
        collider.Pos.X = -1;
        collider.Pos.Y = -1;
        return new List<IReadOnlyEntity>() { entity, collider };
    }
}
