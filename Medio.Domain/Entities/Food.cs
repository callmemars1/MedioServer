using Medio.Domain.Utilities;
using Medio.Domain.Entities.Markers;
using CSharpVitamins;

namespace Medio.Domain.Entities;

public class Food : Entity, IFood
{
    private int _points;
    private Color _color;

    public Food(ShortGuid id) : base(id)
    {
    }

    public Food(ShortGuid id, float sizeIncreaseCoefficient) : this(id)
    {
        SizeIncreaseCoefficient = sizeIncreaseCoefficient;
        Color = Colors.Blue;
    }
    public ref Color Color { get => ref _color; }
    public int Points { get => _points; set => _points = value; }
    public float SizeIncreaseCoefficient { get; set; }
    public override float Radius => _points * SizeIncreaseCoefficient;
}
