using CSharpVitamins;
using Medio.Domain;
using Medio.Domain.Entities;
using Medio.Domain.EntityCollisionHandlers;
using Medio.Domain.Utilities;
using Medio.Proto.Messages;

namespace Medio.Sessions.PvP;

public class MapPvP : Map
{
    private readonly EntityCollisionHandlerManager _collisionHandlers;
    private readonly Timer _timer;

    public MapPvP(Domain.Rules rules, EntityCollisionHandlerManager collisionHandlers) : base(rules)
    {
        _collisionHandlers = collisionHandlers;
        _timer = new((o) =>
        {
            foreach (var entity in _entities.Where((e) =>
                {
                    return e.Value is not Player && e.Value.Pos == new Vector2D { X = -1, Y = -1 } && e.Value is IPoints;
                })
            )
            {
                var rnd = new Random();
                entity.Value.Pos = new Vector2D { X = (float)(rnd.NextDouble() * Rules.MapWidth), Y = (float)(rnd.NextDouble() * Rules.MapHeight) };
                IPoints pointable = (IPoints)entity.Value;
                pointable.Points = rnd.Next(Rules.MinEntitySpawnSize, Rules.MaxEntitySpawnSize);
            }
        }, null, 0, 5_000);
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
        return entity.Radius + 0.5f > range;
    }
    public bool CanMoveTo(IReadOnlyEntity entity, Vector2D pos)
    {
        var range = Math.Sqrt(Math.Pow((entity.Pos.X - pos.X), 2) + Math.Pow((entity.Pos.Y - pos.Y), 2));
        return Rules.Speed >= range;
    }
}
