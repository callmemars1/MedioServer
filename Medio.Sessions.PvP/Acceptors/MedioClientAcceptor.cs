using System.Net;
using System.Net.Sockets;
using Medio.Network.Clients;
using CSharpVitamins;
using Medio.Network.ClientAcceptors;
using Medio.Sessions.PvP.Clients;
using NLog;
using Medio.Proto.Messages;
using Medio.Proto;

namespace Medio.Sessions.PvP.Acceptors;

public class MedioClientAcceptor : IClientAcceptor
{
    private readonly TcpListener _listener;
    private ILogger? logger;
    public MedioClientAcceptor(IPEndPoint localAddress)
    {
        _listener = new(localAddress);
        logger = LogManager.GetCurrentClassLogger();
    }
    public bool Accepting { get; private set; }

    public Client Accept()
    {
        var client = _listener.AcceptTcpClient();

        var id = ShortGuid.NewGuid();
        logger?.Info($"New client with id: {id} and ip: {client.Client.RemoteEndPoint}");
        // тут был возврат id клиенту
        var msg = new ConnectResponse() 
        {
            Id = id,
            SessionId = "lox228"
        };
        client.GetStream().Write(new ByteArr(msg).ToByteArray());
        return new MedioTcpClient(id, client.Client.RemoteEndPoint as IPEndPoint
                                                  ?? throw new ArgumentNullException("wtf"), client);
    }

    public void Start()
    {
        if (Accepting)
            return;

        _listener.Start();
        Accepting = true;
        logger?.Info($"Listening on: {_listener.Server.LocalEndPoint}");
    }

    public void Stop()
    {
        if (Accepting == false)
            return;

        _listener.Stop();
        Accepting = false;
        logger?.Info($"Acceptor stopped on: {_listener.Server.LocalEndPoint}");
    }
}
