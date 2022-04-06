using Medio.Network.ClientAcceptors;
using Medio.Network.ClientHandlers;
using Medio.Network.ClientPools;

namespace Medio.Network;

public class Session
{
    protected readonly IClientAcceptor _acceptor;
    protected readonly ClientPool _clientPool;
    protected readonly ClientHandlerPool _handlerPool;
    protected readonly IClientHandlerCreator _handlerCreator;

    public Session(
        IClientAcceptor acceptor,
        ClientPool clientPool,
        ClientHandlerPool handlerPool,
        IClientHandlerCreator handlerCreator
        )
    {
        _acceptor = acceptor;
        _clientPool = clientPool;
        _handlerPool = handlerPool;
        _handlerCreator = handlerCreator;
    }

    public bool Active { get; protected set; }

    public async Task StartAsync() 
    {
        await Task.Run(Start);
    }

    public void Start()
    {
        if (Active)
            throw new InvalidOperationException("Session already started!");

        Listen();
    }

    protected virtual void Listen()
    {
        _acceptor.Start();
        while (true)
        {
            var client = _acceptor.Accept();
            var clientHandler = _handlerCreator.Create(client);
            _clientPool.Add(client.Id, client);
            _handlerPool.Add(clientHandler.Client.Id, clientHandler);
        }
    }

    public void Stop()
    {
        if (Active == false)
            return;

        Active = false;
        _acceptor.Stop();
        _handlerPool.StopAll();
        _clientPool.CloseAll();
    }

    ~Session()
    {
        Stop();
    }
}
