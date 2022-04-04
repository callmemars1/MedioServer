namespace Medio.Domain.Utilities;

public record class Pair<T, U>
{
    private T _first;
    private U _second;

    public Pair(T first, U second)
    {
        _first = first;
        _second = second;
    }
    public ref T First { get => ref _first; }
    public ref U Second { get => ref _second; }
}
