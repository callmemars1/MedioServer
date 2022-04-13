using Medio.Domain;
using Medio.Domain.Entities;
using Medio.Domain.EntityCollisionHandlers;
using Medio.Network.ClientPools;

namespace Medio.Sessions.PvP.CollisionHandlers;

public class PlayerAndPlayerCollisionHandler : EntityCollisionHandlerBase<Player, Player>
{
    Map _map;
    public PlayerAndPlayerCollisionHandler(Map map)
    {
        _map = map;
    }
    protected override IReadOnlyCollection<IReadOnlyEntity> HandleCollision(Player entity, Player collider)
    {
        if (entity.Points / (float)collider.Points < _map.Rules.CanEatSizeDifference)
            if (collider.Points / (float)entity.Points < _map.Rules.CanEatSizeDifference)
                return new List<IReadOnlyEntity>();
            else 
            {
                collider.Points += entity.Points;
                entity.Pos.X = -1;
                entity.Points = 0;
                entity.Pos.Y = -1;
                return new List<IReadOnlyEntity>() { entity, collider };
            }

        entity.Points += collider.Points;
        collider.Points = 0;
        collider.Pos.X = -1;
        collider.Pos.Y = -1;
        return new List<IReadOnlyEntity>() { entity, collider };
    }
}
