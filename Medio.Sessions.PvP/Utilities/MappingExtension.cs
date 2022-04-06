using Medio.Domain.Utilities;
using Medio.Messages;

namespace Medio.PvPSession.Utilities
{
    public static class MappingExtension
    {
        public static Medio.Messages.Rules GetDtoRules(this Medio.Domain.Rules rules)
        {
            return new Medio.Messages.Rules()
            {
                MapHeight = rules.MapHeight,
                MapWidth = rules.MapWidth,
                MaxEntitySpawnSize = rules.MaxEntitySpawnSize,
                MinEntitySpawnSize = rules.MinEntitySpawnSize,
                CanEatSizeDifference = rules.CanEatSizeDifference,
                FoodCount = rules.FoodCount,
                SpikesEnabled = rules.SpikesEnabled,
                GameLength = rules.GameLength,
                FoodEnabled = rules.FoodEnabled,
                MaxPlayerSize = rules.MaxPlayerSize,
                MaxPlayerSpawnSize = rules.MaxPlayerSpawnSize,
                MinPlayerSize = rules.MinPlayerSize,
                SizeIncreaseCoefficient = rules.SizeIncreaseCoefficient,
                Speed = rules.Speed,
                SpikesCount = rules.SpikesCount
            };
        }
        public static Medio.Messages.Color GetDtoColor(this Medio.Domain.Utilities.Color color)
        {
            return new Messages.Color()
            {
                R = color.R,
                G = color.G,
                B = color.B,
            };
        }
        public static Pos GetDtoPos(this Vector2D pos)
        {
            return new Pos
            {
                X = pos.X,
                Y = pos.Y,
            };
        }
        public static Domain.Utilities.Color FromDtoColor(this Messages.Color color)
        {
            return new Domain.Utilities.Color()
            {
                R = (byte)color.R,
                G = (byte)color.G,
                B = (byte)color.B,
            };
        }
    }
}
