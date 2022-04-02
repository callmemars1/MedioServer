using CSharpVitamins;
using System.Net;

namespace Medio.Network.Clients;

public abstract class Client : IReadOnlyClient
{
    protected Client(ShortGuid id,EndPoint ep)
    {
        RemoteEndPoint = ep;
        Id = id;
    }
    public ShortGuid Id { get; init; }
    public virtual EndPoint RemoteEndPoint { get; init; }
    public virtual bool Connected { get; private set; }
    public abstract int Send(Span<byte> msg);
    public abstract Span<byte> Receive(int size);
    public abstract void Close();
    ~Client() 
    {
        Close();
    }
}