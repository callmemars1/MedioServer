using Medio.Network.ClientAcceptors;
using Medio.Network.ClientHandlers;
using Medio.Network.ClientPools;
using System.Net;

IPEndPoint local = new IPEndPoint(IPAddress.Parse("192.168.88.140"), 5000);
var acceptor = new MedioClientAcceptor(local);
var pool = new ClientPool();

acceptor.Start();
Console.WriteLine("accepting...");
var client = acceptor.Accept();
Console.WriteLine($"new client with guid {client.Id} and IP {client.RemoteEndPoint}");
pool.AddClient(client, new PrintMessageHandlerCreator(pool));
acceptor.Stop();
Console.WriteLine("stop accepting...");
Console.ReadLine();