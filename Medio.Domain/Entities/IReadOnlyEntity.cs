using Medio.Domain.Utilities;

using CSharpVitamins;

namespace Medio.Domain.Entities;

public interface IReadOnlyEntity
{
    public ShortGuid Id { get; }
    public ref Vector2D Pos { get; }
    public float Radius { get; }
}
