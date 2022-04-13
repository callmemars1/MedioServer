using CSharpVitamins;
using Medio.Domain;
using Medio.Domain.Entities;
using Medio.Domain.EntityCollisionHandlers;
using Medio.Domain.Utilities;

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

    public override IReadOnlyCollection<IReadOnlyEntity> TryUpdateEntityState(ShortGuid entityId, Entity newState)
    {
        var oldState = _entities[entityId];
        if (CanMoveTo(oldState, newState.Pos) == false)
            return new List<IReadOnlyEntity>();

        _entities[entityId] = newState;
        var changedEntities = new List<IReadOnlyEntity>();
        foreach (var entity in _entities.Values)
        {
            if (InEntityRange(newState.Pos, entity))
                changedEntities.AddRange(_collisionHandlers.Handle(newState, entity));
        }
        return changedEntities;
    }

    public static bool InEntityRange(Vector2D pos, IReadOnlyEntity entity)
    {
        var range = Math.Sqrt(Math.Pow((entity.Pos.X - pos.X), 2) + Math.Pow((entity.Pos.Y - pos.Y), 2));
        return entity.Radius > range;
    }
    public bool CanMoveTo(IReadOnlyEntity entity, Vector2D pos)
    {
        var range = Math.Sqrt(Math.Pow((entity.Pos.X - pos.X), 2) + Math.Pow((entity.Pos.Y - pos.Y), 2));
        return Rules.Speed >= range;
    }
}
