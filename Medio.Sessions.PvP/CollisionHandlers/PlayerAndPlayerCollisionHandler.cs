﻿using Medio.Domain;
using Medio.Domain.Entities;
using Medio.Domain.EntityCollisionHandlers;

namespace Medio.Sessions.PvP.CollisionHandlers;

public class PlayerAndPlayerCollisionHandler : EntityCollisionHandlerBase<Player, Player>
{
    Map _map;
    public PlayerAndPlayerCollisionHandler(Map map)
    {
        _map = map;
    }
    protected override void HandleCollision(Player entity, Player collider)
    {
        if (entity.Points / (float)collider.Points < _map.Rules.CanEatSizeDifference)
            return;

        entity.Points += collider.Points;
        collider.Points = 0;
        collider.Pos.X = -1;
        collider.Pos.Y = -1;
    }
}
