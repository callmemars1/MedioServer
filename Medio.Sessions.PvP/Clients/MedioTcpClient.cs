using CSharpVitamins;
using Medio.Network.Clients;
using Medio.Network.Exceptions;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Medio.Sessions.PvP.Clients;

public class MedioTcpClient : Client
{
    private readonly TcpClient _client;
    public MedioTcpClient(
        ShortGuid id,
        IPEndPoint clientAddress,
        TcpClient client) : base(id, clientAddress)
    {
        if (client.Connected == false)
            throw new ArgumentException("Client should be connected!");

        _client = client;
        _client.ReceiveTimeout = 60000;
    }

    public override bool Connected => _client.Connected;

    public override void Close()
    {
        if (_client.Connected == false)
            return;

        _client.Close();
    }

    public override Span<byte> Receive(int size)
    {
        List<byte> msg = new List<byte>();
        var stream = _client.GetStream();
        var defaultBufferSize = 256;
        while (msg.Count < size)
        {
            var readSize = size >= msg.Count + defaultBufferSize ? defaultBufferSize : size - msg.Count;
            var buffer = new byte[readSize];
            var bytes = stream.Read(buffer, 0, readSize);
            msg.AddRange(buffer.Take(bytes));
        }
        if (_client.Connected == false)
            throw new ClientConnectionInterruptedException(this, "Client not connected");

        var span = CollectionsMarshal.AsSpan(msg);
        return span;
    }

    public override int Send(Span<byte> msg)
    {
        _client.GetStream().Write(msg);
        return msg.Length;
    }
}