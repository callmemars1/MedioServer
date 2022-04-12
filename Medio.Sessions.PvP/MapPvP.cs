using CSharpVitamins;
using Medio.Domain;
using Medio.Domain.Entities;
using Medio.Domain.EntityCollisionHandlers;

namespace Medio.Sessions.PvP;

public class MapPvP : Map
{
    private readonly EntityCollisionHandlerManager _collisionHandlers;

    public MapPvP(Rules rules, EntityCollisionHandlerManager collisionHandlers) : base(rules)
    {
        _collisionHandlers = collisionHandlers;
    }

    public override void ExplicitUpdateEntityState(ShortGuid entityId, Entity newState)
    {
        _entities[entityId] = newState;
    }

    public override bool TryUpdateEntityState(ShortGuid entityId, Entity newState)
    {
        // checks
        _entities[entityId] = newState;
        return true;
    }
}
