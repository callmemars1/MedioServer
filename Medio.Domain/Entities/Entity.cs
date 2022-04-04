using Medio.Domain.Utilities;

using CSharpVitamins;

namespace Medio.Domain.Entities;

/*
 *  Путем достаточно долгих размышлений был сделан вывод, что
 *  в данной игре не будет реализаций сложный форм для игровых сущнсотей,
 *  только круги.
 *  
 *  Id      -   У каждой сущности есть идентификатор, который однозначно определяет ее на карте
 *  Pos     -   У каждой сущности есть позиция (возврат по ссылке для удобства)
 *  Radius  -   У каждой сущности есть радиус
 */

public abstract class Entity : IReadOnlyEntity
{
    private Vector2D _pos;

    public Entity(ShortGuid id)
    {
        Id = id;
    }

    public ShortGuid Id { get; init; }

    public ref Vector2D Pos { get => ref _pos; }

    public abstract float Radius { get; }

}