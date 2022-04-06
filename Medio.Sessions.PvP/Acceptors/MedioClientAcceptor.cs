using System.Net;
using System.Net.Sockets;
using Medio.Network.Clients;
using CSharpVitamins;
using Medio.Network.ClientAcceptors;
using Medio.Proto;

namespace Medio.Sessions.Pvp.Acceptors;

public class MedioClientAcceptor : IClientAcceptor
{
    private readonly TcpListener _listener;
    public MedioClientAcceptor(IPEndPoint localAddress)
    {
        _listener = new(localAddress);
    }
    public bool Accepting { get; private set; }

    public Client Accept()
    {
        var client = _listener.AcceptTcpClient();
        var id = ShortGuid.NewGuid();
        // тут был возврат id клиенту
        return new MedioTcpClient(id, client.Client.RemoteEndPoint as IPEndPoint
                                                  ?? throw new ArgumentNullException("wtf"), client);
    }

    public void Start()
    {
        if (Accepting)
            return;

        _listener.Start();
        Accepting = true;
    }

    public void Stop()
    {
        if (Accepting == false)
            return;

        _listener.Stop();
        Accepting = false;
    }
}
