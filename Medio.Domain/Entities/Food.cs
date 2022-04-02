using CSharpVitamins;
using Medio.Domain.Interfaces;
using Medio.Domain.Utilities;

namespace Medio.Domain.Entities;

public class Food : Entity
{
    public Food(ShortGuid id) : base(id, "Food")
    {
    }
    public Food(ShortGuid id, float sizeIncreaseCoefficient) : this(id)
    {
        SizeIncreaseCoefficient = sizeIncreaseCoefficient;
        Color = Color.Blue;
    }
    public float SizeIncreaseCoefficient { get; set; }
    public override float Radius => _points * SizeIncreaseCoefficient;
}
