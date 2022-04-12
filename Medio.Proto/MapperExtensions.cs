namespace Medio.Proto;

public static class MapperExtensions
{
    public static void Map(this Messages.Rules from, out Domain.Rules to) 
    {
        to = new()
        {
            CanEatSizeDifference = from.CanEatSizeDifference,
            GameLength = from.GameLength,
            FoodCount = from.FoodCount,
            FoodEnabled = from.FoodEnabled,
            MapHeight = from.MapHeight,
            MapWidth = from.MapWidth,
            MaxEntities = from.MaxEntities,
            MaxEntitySpawnSize= from.MaxEntitySpawnSize,
            MaxPlayerSize = from.MaxPlayerSize,
            MaxPlayerSpawnSize = from.MaxPlayerSpawnSize,
            MinEntitySpawnSize = from.MinEntitySpawnSize,
            MinPlayerSize = from.MinPlayerSize,
            SizeIncreaseCoefficient = from.SizeIncreaseCoefficient,
            Speed = from.Speed,
            SpikesCount = from.SpikesCount,
            SpikesEnabled = from.SpikesEnabled
        };
    }

    public static void Map(this Domain.Rules from, out Messages.Rules to) 
    {
        to = new()
        {
            CanEatSizeDifference = from.CanEatSizeDifference,
            GameLength = from.GameLength,
            FoodCount = from.FoodCount,
            FoodEnabled = from.FoodEnabled,
            MapHeight = from.MapHeight,
            MapWidth = from.MapWidth,
            MaxEntities = from.MaxEntities,
            MaxEntitySpawnSize = from.MaxEntitySpawnSize,
            MaxPlayerSize = from.MaxPlayerSize,
            MaxPlayerSpawnSize = from.MaxPlayerSpawnSize,
            MinEntitySpawnSize = from.MinEntitySpawnSize,
            MinPlayerSize = from.MinPlayerSize,
            SizeIncreaseCoefficient = from.SizeIncreaseCoefficient,
            Speed = from.Speed,
            SpikesCount = from.SpikesCount,
            SpikesEnabled = from.SpikesEnabled
        };
    }

    public static void Map(this Messages.Color from, out Domain.Utilities.Color to) 
    {
        to = new()
        { 
            R = (byte)from.R,
            G = (byte)from.G,
            B = (byte)from.B,
        };
    }

    public static void Map(this Domain.Utilities.Color from, out Messages.Color to) 
    {
        to = new()
        {
            R = from.R,
            G = from.G,
            B = from.B,
        };
    }

    public static void Map(this Messages.Pos from, out Domain.Utilities.Vector2D to) 
    {
        to = new()
        {
            X = from.X,
            Y = from.Y,
        };
    }

    public static void Map(this Domain.Utilities.Vector2D from, out Messages.Pos to)
    {
        to = new()
        {
            X = from.X,
            Y = from.Y,
        };
    }

    public static void Map(this Messages.FoodData from, out Domain.Entities.Food to, float sizeIncreaseCoefficient) 
    {
        to = new(from.Id, sizeIncreaseCoefficient);
        from.Color.Map(out to.Color);
    }

    public static void Map(this Domain.Entities.Food from, out Messages.FoodData to) 
    {
        to = new() 
        {
            Id = from.Id,
        };
        from.Color.Map(out var color);
        to.Color = color;
    }

    public static void Map(this Messages.PlayerData from, out Domain.Entities.Player to, float sizeIncreaseCoefficient) 
    {
        to = new(from.Id, sizeIncreaseCoefficient) 
        {
            Name = from.Name
        };
        from.Color.Map(out to.Color);
    }

    public static void Map(this Domain.Entities.Player from, out Messages.PlayerData to) 
    {
        from.Color.Map(out var color);
        to = new()
        {
            Id = from.Id,
            Name = from.Name,
            Color = color
        };
    } 
}
