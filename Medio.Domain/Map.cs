using System.Collections.Concurrent;
using Medio.Domain.Entities;
using CSharpVitamins;

namespace Medio.Domain;

/*
 *  Контейнер для сущностей с привязкой к правилам
 */

public abstract class Map
{
    protected delegate void EntityTryAddHandler(Map sender, Entity entity, bool tryResult);
    protected delegate void EntityTryRemoveHandler(Map sender, ShortGuid entityId, bool tryResult);
    protected event EntityTryAddHandler? OnTryAdd;
    protected event EntityTryRemoveHandler? OnTryRemove;

    protected readonly ConcurrentDictionary<ShortGuid, Entity> _entities = new();
    private readonly Rules _rules;

    public Map(Rules rules)
    {
        _rules = rules;
    }

    public virtual IReadOnlyDictionary<ShortGuid, IReadOnlyEntity> Entities
    {
        get => _entities.Select(p => KeyValuePair.Create<ShortGuid, IReadOnlyEntity>(p.Key, p.Value))
                        .ToDictionary(p => p.Key, p => p.Value);
    }

    public Rules Rules { get => _rules; }

    public virtual bool TryAddEntity(Entity entity)
    {
        if (_entities.ContainsKey(entity.Id) || _entities.Count == _rules.MaxEntities)
        {
            OnTryAdd?.Invoke(this, entity, false);
            return false;
        }

        _entities[entity.Id] = entity;
        OnTryAdd?.Invoke(this, entity, true);
        return true;
    }

    public virtual bool TryRemoveEntity(ShortGuid id)
    {
        var result = _entities.TryRemove(id, out _);
        OnTryRemove?.Invoke(this, id, result);
        return result;
    }

    // Проверят возможность изменения состояния
    public abstract void UpdateEntityState(ShortGuid entityId, Entity newState);
    // Ничего не проверяет
    public abstract void ExplicitUpdateEntityState(ShortGuid entityId, Entity newState);
}
