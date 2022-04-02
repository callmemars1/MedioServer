using Medio.Domain.Utilities;

namespace Medio.Domain.Interfaces;

// Каждый хендлер знает с какими типами работает
public interface IEntityCollisionHandler
{
    public Pair<Type, Type> Types { get; }
    public IEnumerable<IReadOnlyEntity> Handle<TEntity, TCollider>(TEntity entity, TCollider collider)
        where TEntity   : class, IReadOnlyEntity
        where TCollider : class, IReadOnlyEntity;
}
