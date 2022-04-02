using CSharpVitamins;
using Medio.Domain.Utilities;

namespace Medio.Domain.Interfaces;

public interface IReadOnlyEntity
{
    public ShortGuid Id { get; }
    public Vector2D Pos { get; }
    public string Type { get; }
    public float Radius { get; }
    public Color Color { get; }
    public int Points { get; }
}
