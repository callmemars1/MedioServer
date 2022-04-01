using CSharpVitamins;
using Medio.Domain.Interfaces;
using Medio.Domain.Utilities;

namespace Medio.Domain.Entities;

public class Food : Entity
{
    private int _points;
    public Food(ShortGuid id) : base(id, "Food")
    {
    }
    public Food(ShortGuid id, float sizeIncreaseCoefficient) : this(id)
    {
        SizeIncreaseCoefficient = sizeIncreaseCoefficient;
    }
    public float SizeIncreaseCoefficient { get; set; }
    public int Points { get => _points; }
    public Color Color { get; set; } = Color.Blue;

    public override float Radius => _points * SizeIncreaseCoefficient;
}
