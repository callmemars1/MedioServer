namespace Medio.Domain.Utilities;

public record struct Color
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
}

public static class Colors 
{
    public static Color White
    {
        get
        {
            return new Color
            {
                R = 255,
                G = 255,
                B = 255,
            };
        }
    }
    public static Color Green
    {
        get
        {
            return new Color
            {
                R = 0,
                G = 255,
                B = 0,
            };
        }
    }
    public static Color Red
    {
        get
        {
            return new Color
            {
                R = 255,
                G = 0,
                B = 0,
            };
        }
    }
    public static Color Blue
    {
        get
        {
            return new Color
            {
                R = 0,
                G = 0,
                B = 255,
            };
        }
    }
}