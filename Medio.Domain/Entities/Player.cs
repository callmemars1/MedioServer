using CSharpVitamins;
using Medio.Domain.Interfaces;
using Medio.Domain.Utilities;

namespace Medio.Domain.Entities;

public class Player : Entity
{
    private int _points;
    private string _name;
    public Player(ShortGuid id) : base(id, "Player")
    {
        _name = "Player" + id.GetHashCode();
    }
    public int Points => _points;
    public string Name { get => _name; }
    public Player(ShortGuid id, float sizeIncreaseCoefficient) : this(id)
    {
        SizeIncreaseCoefficient = sizeIncreaseCoefficient;
    }
    public Color Color { get; set; } = Color.Red;
    public float SizeIncreaseCoefficient { get; set; }

    public override float Radius => _points * SizeIncreaseCoefficient;
}
