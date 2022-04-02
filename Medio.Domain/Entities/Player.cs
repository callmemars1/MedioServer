using CSharpVitamins;
using Medio.Domain.Interfaces;
using Medio.Domain.Utilities;

namespace Medio.Domain.Entities;

public class Player : Entity
{
    
    private readonly string _name;
    public Player(ShortGuid id) : base(id, "Player")
    {
        _name = "Player" + id.GetHashCode();
    }
    
    public string Name { get => _name; init => _name = value; }
    public Player(ShortGuid id, float sizeIncreaseCoefficient) : this(id)
    {
        SizeIncreaseCoefficient = sizeIncreaseCoefficient;
        Color = Color.Red;
    }
    public float SizeIncreaseCoefficient { get; set; }

    public override float Radius => _points * SizeIncreaseCoefficient;
}
