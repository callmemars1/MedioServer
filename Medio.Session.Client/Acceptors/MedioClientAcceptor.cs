using CSharpVitamins;
using Medio.Messages;
using Medio.Network.Clients;
using Medio.Session.Client.Utilities;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Medio.Network.ClientAcceptors;

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
        Console.WriteLine("accepted");
        var id = ShortGuid.NewGuid();
        var response = new ConnectResponse 
        {
            Id = id,
        };
        client.GetStream().Write(new ByteArr(response).ToByteArray());
        return new MedioTcpClient(id, client.Client.RemoteEndPoint as IPEndPoint
                                                  ?? throw new ArgumentNullException("wtf"), client);
    }

    public void Start()
    {
        if (Accepting)
            return;

        Console.WriteLine("started");
        _listener.Start();
        Accepting = true;
    }

    public void Stop()
    {
        if (Accepting == false)
            return;

        Console.WriteLine("stopped");
        _listener.Stop();
        Accepting = false;
    }
}
