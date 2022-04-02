using CSharpVitamins;
using Medio.Domain.Entities;
using Medio.Domain.Utilities;

namespace Medio.Domain;

public static class MapGenerator
{
    public static Vector2D GeneratePos(Rules rules)
    {
        var rnd = new Random();

        var x = rnd.NextSingle() * rules.MapWidth;
        var y = rnd.NextSingle() * rules.MapHeight;
        return new Vector2D { X = x, Y = y };
    }

    public static IReadOnlyDictionary<ShortGuid, Entity> GenerateEntitiesForMap(Rules rules)
    {
        Dictionary<ShortGuid, Entity> entities = new();
        var rnd = new Random();
        if (rules.FoodEnabled)
            for (var i = 0; i < rules.FoodCount; ++i)
            {
                var pos = GeneratePos(rules);
                var points = rnd.Next(rules.MinEntitySpawnSize, rules.MaxEntitySpawnSize);
                var food = new Food(ShortGuid.NewGuid())
                {
                    Pos = pos,
                    Points = points,
                    SizeIncreaseCoefficient = rules.SizeIncreaseCoefficient,
                };
                entities.Add(food.Id, food);
            }
        return entities;
    }

    public static int GeneratePointsForPlayer(Rules rules) 
    {
        var rnd = new Random();
        var points = rnd.Next(rules.MinPlayerSize, rules.MaxEntitySpawnSize);
        return points;
    }
}
