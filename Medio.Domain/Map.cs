using CSharpVitamins;
using Medio.Domain.Entities;
using Medio.Domain.Interfaces;
using Medio.Domain.Utilities;
using System.Collections.Concurrent;

namespace Medio.Domain;

public class Map
{
    private readonly EntityCollisionHandlerManager _handlerManager;
    public Rules Rules { get; init; }
    private ConcurrentDictionary<ShortGuid, Entity> _entities = new();
    public IReadOnlyDictionary<ShortGuid, IReadOnlyEntity> Entities
    {
        get => _entities.Select(p => KeyValuePair.Create<ShortGuid, IReadOnlyEntity>(p.Key, p.Value))
                        .ToDictionary(p => p.Key, p => p.Value);
    }
    public Map(Rules rules, EntityCollisionHandlerManager handlerManager)
    {
        Rules = rules;
        _handlerManager = handlerManager;
    }

    public void AddEntity(Entity entity)
    {
        if (_entities.ContainsKey(entity.Id))
            throw new ArgumentException("entite already in collection");

        _entities[entity.Id] = entity;
    }
    public bool TryRemoveEntity(ShortGuid id)
    {
        if (_entities.ContainsKey(id) == false)
            return false;

        _entities.TryRemove(id, out _);
        return true;
    }
    public bool TryUpdateEnityPos(ShortGuid entityID, Vector2D pos)
    {
        if (!_entities.ContainsKey(entityID))
            throw new ArgumentException("no entity with this ID");

        if (IsInsideMap(pos) == false)
            return false;

        var entity = _entities[entityID];
        if (CanMoveTo(entity, pos) == false)
            return false;

        entity.Pos = pos;
        var collideEntities = _entities.Where((pair) => InEntityRange(pos, pair.Value));
        foreach (var collideEntityPair in collideEntities)
        {
            _handlerManager.Handle(entity, collideEntityPair.Value);
        }

        return true;
    }
    public bool IsInsideMap(Vector2D pos)
    {
        return pos.X >= 0
               && pos.Y >= 0
               && pos.X <= Rules.MapWidth
               && pos.Y <= Rules.MapHeight;
    }
    public bool InEntityRange(Vector2D pos, IReadOnlyEntity entity)
    {
        var range = Math.Sqrt(Math.Pow((entity.Pos.X - pos.X), 2) + Math.Pow((entity.Pos.Y - pos.Y), 2));
        return entity.Radius < range;
    }
    public bool CanMoveTo(IReadOnlyEntity entity, Vector2D pos)
    {
        var range = Math.Sqrt(Math.Pow((entity.Pos.X - pos.X), 2) + Math.Pow((entity.Pos.Y - pos.Y), 2));
        return Rules.Speed >= range;
    }
}
