﻿using CSharpVitamins;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Medio.Network.Clients;

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
        while (msg.Count < size)
        {
            var readSize = size >= msg.Count + 256 ? 256 : size - msg.Count;
            var buffer = new byte[readSize];
            var bytes = stream.Read(buffer, 0, readSize);
            msg.AddRange(buffer.Take(bytes));
        }
        var span = CollectionsMarshal.AsSpan(msg);
        return span;
    }

    public override int Send(Span<byte> msg)
    {
        _client.GetStream().Write(msg);
        return msg.Length;
    }
}
