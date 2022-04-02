using Medio.Domain.Entities;
using Medio.Domain.Utilities;

namespace Medio.Domain.Interfaces;

// Handler manager
// Направляет запросы к хендлеру на обработку
public class EntityCollisionHandlerManager
{
    private readonly Dictionary<Pair<Type, Type>, IEntityCollisionHandler> _handlers = new();
    public IEnumerable<IReadOnlyEntity> Handle<TEntity, TCollider>(TEntity entity, TCollider collider)
        where TEntity   : class, IReadOnlyEntity
        where TCollider : class, IReadOnlyEntity
    {
        var handler = GetHandler(entity.GetType(), collider.GetType()) ?? throw new ArgumentNullException(null, "handler not exist!");
        return handler.Handle(entity, collider);
    }
    private IEntityCollisionHandler? GetHandler(Type entityType, Type colliderType)
    {
        var key = new Pair<Type, Type> { First = entityType, Second = colliderType };
        if (_handlers.ContainsKey(key))
            return _handlers[key];

        return null;
    }
    public void RegisterHandler(IEntityCollisionHandler handler)
    {
        if (GetHandler(handler.Types.First, handler.Types.Second) is not null)
            throw new ArgumentException("handler already registered");

        _handlers[handler.Types] = handler;
    }
}
