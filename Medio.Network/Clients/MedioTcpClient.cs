using System.Net;
using System.Net.Sockets;

namespace Medio.Network.Clients;

public class MedioTcpClient : Client
{
    TcpClient _client;
    public MedioTcpClient(
        Guid id,
        IPEndPoint clientAddress,
        TcpClient client) : base(id, clientAddress)
    {
        if (client.Connected == false)
            throw new ArgumentException("Client should be connected!");

        _client = client;
    }
    public override void Close()
    {
        if (_client.Connected == false)
            return;

        _client.Close();
    }

    public override int Receive(byte[] buffer, int offset, int size)
    {
        return _client.GetStream().Read(buffer, offset, size);
    }

    public override int Send(Span<byte> msg)
    {
        _client.GetStream().Write(msg);
        return msg.Length;
    }
}
