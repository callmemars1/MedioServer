using CSharpVitamins;
using Medio.Domain.Interfaces;
using Medio.Domain.Utilities;

namespace Medio.Domain.Entities;

public abstract class Entity : IReadOnlyEntity
{
    public Entity(ShortGuid id, string type)
    {
        Id = id;
        Type = type;
    }

    public ShortGuid Id { get; init; }
    public Vector2D Pos { get; set; }
    public string Type { get; init; }
    public abstract float Radius { get; }
}
