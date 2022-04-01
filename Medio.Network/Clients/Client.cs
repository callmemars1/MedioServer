using System.Net;

namespace Medio.Network.Clients;

public abstract class Client : IReadOnlyClient
{
    protected Client(Guid id,EndPoint ep)
    {
        RemoteEndPoint = ep;
        Id = id;
    }
    public Guid Id { get; init; }
    public virtual EndPoint RemoteEndPoint { get; init; }
    public virtual bool Connected { get; private set; }
    public abstract int Send(Span<byte> msg);
    public abstract int Receive(byte[] buffer, int offset, int size);
    public abstract void Close();
    ~Client() 
    {
        Close();
    }
}