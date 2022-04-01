using Medio.Network.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Medio.Network.ClientAcceptors;

public class MedioClientAcceptor : IClientAcceptor
{
    TcpListener _listener;
    public MedioClientAcceptor(IPEndPoint localAddress)
    {
        _listener = new(localAddress);
    }
    public bool Accepting { get; private set; }

    public Client Accept()
    {
        var client = _listener.AcceptTcpClient();
        return new MedioTcpClient(Guid.NewGuid(), client.Client.RemoteEndPoint as IPEndPoint, client);
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
