using System.Net;

namespace Medio.Network.Clients;

public interface IReadOnlyClient
{
    public Guid Id { get; }
    public EndPoint RemoteEndPoint { get; }
    public bool Connected { get; }
    public int Send(Span<byte> msg);
    public int Receive(byte[] buffer, int offset, int size);
}
