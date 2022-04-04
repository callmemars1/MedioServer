using Medio.Domain.Entities;
using Medio.Domain.Utilities;

namespace Medio.Domain.EntityCollisionHandlers;

public interface IEntityCollisionHandler
{
    public Pair<Type, Type> Types { get; }
    public void Handle<TEntity, TCollider>(TEntity entity, TCollider collider)
        where TEntity   : class, IReadOnlyEntity
        where TCollider : class, IReadOnlyEntity;
}
