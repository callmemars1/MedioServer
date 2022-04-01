namespace Medio.Domain;

public record Rules
{
    public float Speed { get; init; }
    public float SizeIncreaseCoefficient { get; init; }
    public int GameLength { get; init; } //ms
    public int MaxPlayerSize { get; init; } // points
    public int MinPlayerSize { get; init; } // points
    public int MaxPlayerSpawnSize { get; init; } // points
    public int MinEntitySpawnSize { get; init; } // points
    public int MaxEntitySpawnSize { get; init; } // points
    public float CanEatSizeDifference { get; init; }
    public float MapWidth { get; init; }
    public float MapHeight { get; init; }
    public bool SpikesEnabled { get; init; }
    public int SpikesCount { get; init; }
    public int FoodCount { get; init; }
    public bool FoodEnabled { get; init; }

    public static Rules GetDefaultPvPRules() 
    {
        return new Rules
        {
            MapHeight = 500f,
            MapWidth = 500f,
            MinEntitySpawnSize = 5,
            MaxEntitySpawnSize = 15,
            MinPlayerSize = 10,
            MaxPlayerSpawnSize = 20,
            CanEatSizeDifference = 0.20f,
            SpikesEnabled = false,
            FoodEnabled = true,
            Speed = 5f,
            SizeIncreaseCoefficient = 1.2f,
            GameLength = int.MaxValue,
            SpikesCount = 0,
            FoodCount = 1000
        };
    }
}
