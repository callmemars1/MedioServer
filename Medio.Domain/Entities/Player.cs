using Medio.Domain.Utilities;
using Medio.Domain.Entities.Markers;
using CSharpVitamins;

namespace Medio.Domain.Entities;

public class Player : Entity, IPlayer, IPoints
{

    private int _points;
    private Color _color;
    private readonly string _name;

    public Player(ShortGuid id) : base(id)
    {
        _name = "Player" + id.GetHashCode();
    }
    
    public string Name { get => _name; init => _name = value; }

    public Player(ShortGuid id, float sizeIncreaseCoefficient) : this(id)
    {
        SizeIncreaseCoefficient = sizeIncreaseCoefficient;
        Color = Colors.Red;
    }

    public ref Color Color { get => ref _color; }

    public float SizeIncreaseCoefficient { get; set; }

    public int Points { get => _points; set => _points = value; }

    public override float Radius => _points * SizeIncreaseCoefficient;
}
