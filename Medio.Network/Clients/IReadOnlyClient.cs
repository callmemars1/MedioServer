using CSharpVitamins;
using System.Net;

namespace Medio.Network.Clients;

public interface IReadOnlyClient
{
    public ShortGuid Id { get; }
    public EndPoint RemoteEndPoint { get; }
    public bool Connected { get; }
    public int Send(Span<byte> msg);
    public Span<byte> Receive(int size);
}
