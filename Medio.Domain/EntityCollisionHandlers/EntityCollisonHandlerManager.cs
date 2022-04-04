using Medio.Domain.Entities;
using Medio.Domain.Exceptions;
using Medio.Domain.Utilities;

namespace Medio.Domain.EntityCollisionHandlers;

/*
 * Данный класс содержит в себе обработчики столкновений (коллизий)
 * сущностей. Путем маппинга типов удается выбрать нужный обработчик
 * без лишнего приведения типов в вызывающем коде, а так же без
 * сумасшедих условных ветвлений
 */

public class EntityCollisionHandlerManager
{
    private readonly Dictionary<Pair<Type, Type>, IEntityCollisionHandler> _handlers = new();

    public void Handle<TEntity, TCollider>(TEntity entity, TCollider collider)
        where TEntity : class, IReadOnlyEntity
        where TCollider : class, IReadOnlyEntity
    {
        // Получаем хендлер для реальных переданных типов
        var handler = GetHandler(entity.GetType(), collider.GetType());
        handler.Handle(entity, collider);
    }

    private IEntityCollisionHandler GetHandler(Type entityType, Type colliderType)
    {
        var key = new Pair<Type, Type>(entityType, colliderType );
        // check for explicit match
        if (_handlers.ContainsKey(key))
            return _handlers[key];

        // check for interfaces or classes type can be subclass of
        bool isFinded = false;
        var handlerPair = _handlers.FirstOrDefault(
            (pair) =>
            {
                isFinded = entityType.IsSubclassOf(pair.Key.First) && colliderType.IsSubclassOf(pair.Key.Second);
                return isFinded;
            });
        if (isFinded)
            return handlerPair.Value;

        throw new HandlerNotFoundException(
            this,
            "No handler with types <" + entityType.FullName + "," + colliderType.FullName + ">"
            );
    }

    public void RegisterHandler(IEntityCollisionHandler handler)
    {
        if (_handlers.ContainsKey(handler.Types))
            throw new ArgumentException("Handler already registered!");

        _handlers[handler.Types] = handler;
    }
}
