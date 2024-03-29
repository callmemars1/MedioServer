﻿using Medio.Domain.Entities;
using Medio.Domain.Utilities;

namespace Medio.Domain.EntityCollisionHandlers;

/*
 * Базовый класс для всех хендлеров.
 * Позволяет на этапе написания класса явно определить типы, с которыми будет работать хендлер
 */

public abstract class EntityCollisionHandlerBase<T, U> : IEntityCollisionHandler
    where T : class, IReadOnlyEntity
    where U : class, IReadOnlyEntity
{
    public Pair<Type, Type> Types { get; } = new Pair<Type, Type>(typeof(T), typeof(U));

    public IReadOnlyCollection<IReadOnlyEntity> Handle<TEntity, TCollider>(TEntity entity, TCollider collider)
        where TEntity : class, IReadOnlyEntity
        where TCollider : class, IReadOnlyEntity
    {
        T castedEntity = entity as T ?? throw new InvalidCastException("Entity type not matched");
        U castedCollider = collider as U ?? throw new InvalidCastException("Collider type not matched");
        return HandleCollision(castedEntity, castedCollider);
    }
    protected abstract IReadOnlyCollection<IReadOnlyEntity> HandleCollision(T entity, U collider);
}